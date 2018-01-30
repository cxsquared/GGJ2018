using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(LineRenderer))]
public class NavigationTrail : MonoBehaviour {

    [SerializeField]
    private Transform[] _transistors;
    private Transform[] Transistors{ get { return _transistors; } set { _transistors = value; } }

    [SerializeField]
    private Transform _radioTower;
    private Transform RadioTower { get { return _radioTower; } set { _radioTower = value; } }

    [SerializeField]
    private float _trailOpacity = .5f;
    private float TrailOpacity { get { return _trailOpacity; } set { _trailOpacity = value; } }

	public static float ParticleSpeed = 5;
    public int ParticleSpeedThresholdForRadioTower;
    public Transform Player;

    private NavMeshAgent Agent { get; set; }
    private LineRenderer Line { get; set; }
    private Transform CurrentTarget { get; set; }
    private NavMeshPath Path { get; set; }
    private CapsuleCollider ParentCapsule { get; set; }
    private ParticleSystem Particle { get; set; }

    private int CurrentParticleWaypoint { get; set; }
    private Vector3 CurrentParticleTarget { get; set; }

	// Use this for initialization
	void Start () {
        Agent = GetComponent<NavMeshAgent>();
        Path = new NavMeshPath();
        Line = GetComponent<LineRenderer>();
        ParentCapsule = GetComponentInParent<CapsuleCollider>();
        Particle = GetComponentInChildren<ParticleSystem>();
        CurrentParticleWaypoint = 0;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Player.position;
        UpdatePathTarget();
	}

    void UpdatePathTarget()
    {
        var oldCurrentTarget = CurrentTarget;
        if (CurrentTarget == null)
        {
            CurrentTarget = RadioTower;
        }

        if (ParticleSpeed < ParticleSpeedThresholdForRadioTower)
        {
            var distanceToTarget = Vector3.Distance(this.transform.position, CurrentTarget.position);

            foreach (var transistor in Transistors)
            {
                if (!transistor.gameObject.activeInHierarchy)
                    continue;

                var newDistance = Vector3.Distance(this.transform.position, transistor.position);

                if (newDistance < distanceToTarget)
                {
                    CurrentTarget = transistor;
                    distanceToTarget = newDistance;
                }
            }
        }
        else
        {
            CurrentTarget = RadioTower;
        }

        Line.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        Agent.SetDestination(CurrentTarget.position);

        RenderPathLine(Agent.path);

        if (CurrentParticleWaypoint < Agent.path.corners.Length)
        {
            if (CurrentParticleTarget == null)
            {
                CurrentParticleTarget = Agent.path.corners[CurrentParticleWaypoint];
            }
            MoveParticle();
        }

        Agent.isStopped = true;
    }

    void MoveParticle()
    {
        Particle.transform.forward = Vector3.RotateTowards(Particle.transform.forward, CurrentParticleTarget - Particle.transform.position, ParticleSpeed * Time.deltaTime, 0.0f);

        // move towards the target
        Particle.transform.position = Vector3.MoveTowards(Particle.transform.position, CurrentParticleTarget, ParticleSpeed * Time.deltaTime);

        if (Vector3.Distance(Particle.transform.position, CurrentParticleTarget) < .25)
        {
            CurrentParticleWaypoint++;
            if (CurrentParticleWaypoint >= Agent.path.corners.Length)
            {
                CurrentParticleWaypoint = 0;
                Particle.transform.position = Agent.path.corners[CurrentParticleWaypoint];
            }
            CurrentParticleTarget = Agent.path.corners[CurrentParticleWaypoint];
        }
    }

    void RenderPathLine(NavMeshPath path)
    {
        if (path.corners.Length < 2)
        {
            Line.material.color = Color.clear;
            return;
        }

        for (var i = 0; i < Line.colorGradient.alphaKeys.Length; i++) {
            Line.colorGradient.alphaKeys[i] = new GradientAlphaKey(TrailOpacity, .5f);
        }

        Line.positionCount = path.corners.Length;

        for (var i = 1; i < path.corners.Length; i++)
        {
            var point = path.corners[i];
            point.y = FindFloor(path.corners[i]);

            Line.SetPosition(i, point);
        }
 
    }

    float FindFloor(Vector3 worldPoint)
    {
        RaycastHit hit; 
        if (Physics.Raycast(worldPoint, Vector3.down, out hit)) {
            return hit.point.y; 
        }

        return worldPoint.y;
    }
}
