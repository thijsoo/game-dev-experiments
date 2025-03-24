using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody2D ballBody;
    // Start is called before the first frame update
    private void Awake()
    {
        ballBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        ballBody.AddForce(Vector2.up * 300);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Breakable"))
        {
            if (other.gameObject.GetComponent<Brick>() != null)
            {
                Brick brick = other.gameObject.GetComponent<Brick>();
                brick.Hit();
            }
           
        }
       
    }
}
