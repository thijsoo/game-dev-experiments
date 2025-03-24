using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public static UnityAction<int> OnBrickDestroyed;
    public int hitsNeeded = 1;
    public int scoreWorth = 1;
    private int hitsTaken = 0;

    public List<Sprite> sprites;

    public void Hit()
    {
        hitsTaken++;
        if (hitsTaken == hitsNeeded)
        {
            OnBrickDestroyed.Invoke(scoreWorth);
            Destroy(gameObject);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[hitsTaken];
        }
    }
}
