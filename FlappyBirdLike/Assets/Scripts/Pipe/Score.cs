using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    public static UnityAction ScoreAction;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        ScoreAction?.Invoke();
    }
}
