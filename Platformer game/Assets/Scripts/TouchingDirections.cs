using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    private Animator animator;
    private CapsuleCollider2D touchingCapsuleColider2d;
    private Vector2 wallDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private float groundDistance = 0.05f;
    private float wallDistance = 0.2f;
    private float ceilingDistance = 0.05f;

    [SerializeField] private bool isOnWall = false;

    public bool IsOnWall
    {
        get => isOnWall;
        private set
        {
            isOnWall = value;
            this.animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    [SerializeField] private bool isOnCeiling = false;

    public bool IsOnCeiling
    {
        get => isOnCeiling;
        private set
        {
            isOnCeiling = value;
            this.animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    [SerializeField] private bool isGrounded = true;


    public bool IsGrounded
    {
        get => isGrounded;
        private set
        {
            isGrounded = value;
            this.animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
        this.touchingCapsuleColider2d = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        IsGrounded = touchingCapsuleColider2d.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCapsuleColider2d.Cast(wallDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCapsuleColider2d.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}