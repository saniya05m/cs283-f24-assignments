using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTAI;
using UnityEngine.AI;
public class BehaviorMinion : MonoBehaviour
{   public Transform Player;
    public Transform Home;
    NavMeshAgent agent;
    public int rangeFar;
    public int rangeHome;

    public int NPCSpeed;

    private Animator animator;
    public Transform NPCHome;
    private Root m_btRoot = BT.Root();
    public int rangeAttack;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = NPCSpeed;

        m_btRoot.OpenBranch(
            BT.Selector().OpenBranch(
                BT.Sequence().OpenBranch(
                    BT.Condition(() => PlayerInHome()),
                    BT.Call(() => Retreat())
                ),
                BT.Sequence().OpenBranch(
                    BT.Condition(() => PlayerInRange()),
                    BT.Call(() => Attack())
                ),
                BT.Sequence().OpenBranch(
                    BT.Condition(() => PlayerFollowRange()),
                    BT.Call(() => Follow())
                )
            )
        ); 
    }

    // Update is called once per frame
    void Update()
    {
        m_btRoot.Tick();
    }

    bool PlayerInRange(){
        return Vector3.Distance(transform.position, Player.position) <= rangeAttack;
    }

    bool PlayerInHome(){
        return Vector3.Distance(Home.position, Player.position)<=rangeHome;
    }

    bool PlayerFar(){
        return Vector3.Distance(transform.position, Player.position)>=rangeFar;
    }

    bool PlayerFollowRange(){
        return PlayerFar() && !PlayerInHome() && !PlayerInRange();
    }

    void Attack(){
        animator.SetBool("isAttack", true);
        agent.isStopped = true;
        animator.SetBool("isRunning", false);
    }

    void Follow(){
        animator.SetBool("isAttack", false);
        agent.isStopped = false;
        animator.SetBool("isRunning", true);
        agent.SetDestination(Player.position);

    }

    void Retreat(){
        animator.SetBool("isAttack", false);
        agent.isStopped = false;
        animator.SetBool("isRunning", true);
        agent.SetDestination(NPCHome.position);
    }
}

