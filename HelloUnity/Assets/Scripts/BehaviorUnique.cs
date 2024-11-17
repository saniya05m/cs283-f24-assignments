using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTAI;
using UnityEngine.AI;

public class BehaviorUnique : MonoBehaviour
{   public Transform Player;

    public int runRange;

    public int killRange;

    private Animator animator;

    public int runRadius;
    public int NPCSpeed;
    NavMeshAgent agent;
    private Root m_btRoot = BT.Root();

    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {   
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = NPCSpeed;
        agent.isStopped = true;
        agent.updateRotation = false;
        m_btRoot.OpenBranch(
            BT.Selector().OpenBranch(
                BT.Sequence().OpenBranch(
                    BT.Condition(() => PlayerKilling()),
                    BT.Call(() => Die())
                )
            ),
            BT.Selector().OpenBranch(
                BT.Sequence().OpenBranch(
                    BT.Condition(() => PlayerNear()),
                    BT.Call(() => Run())
                )
            ),
            BT.Selector().OpenBranch(
                BT.Sequence().OpenBranch(
                    BT.Condition(() => !PlayerNear()),
                    BT.Call(() => Stop())
                )
            )

        ); 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        m_btRoot.Tick();
    }

    bool PlayerNear(){
        return Vector3.Distance(Player.position, transform.position) <= runRange && !isDead;
    }

    bool PlayerKilling(){
        return Vector3.Distance(Player.position, transform.position) <= killRange && Input.GetKey(KeyCode.K);
    }

    void Run(){
        Vector3 targetDirection = Player.transform.forward;

    // Rotate the agent to face the target direction
    if (targetDirection != Vector3.zero && !isDead)
    {
        Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
        Vector3 movement = Player.transform.forward * Time.deltaTime * NPCSpeed;
        agent.Move(movement);
        animator.SetBool("isRunning", true);
    }

    void Die(){
        Debug.Log("Sheep dead");
        animator.SetBool("isRunning", false);
        animator.SetBool("isDead", true);
        agent.isStopped = true;
        isDead = true;
        Vector3 currentEuler = transform.rotation.eulerAngles;

        Quaternion targetRotation = Quaternion.Euler(currentEuler.x, currentEuler.y, 90f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    void Stop(){
        animator.SetBool("isRunning", false);
        agent.isStopped = true;
    }

}
