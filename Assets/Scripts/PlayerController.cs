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
    public Sprite lunchSprite;
    private bool isOpenInventory = false;

    public GameManager gameManager;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventoryPanel.SetActive(false);
        gameManager.dialogueManager.ShowDialogue(0);
    }

    void Update()
    {
        HandleMovement();
        Inventory();

        if (gameManager.requirementsData.CheckRequirement("Cash") && isOpenInventory)
        {
            cashObject.SetActive(true);
        }
        else
        {
            cashObject.SetActive(false);
        }

        if (gameManager.requirementsData.CheckRequirement("Fish") && isOpenInventory)
        {
            sandwichObject.GetComponent<Image>().sprite = fishSprite;
        }
        else if (gameManager.requirementsData.CheckRequirement("Lunch") && isOpenInventory)
        {
            sandwichObject.GetComponent<Image>().sprite = lunchSprite;
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