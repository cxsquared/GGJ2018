using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodyThridPersonCamera : MonoBehaviour {

    [SerializeField]
    private GameObject FollowCharacter;
    [SerializeField]
    private float DistanceUp = 3;
    [SerializeField]
    private float DistanceAway = 5;
    [SerializeField]
    private float Dampening = .5f;
    [SerializeField]
	private float compensationOffset = 0.2f;

	private Vector3[] viewFrustum;
    private Vector3 nearClipDimensions = Vector3.zero; // width, height, radius

    // Use this for initialization
    void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
        var targetPostion = FollowCharacter.transform.position + (FollowCharacter.transform.forward * -DistanceAway);
        targetPostion.y += DistanceUp;

        CompensateForWalls(transform.position, ref targetPostion);

        transform.position = Vector3.Lerp(transform.position, targetPostion, Time.deltaTime * Dampening);

        transform.LookAt(FollowCharacter.transform);
	}

    private void FixedUpdate()
    {
		viewFrustum = DebugDraw.CalculateViewFrustum(GetComponent<Camera>(), ref nearClipDimensions);
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
}
