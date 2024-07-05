using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public SceneLoader SceneLoader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(SceneLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }
}
