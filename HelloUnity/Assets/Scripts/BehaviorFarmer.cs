using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTAI;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class BehaviorFarmer : MonoBehaviour
{   public Transform Player;

    public GameObject greetPanel;
    public GameObject CharacterResponse;
    public TMP_Text textCharacter;
    public Button PlayerResponse;
    private Animator animator;
    private Animator PlayerAnimator;
    private Root m_btRoot = BT.Root();
    public int rangeClose;
    private bool isGreeted = false;

    public CollectionGame collectionGame;

    public BehaviorBlacksmith behaviorBlacksmith;
    private string stage = "GreetFarmer";
    public Button GreetOptionButton;
    public Button AskForJobButton;
    private bool isTasksDone = false;
    private bool isMoneyGiven = false;
    public GameObject coins;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerAnimator = Player.GetComponent<Animator>();
        m_btRoot.OpenBranch(
            BT.Selector().OpenBranch(
                BT.Sequence().OpenBranch(
                    BT.Condition(() => PlayerInRange() && !isGreeted),
                    BT.Call(() => Greet())
                ),
                BT.Sequence().OpenBranch(
                    BT.Condition(() => PlayerInRange() && isTasksDone && !isMoneyGiven),
                    BT.Call(() => Pay())
                )
            )
        ); 
    }

    public void AllTasksDone(){
        isTasksDone = true;
        Debug.Log("all tasks done");
    }
    // Update is called once per frame
    void Update()
    {
        m_btRoot.Tick();
    }

    bool PlayerInRange(){
        return Vector3.Distance(transform.position, Player.position) <= rangeClose;
    }

    void Greet(){
        isGreeted = true;
        greetPanel.SetActive(true);
        GreetOptionButton.onClick.AddListener(OnGreetPressed);
        AskForJobButton.onClick.AddListener(OnAskForJobPressed);
        StartCoroutine(Wave());
    }

    IEnumerator Wave(){
        animator.SetBool("isWave", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("isWave", false);
    }

    private void OnGreetPressed()
    {
        StartCoroutine(PlayerWave());
    }

    IEnumerator PlayerWave(){
        PlayerAnimator.SetBool("isWave", true);
        yield return new WaitForSeconds(2f);
        PlayerAnimator.SetBool("isWave", false);
    }

    private void OnAskForJobPressed(){
        greetPanel.SetActive(false);
        CharacterResponse.SetActive(true);
        PlayerResponse.gameObject.SetActive(true);
        PlayerResponse.onClick.AddListener(() => OnPlayerResponse("TaskGiven"));
        collectionGame.InstructionsGiven();
    }

    private void OnPlayerResponse(string interaction){
        if(interaction == "TaskGiven"){
            PlayerResponse.gameObject.SetActive(false);
            CharacterResponse.gameObject.SetActive(false);
            stage = "TaskInProgress";
        }
    }
    private void Pay(){
        Debug.Log("in pay coroutine");
        isMoneyGiven = true;
        StartCoroutine(ShowGameText("Thank you so much for helping me! Here is your 3 gold coins. Now go back to the blacksmith"));
        coins.SetActive(true);
        behaviorBlacksmith.AllTasksDone();
    }

    private IEnumerator ShowGameText(string textGameResponse){
        Debug.Log(textGameResponse);
        CharacterResponse.SetActive(true);
        textCharacter.text = textGameResponse;
        yield return new WaitForSeconds(5);
        CharacterResponse.SetActive(false);
    }
}
