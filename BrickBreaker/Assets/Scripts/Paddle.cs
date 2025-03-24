using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float maxRightDistance = 7.4f;
    public float maxLeftDistance = -7.4f;

    // Update is called once per frame
    void Update()
    {
        float horizontal  = Input.GetAxis("Horizontal");
        
        transform.Translate(Vector2.right * horizontal * movementSpeed * Time.deltaTime);
        if (transform.position.x < maxLeftDistance)
        {
            transform.position = new Vector2(maxLeftDistance, transform.position.y);
        }
        
        if (transform.position.x > maxRightDistance)
        {
            transform.position = new Vector2(maxRightDistance, transform.position.y);
        }
    }
}
