using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Rigidbody2D rb;
    public GameObject inventoryPanel;
    private bool isOpenInventory = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        HandleMovement();
        Inventory();
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

    void Inventory()
    {
        if (isOpenInventory == true)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                inventoryPanel.SetActive(false);
                isOpenInventory = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                inventoryPanel.SetActive(true);
                isOpenInventory = true;
            }
        }
    }
}
