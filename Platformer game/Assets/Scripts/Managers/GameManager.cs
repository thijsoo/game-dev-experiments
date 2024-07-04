using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    private static int score;
    public static GameManager Instance = null;


    private void Awake()
    {
        HandleCoinPickup(0);
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Duplicate GameManager created every time the scene is loaded
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        CoinPickup.coinPickupEvent += HandleCoinPickup;
    }

    private void OnDisable()
    {
        CoinPickup.coinPickupEvent -= HandleCoinPickup;
    }


    private void HandleCoinPickup(int amount)
    {
        score += amount;
        GameObject scoreTextObject = GameObject.Find("coin_amount");
        if (scoreTextObject != null )
        {
            scoreTextObject.GetComponent<TMP_Text>().text = "Coins: " + score;
        }

        scoreTextObject = GameObject.Find("final_coin_text");
        if (scoreTextObject != null)
        {
            scoreTextObject.GetComponent<TMP_Text>().text = "Final coin amount: " + score;
        }

        scoreTextObject = GameObject.Find("highscore_text");
        if (scoreTextObject != null)
        {
            scoreTextObject.GetComponent<TMP_Text>().text = "Current highscore:  " + score;
        }

        CheckHighScore();
    }

    private void CheckHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}