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

    private NavMeshAgent Agent { get; set; }
    private LineRenderer Line { get; set; }
    private Transform CurrentTarget { get; set; }
    private NavMeshPath Path { get; set; } 

	// Use this for initialization
	void Start () {
        Agent = GetComponent<NavMeshAgent>();
        Path = new NavMeshPath();
        Line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdatePathTarget();
        RenderPathLine();
	}

    void UpdatePathTarget()
    {
        var oldCurrentTarget = CurrentTarget;
        if (CurrentTarget == null)
        {
            CurrentTarget = RadioTower;
        }

        var distanceToTarget = Vector3.Distance(this.transform.position, CurrentTarget.position);

        foreach (var transistor in Transistors)
        {
            var newDistance = Vector3.Distance(this.transform.position, transistor.position);

            if (newDistance < distanceToTarget)
            {
                CurrentTarget = transistor;
                distanceToTarget = newDistance;
            }
        }
    }

    void RenderPathLine()
    {
        if (Agent.CalculatePath(CurrentTarget.position, Path))
        {
            Line.positionCount = Path.corners.Length;
            for( var i = 0; i < Path.corners.Length; i++)
            {
                Line.SetPosition(i, Path.corners[i]);
            }
        }
    }
}
