using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    
    public SceneLoader SceneLoader;


    
    public void OnStartGameClicked()
    {
        StartCoroutine(SceneLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    }
}
