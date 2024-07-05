using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private TMP_Text scoreText;
    private TMP_Text highScoreText;
    private static ScoreManager Instance = null;
    public static int ScoreValue = 0;

    private void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        highScoreText = GameObject.Find("HighScoreText")?.GetComponent<TMP_Text>();
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if(UNITY_EDITOR)
                UnityEditor.EditorApplication.isPlaying = false;
            #elif (UNITY_STANDALONE)
                Application.Quit();
            #endif
        }
    }
    private void OnEnable()
    {
        Score.ScoreAction += ScoreChange;
    }
    
    private void OnDisable()
    {
        Score.ScoreAction -= ScoreChange;
    }

    private void ScoreChange()
    {
        ScoreValue++;
        SetScore();
        CheckHighScore();
    }

    private void SetScore()
    {
        scoreText.text = ScoreValue.ToString();
        if (highScoreText != null)
        {
            highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
    }
    
    private void CheckHighScore()
    {
        if (ScoreValue > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", ScoreValue);
        }
    }
}
