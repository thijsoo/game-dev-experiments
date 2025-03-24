using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int brickAmount;
    private void Awake()
    {
        brickAmount = FindObjectsOfType<Brick>().Length;
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
        brickAmount --;
    }

    // Update is called once per frame
    void Update()
    {
        if (brickAmount == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
        
    }
}
