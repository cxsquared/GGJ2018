/// <summary>
/// UnityTutorials - A Unity Game Design Prototyping Sandbox
/// <copyright>(c) John McElmurray and Julian Adams 2013</copyright>
/// 
/// UnityTutorials homepage: https://github.com/jm991/UnityTutorials
/// 
/// This software is provided 'as-is', without any express or implied
/// warranty.  In no event will the authors be held liable for any damages
/// arising from the use of this software.
///
/// Permission is granted to anyone to use this software for any purpose,
/// and to alter it and redistribute it freely, subject to the following restrictions:
///
/// 1. The origin of this software must not be misrepresented; you must not
/// claim that you wrote the original software. If you use this software
/// in a product, an acknowledgment in the product documentation would be
/// appreciated but is not required.
/// 2. Altered source versions must be plainly marked as such, and must not be
/// misrepresented as being the original software.
/// 3. This notice may not be removed or altered from any source distribution.
/// </summary>
using UnityEngine;
using UnityEditor;


/// <summary>
/// Struct to hold data for aligning camera
/// </summary>
struct CameraPosition 
{
	// Position to align camera to, probably somewhere behind the character
	// or position to point camera at, probably somewhere along character's axis
	private Vector3 position;
	// Transform used for any rotation
	private Transform xForm;
	
	public Vector3 Position { get { return position; } set { position = value; } }
	public Transform XForm { get { return xForm; } set { xForm = value; } }
	
	public void Init(string camName, Vector3 pos, Transform transform, Transform parent)
	{
		position = pos;
		xForm = transform;
		xForm.name = camName;
		xForm.parent = parent;
		xForm.localPosition = Vector3.zero;
		xForm.localPosition = position;
	}
}

/// <summary>
/// #DESCRIPTION OF CLASS#
/// </summary>
public class ThirdPersonCamera : MonoBehaviour
{
	#region Variables (private)
	
	// Inspector serialized	
	[SerializeField]
	private Transform cameraXform;
	[SerializeField]
	private float distanceAway;
	[SerializeField]
	private float distanceAwayMultipler = 1.5f;
	[SerializeField]
	private float distanceUp;
	[SerializeField]
	private float distanceUpMultiplier = 5f;
	[SerializeField]
	private PlayerMove follow;
	[SerializeField]
	private Transform followXform;
	[SerializeField]
	private float freeThreshold = -0.1f;
	[SerializeField]
	private Vector2 camMinDistFromChar = new Vector2(1f, -0.5f);
	[SerializeField]
	private float rightStickThreshold = 0.1f;
	[SerializeField]
	private const float freeRotationDegreePerSecond = -5f;
	[SerializeField]
	private float compensationOffset = 0.2f;
	[SerializeField]
	private CamStates startingState = CamStates.Free;
	
	// Smoothing and damping
    private Vector3 velocityCamSmooth = Vector3.zero;	
	[SerializeField]
	private float camSmoothDampTime = 0.1f;
    private Vector3 velocityLookDir = Vector3.zero;
	[SerializeField]
	private float lookDirDampTime = 0.1f;
	
	// Private global only
	private Vector3 lookDir;
	private Vector3 curLookDir;
	private CamStates camState = CamStates.Behind;	
	private float xAxisRot = 0.0f;
	private CameraPosition firstPersonCamPos;			
	private float lookWeight;
	private Vector3 savedRigToGoal;
	private float distanceAwayFree;
	private float distanceUpFree;	
	private Vector2 rightStickPrevFrame = Vector2.zero;
	private float lastStickMin = float.PositiveInfinity;	// Used to prevent from zooming in when holding back on the right stick/scrollwheel
	private Vector3 nearClipDimensions = Vector3.zero; // width, height, radius
	private Vector3[] viewFrustum;
	private Vector3 characterOffset;
	private Vector3 targetPosition;	
	
	#endregion
	
	
	#region Properties (public)	

	public Transform CameraXform
	{
		get
		{
			return this.cameraXform;
		}
	}

	public Vector3 LookDir
	{
		get
		{
			return this.curLookDir;
		}
	}

	public CamStates CamState
	{
		get
		{
			return this.camState;
		}
	}
	
	public enum CamStates
	{
		Behind,			// Single analog stick, Japanese-style; character orbits around camera; default for games like Mario64 and 3D Zelda series
		Free			// High angle; character moves relative to camera facing direction
	}

	public Vector3 RigToGoalDirection
	{
		get
		{
			// Move height and distance from character in separate parentRig transform since RotateAround has control of both position and rotation
			Vector3 rigToGoalDirection = Vector3.Normalize(characterOffset - this.transform.position);
			// Can't calculate distanceAway from a vector with Y axis rotation in it; zero it out
			rigToGoalDirection.y = 0f;

			return rigToGoalDirection;
		}
	}
	
	#endregion
	
	
	#region Unity event functions
	
	/// <summary>
	/// Use this for initialization.
	/// </summary>
	void Start ()
	{
		cameraXform = this.transform;//.parent;
		if (cameraXform == null)
		{
			Debug.LogError("Parent camera to empty GameObject.", this);
		}
		
		follow = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
		followXform = GameObject.FindWithTag("Player").transform;
		
		lookDir = followXform.forward;
		curLookDir = followXform.forward;
		
		camState = startingState;

		// Intialize values to avoid having 0s
		characterOffset = followXform.position + new Vector3(0f, distanceUp, 0f);
		distanceUpFree = distanceUp;
		distanceAwayFree = distanceAway;
		savedRigToGoal = RigToGoalDirection;
	}
	
	/// <summary>
	/// Debugging information should be put here.
	/// </summary>
	void OnDrawGizmos ()
	{	
		if (EditorApplication.isPlaying && !EditorApplication.isPaused)
		{			
			DebugDraw.DrawDebugFrustum(viewFrustum);
		}
	}
	
	void LateUpdate()
	{		
		viewFrustum = DebugDraw.CalculateViewFrustum(GetComponent<Camera>(), ref nearClipDimensions);

		// Pull values from controller/keyboard
		float rightX = Input.GetAxis("RightStickX");
		float rightY = Input.GetAxis("RightStickY");
		float leftX = Input.GetAxis("Horizontal");
		float leftY = Input.GetAxis("Vertical");
		bool qKeyDown = Input.GetKey(KeyCode.Q);
		bool eKeyDown = Input.GetKey(KeyCode.E);
		bool lShiftKeyDown = Input.GetKey(KeyCode.LeftShift);

		// Abstraction to set right Y when using mouse
		if (qKeyDown)
		{
			rightX = 1;
		}
		if (eKeyDown)
		{
			rightX = -1;
		}
		
		characterOffset = followXform.position + (distanceUp * followXform.up);
		Vector3 lookAt = characterOffset;
		targetPosition = Vector3.zero;
		
		// Determine camera state
		// * Targeting *

			// * Free *
			if ((rightY < freeThreshold) && System.Math.Round(follow.speed, 2) == 0)
			{
				camState = CamStates.Free;
				savedRigToGoal = Vector3.zero;
			}

		
		// Execute camera state
		switch (camState)
		{
			case CamStates.Behind:
				ResetCamera();
			
				// Only update camera look direction if moving
                if (follow.speed > follow.LocomotionThreshold)
				{
					lookDir = Vector3.Lerp(followXform.right * (leftX < 0 ? 1f : -1f), followXform.forward * (leftY < 0 ? -1f : 1f), Mathf.Abs(Vector3.Dot(this.transform.forward, followXform.forward)));
					Debug.DrawRay(this.transform.position, lookDir, Color.white);
				
					// Calculate direction from camera to player, kill Y, and normalize to give a valid direction with unit magnitude
					curLookDir = Vector3.Normalize(characterOffset - this.transform.position);
					curLookDir.y = 0;
					Debug.DrawRay(this.transform.position, curLookDir, Color.green);
				
					// Damping makes it so we don't update targetPosition while pivoting; camera shouldn't rotate around player
					curLookDir = Vector3.SmoothDamp(curLookDir, lookDir, ref velocityLookDir, lookDirDampTime);
				}				
				
				targetPosition = characterOffset + followXform.up * distanceUp - Vector3.Normalize(curLookDir) * distanceAway;
				Debug.DrawLine(followXform.position, targetPosition, Color.magenta);
				
				break;
		}
		

		CompensateForWalls(characterOffset, ref targetPosition);		
		SmoothPosition(cameraXform.position, targetPosition);	
		transform.LookAt(lookAt);	

		// Make sure to cache the unscaled mouse wheel value if using mouse/keyboard instead of controller
		rightStickPrevFrame = new Vector2(rightX, rightY);//mouseWheel != 0 ? mouseWheelScaled : rightY);
	}
	
	#endregion
	
	
	#region Methods
	
	private void SmoothPosition(Vector3 fromPos, Vector3 toPos)
	{		
		// Making a smooth transition between camera's current position and the position it wants to be in
		cameraXform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
	}

	private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
	{
		// Compensate for walls between camera
		RaycastHit wallHit = new RaycastHit();		
		if (Physics.Linecast(fromObject, toTarget, out wallHit)) 
		{
			Debug.DrawRay(wallHit.point, wallHit.normal, Color.red);
			toTarget = wallHit.point;
		}		
		
		// Compensate for geometry intersecting with near clip plane
		Vector3 camPosCache = GetComponent<Camera>().transform.position;
		GetComponent<Camera>().transform.position = toTarget;
		viewFrustum = DebugDraw.CalculateViewFrustum(GetComponent<Camera>(), ref nearClipDimensions);
		
		for (int i = 0; i < (viewFrustum.Length / 2); i++)
		{
			RaycastHit cWHit = new RaycastHit();
			RaycastHit cCWHit = new RaycastHit();
			
			// Cast lines in both directions around near clipping plane bounds
			while (Physics.Linecast(viewFrustum[i], viewFrustum[(i + 1) % (viewFrustum.Length / 2)], out cWHit) ||
			       Physics.Linecast(viewFrustum[(i + 1) % (viewFrustum.Length / 2)], viewFrustum[i], out cCWHit))
			{
				Vector3 normal = wallHit.normal;
				if (wallHit.normal == Vector3.zero)
				{
					// If there's no available wallHit, use normal of geometry intersected by LineCasts instead
					if (cWHit.normal == Vector3.zero)
					{
						if (cCWHit.normal == Vector3.zero)
						{
							Debug.LogError("No available geometry normal from near clip plane LineCasts. Something must be amuck.", this);
						}
						else
						{
							normal = cCWHit.normal;
						}
					}	
					else
					{
						normal = cWHit.normal;
					}
				}
				
				toTarget += (compensationOffset * normal);
				GetComponent<Camera>().transform.position += toTarget;
				
				// Recalculate positions of near clip plane
				viewFrustum = DebugDraw.CalculateViewFrustum(GetComponent<Camera>(), ref nearClipDimensions);
			}
		}
		
		GetComponent<Camera>().transform.position = camPosCache;
		viewFrustum = DebugDraw.CalculateViewFrustum(GetComponent<Camera>(), ref nearClipDimensions);
	}
	
	private void ResetCamera()
	{
		lookWeight = Mathf.Lerp(lookWeight, 0.0f, Time.deltaTime);
		transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime);
	}
	
	#endregion Methods
}
