using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Rigidbody2D rb;
    //private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovement();
        //FlipSprite();
    }

    void HandleMovement()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    //void FlipSprite()
    //{
    //    if (moveInput.x > 0)
    //    {
    //        spriteRenderer.flipX = false; // Facing right
    //    }
    //    else if (moveInput.x < 0)
    //    {
    //        spriteRenderer.flipX = true; // Facing left
    //    }
    //}
}
