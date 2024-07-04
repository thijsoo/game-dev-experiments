using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public LevelLoader LevelLoader;
    public CinemachineVirtualCamera VirtualCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        VirtualCamera.Follow = null;
        StartCoroutine(LevelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }
}
