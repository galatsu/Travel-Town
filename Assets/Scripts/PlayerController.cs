using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Rigidbody2D rb;
    public GameObject inventoryPanel;
    public GameObject cashObject;
    public GameObject sandwichObject;
    public Sprite fishSprite;
    private bool isOpenInventory = false;

    public GameManager gameManager;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        HandleMovement();
        Inventory();

        if (gameManager.isReceivedCash && isOpenInventory)
        {
            cashObject.SetActive(true);
        }
        else
        {
            cashObject.SetActive(false);
        }

        if (gameManager.isReceivedFish && isOpenInventory)
        {
            sandwichObject.GetComponent<Image>().sprite = fishSprite;
        }
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
