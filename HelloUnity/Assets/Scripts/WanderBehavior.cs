using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using BTAI;
public class WanderBehavior : MonoBehaviour
{
    public int wanderAreaRadius;  // Set to a sphere
    private Root m_btRoot = BT.Root(); 
    Animator animator;

    void Start()
    {  animator = GetComponent<Animator>();
        BTNode moveTo = BT.RunCoroutine(MoveToRandom);

        Sequence sequence = BT.Sequence();
        sequence.OpenBranch(moveTo);

        m_btRoot.OpenBranch(sequence);
        animator.SetBool("isRunning", true);
    }

    void Update()
    {
        m_btRoot.Tick();
    }

    IEnumerator<BTState> MoveToRandom()
    {
       NavMeshAgent agent = GetComponent<NavMeshAgent>();
      

       Vector3 randomDirection = Random.insideUnitSphere * wanderAreaRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderAreaRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
       // wait for agent to reach destination
       while (agent.remainingDistance > 0.1f)
       {
          yield return BTState.Continue;
       }

       yield return BTState.Success;
    }
}
