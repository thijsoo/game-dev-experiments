using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
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
       
    }
}
