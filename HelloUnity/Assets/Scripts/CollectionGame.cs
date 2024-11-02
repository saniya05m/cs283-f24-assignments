using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectionGame : MonoBehaviour
{
    public TextMeshProUGUI scoreText;     
    private int score = 0;   

    void Start()
    {
        UpdateScoreText();
    }

    private void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Coin"))
            {
                score++;
                UpdateScoreText();

                other.gameObject.SetActive(false);

            }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}

