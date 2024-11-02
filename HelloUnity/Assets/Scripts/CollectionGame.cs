using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectionGame : MonoBehaviour
{
    public TextMeshProUGUI scoreText;   
    public GameObject[] coins;   
    private int score = 0;   

    void Start()
    {
        UpdateScoreText();
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < coins.Length; i++)
        {
            if (other.gameObject == coins[i])
            {
                score++;
                UpdateScoreText();

                coins[i].SetActive(false);

                break;
            }
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}

