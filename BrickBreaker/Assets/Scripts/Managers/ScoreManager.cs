using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private static int score;
    private TMP_Text scoreText;
    private static ScoreManager Instance = null;
    private void Awake()
    {
        scoreText = GameObject.Find("score_counter").GetComponent<TMP_Text>();
        SetScore();
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
        Brick.OnBrickDestroyed += OnBrickDestroyed;
    }

    private void OnDisable()
    {
        Brick.OnBrickDestroyed -= OnBrickDestroyed;
    }

    private void OnBrickDestroyed(int scoreWorth)
    {
        score += scoreWorth;
        SetScore();
    }

    private void SetScore()
    {
        scoreText.text = score.ToString();
    }
}
