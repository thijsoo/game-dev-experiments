using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D eyeRigidbody;
    private Damageble damageble;
    private bool hasTarget = false;
    public DetectionZone attackZone;
    public List<Transform> waypoints;
    public float flightSpeed = 2f;
    public float waypointReachedDistance = 0.1f;
    int waypointIndex = 0;
    Transform nextWaypoint = null;
    public bool CanMove => animator.GetBool(AnimationStrings.canMove);
    
    public bool HasTarget
    {
        get => hasTarget;
        private set
        {
            hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    
    public float AttackCooldown
    {
        get => animator.GetFloat(AnimationStrings.attackCooldown);
        set => animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
    }
    
    // Start is called before the first frame update
    private void Awake()
    {
        eyeRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageble = GetComponent<Damageble>();
    }
    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        AttackCooldown -= Time.deltaTime;
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointIndex];
    }

    private void FixedUpdate()
    {
        if (damageble.IsAlive)
        {
            if (CanMove)
            {
                Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;
                
                float distanceToWaypoint = Vector2.Distance(nextWaypoint.position,transform.position);
                
                eyeRigidbody.velocity = directionToWaypoint * flightSpeed;
                if (transform.localScale.x > 0)
                {
                    if (eyeRigidbody.velocity.x < 0)
                    {
                        transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    }
                }
                else
                {
                    if (eyeRigidbody.velocity.x > 0)
                    {
                        transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    }
                }
                if (distanceToWaypoint <= waypointReachedDistance)
                {
                    waypointIndex++;
                    if (waypointIndex >= waypoints.Count)
                    {
                        waypointIndex = 0;
                    }
                    nextWaypoint = waypoints[waypointIndex];
                }
            }
            else
            {
                eyeRigidbody.velocity = Vector3.zero;
            }
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        eyeRigidbody.velocity = new Vector2(knockback.x, eyeRigidbody.velocity.y + knockback.y);
    }

    public void OnDeath()
    {
        CoinPickup.coinPickupEvent.Invoke(5);
        eyeRigidbody.gravityScale = 2f;
        eyeRigidbody.velocity = new Vector2(0, eyeRigidbody.velocity.y);
    }
}
