using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BTAI;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class CollectionGame : MonoBehaviour
{
    public TextMeshProUGUI scoreText;     
    private int score = 0;   
    public string TagObj;
    bool isSword = false;
    Animator animator;
    public GameObject wrist;
    public GameObject farm;
    public GameObject seedDirt;
    public GameObject wateringDirt;
    public GameObject cabbageDirt;
    public GameObject GameResponse;
    public GameObject GameResponseBig;
    public Transform barn;
    public GameObject inHand = null; //variable to store what the player has in hand;

    private GameObject lastInHand = null;
    private float cooldown = 0f; //this fixes my issue of a dropped object being picked up again, due to fast processing of ontrigger.
    public float pickupcooldown = 5.0f;
    public TMP_Text textGame;
    public TMP_Text textGameBig;
    public GameObject wateringEffect;
    private bool WateringTextShown = false;
    private bool CabbageTextShown = false;
    private bool GameStartTextShown = false;
    private bool pickupTextShown = false;
    private bool plantingTextShown = false;
    public GameObject Cabbage;

    public BehaviorFarmer behaviorFarmer;
    
    
    enum gameState{
        Start = 0,
        FarmerTalk1 = 1,
        
        Barn = 2,
        Plant = 3,
        Water = 4,
        Cabbage = 5,
        Done = 6

    }

    gameState currentState = gameState.Start;
    void Start()
    {
        UpdateScoreText();
        animator = GetComponent<Animator>();
        
    }

    public void InstructionsGiven(){
        currentState = gameState.Barn;
    }

    private void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag(TagObj) && isSword)
            {   Debug.Log("Object encountered");
                score++;
                UpdateScoreText();
                isSword = true;
                other.gameObject.SetActive(false);

            }
            if (other.CompareTag("Seed")){
                Debug.Log("Seed encountered");
                //other.gameObject.transform.position = wrist.transform.position;
            }
    }

    private void OnTriggerStay(Collider other){
        if(Input.GetKey(KeyCode.I) && cooldown<=0){
        if (other.CompareTag("Seed") &&(inHand!=other)){
            cooldown = pickupcooldown;
            Debug.Log("Seed Ontrigger Stay");
                if (inHand!=null){
                    Vector3 pos = inHand.transform.position;
                    pos.y = 1;
                    inHand.transform.position = pos;
                    inHand.transform.SetParent(farm.transform);
                }
                Vector3 newpos = wrist.transform.position;
                newpos.y -= 1;
                other.gameObject.transform.position = newpos;
                other.gameObject.transform.SetParent(wrist.transform);
                lastInHand = inHand;
                inHand = other.gameObject;
            

        }

        if (other.CompareTag("Can") && (inHand!=other)){
            cooldown = pickupcooldown;
            Debug.Log("Can OnTrigger Stay");
                if (inHand!=null){
                    Debug.Log("inHand not null");
                    Vector3 pos = inHand.transform.position;
                    pos.y = 1;
                    inHand.transform.position = pos;
                    inHand.transform.SetParent(farm.transform);
                }
                Vector3 newpos = wrist.transform.position;
                newpos.y -= 3;
                other.gameObject.transform.position = newpos;
                other.gameObject.transform.SetParent(wrist.transform);
                lastInHand = inHand;
                inHand = other.gameObject;
            
        }

        if(other.CompareTag("Coins") && inHand!=other){
            cooldown = pickupcooldown;
            Debug.Log("Coins on trigger stay");
            if (inHand!=null){
                    Debug.Log("inHand not null");
                    Vector3 pos = inHand.transform.position;
                    pos.y = 1;
                    inHand.transform.position = pos;
                    inHand.transform.SetParent(farm.transform);
                }
                Vector3 newpos = wrist.transform.position;
                newpos.y -= 3;
                other.gameObject.transform.position = newpos;
                other.gameObject.transform.SetParent(wrist.transform);
                lastInHand = inHand;
                inHand = other.gameObject;
        }
    }
    }

    void UpdateScoreText()
    { 
        scoreText.text = score.ToString();
    }

    private void DropInHand(){
        if (inHand!=null){
                    Debug.Log("inHand not null");
                    Vector3 pos = inHand.transform.position;
                    pos.y = 1;
                    inHand.transform.position = pos;
                    inHand.transform.SetParent(farm.transform);
                    
                    lastInHand = inHand;
                    inHand = null;
                }
    }

    void Update(){
        if (cooldown>0){
            cooldown -= Time.deltaTime;
        }
        isSword = false;
        animator.SetBool("isSword", false);

        if((int)currentState == 0 && !GameStartTextShown){
            GameStartTextShown = true;
            StartCoroutine(ShowGameTextBig("Your carriage is broken! But a blacksmith can fix it. Go to her, she's in front of the forest.", 7));
        }

        if ((int)currentState == 2 && Vector3.Distance(transform.position, seedDirt.transform.position) <= 10){ 
            if(!plantingTextShown){
                plantingTextShown = true;
                StartCoroutine(ShowGameText("Press K to drop the seed"));
                
            }
            if (Input.GetKey(KeyCode.K)){
                if(inHand.CompareTag("Seed")){
                    isSword = true; animator.SetBool("isSword", isSword);
                    DropInHand();
                    lastInHand.SetActive(false);
                    StartCoroutine(ShowGameText("Planting seeds done! Watering next."));
                    currentState = gameState.Plant;
                }
            }  
            }
        if ((int)currentState == 2 && !pickupTextShown && Vector3.Distance(transform.position, barn.position)<=15){
            pickupTextShown = true;
            StartCoroutine(ShowGameText("Press I to pick up item \n Press K to drop item"));
            
        }
        if ((int)currentState == 3 && Vector3.Distance(transform.position, wateringDirt.transform.position) <=10){
            if (!WateringTextShown){
                StartCoroutine(ShowGameText("Press Space to water"));
                WateringTextShown = true;
            }
            if (Input.GetKey(KeyCode.Space)){
                StartCoroutine(ShowWateringEffect());
            }    
        }

        if ((int)currentState == 4 && Vector3.Distance(transform.position, cabbageDirt.transform.position) <=10){
            if(!CabbageTextShown){
                StartCoroutine(ShowGameText("Press Space to pick the cabbage"));
                CabbageTextShown = true;
            }

            if (Input.GetKey(KeyCode.Space)){
                StartCoroutine(ShowCabbageEffect());
            }
        }

        if((int)currentState == 5 && Vector3.Distance(transform.position, barn.transform.position) <=20){
            if(Input.GetKey(KeyCode.K)){
                 DropInHand();
                 StartCoroutine(ShowGameText("Congrats! All tasks done! Now go to the farmer"));
                 currentState = gameState.Done;
                 behaviorFarmer.AllTasksDone();
            }
        }
        if (Input.GetKey(KeyCode.K)){
            if (inHand!=null){
                DropInHand();
            }
        } 
    }
    
    private IEnumerator EnableSword()
    {
        // Set isSword to true
        isSword = true;
        animator.SetBool("isSword", isSword);
        // Wait for 3 seconds
        yield return new WaitForSeconds(3);

        // Set isSword to false
        isSword = false;
        animator.SetBool("isSword", isSword);
    }

    private IEnumerator ShowGameText(string textGameResponse, float seconds = 5){
        Debug.Log(textGameResponse);
        GameResponse.SetActive(true);
        textGame.text = textGameResponse;
        yield return new WaitForSeconds(seconds);
        GameResponse.SetActive(false);
    }

    private IEnumerator ShowGameTextBig(string textGameResponse, float seconds = 5){
        Debug.Log(textGameResponse);
        GameResponseBig.SetActive(true);
        textGameBig.text = textGameResponse;
        yield return new WaitForSeconds(seconds);
        GameResponseBig.SetActive(false);
    }

    private IEnumerator ShowWateringEffect(){
        Debug.Log("In watering effect coroutine");
        if(!wateringEffect.activeInHierarchy){
            Debug.Log("Watering effect was off now its on");
            wateringEffect.SetActive(true);
            yield return new WaitForSeconds(5);
            wateringEffect.SetActive(false);
            StartCoroutine(ShowGameText("Watering done! Now time to pick out cabbage"));
            currentState = gameState.Water;
        }
        
    }

    private IEnumerator ShowCabbageEffect(){
        Debug.Log("In Cabbage effect coroutine");
        animator.SetBool("isSword", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("isSword", false);
        animator.SetBool("isShake",true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isShake", false);

        DropInHand();
        Vector3 newpos = wrist.transform.position;
                newpos.y -= 3;
                Cabbage.gameObject.transform.position = newpos;
                Cabbage.gameObject.transform.SetParent(wrist.transform);
                lastInHand = inHand;
                inHand = Cabbage.gameObject;
        currentState = gameState.Cabbage;
        StartCoroutine(ShowGameText("Yay! Now go and place the cabbage in the barn"));

    }
}

