using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    
    public AudioSource OnSelectClip;
  
    public LevelLoader LevelLoader;

    public void OnGameStart()
    {
        OnSelectClip.Play();
        StartCoroutine(LevelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    
}
