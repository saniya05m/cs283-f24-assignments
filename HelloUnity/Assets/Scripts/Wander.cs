using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public float thresholdDistance = 1f; // To see if the target is too close
    public float wanderAreaRadius = 50f; // Radius within which to pick random points
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNewDestination();
    }

    void Update()
    {
        // Check if the agent is close to the target
        if (!agent.pathPending && agent.remainingDistance <= thresholdDistance)
        {
            SetNewDestination();
        }
    }

    void SetNewDestination()
    {
        // Generate a random point in a raidus and move the agent
        Vector3 randomDirection = Random.insideUnitSphere * wanderAreaRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderAreaRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}

