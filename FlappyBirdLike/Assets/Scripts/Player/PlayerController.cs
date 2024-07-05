using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float vertSpeed = 0f;
    public float fallingConstant = 17f;
    public float jumpIntensity = 7f;

    private Transform playerBody;
    private void Awake()
    {
        playerBody = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            vertSpeed = jumpIntensity;
        }
    }

    private void FixedUpdate()
    {
  
        playerBody.position = new Vector2(playerBody.position.x, playerBody.position.y + vertSpeed * Time.fixedDeltaTime);
        vertSpeed -= fallingConstant * Time.fixedDeltaTime;
    }
 
    
}
