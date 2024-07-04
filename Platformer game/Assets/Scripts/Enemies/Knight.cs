using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float walkAcceleration = 50f;
    public float maxSpeed = 3f;
    private float walkStopRate = 0.05f;

    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    private Rigidbody2D knightRigidbody;
    private TouchingDirections touchingDirections;
    private Animator animator;
    private Damageble damageble;

    public enum WalkableDirection
    {
        Right,
        Left
    }

    private WalkableDirection walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get => walkDirection;
        set
        {
            if (walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }

            walkDirection = value;
        }
    }

    private bool hasTarget = false;

    public bool HasTarget
    {
        get => hasTarget;
        private set
        {
            hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove => animator.GetBool(AnimationStrings.canMove);

    public float AttackCooldown
    {
        get => animator.GetFloat(AnimationStrings.attackCooldown);
        set => animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
    }

    private void Awake()
    {
        knightRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageble = GetComponent<Damageble>();
    }

    // Start is called before the first frame update
    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        AttackCooldown -= Time.deltaTime;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (damageble.LockVelocity)
        {
            return;
        }

        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        if (CanMove && touchingDirections.IsGrounded)
        {
            float xVelocity = Mathf.Clamp(
                knightRigidbody.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime),
                -maxSpeed,maxSpeed
                );
            knightRigidbody.velocity = new Vector2(xVelocity, knightRigidbody.velocity.y);
        }
        else
        {
            knightRigidbody.velocity = new Vector2(Mathf.Lerp(knightRigidbody.velocity.x, 0, walkStopRate),
                knightRigidbody.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("NANI!");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        knightRigidbody.velocity = new Vector2(knockback.x, knightRigidbody.velocity.y + knockback.y);
    }
    
    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        } 
    }
    
    public void OnDeath()
    {
        CoinPickup.coinPickupEvent.Invoke(7);
    }
}