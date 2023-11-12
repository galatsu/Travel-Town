using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 3.0f;
    public GameObject uiElement; // UI element to activate
    public Vector3 enlargedScale = new Vector3(1.5f, 1.5f, 1.5f); // Scale when player is near
    private Vector3 originalScale;

    private bool isPlayerClose = false;

    private void Start()
    {
        originalScale = transform.localScale;
        uiElement.SetActive(false); // Ensure the UI element is initially deactivated
    }

    private void Update()
    {
        // Check the distance between the player and this object
        if (Vector3.Distance(player.position, transform.position) <= activationDistance)
        {
            if (!isPlayerClose)
            {
                transform.localScale = enlargedScale; // Enlarge the object
                isPlayerClose = true;
            }

            if (!uiElement.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.E) && isPlayerClose)
                {
                    uiElement.SetActive(true); // Activate the UI element
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    uiElement.SetActive(false);
                }
            }
        }
        else
        {
            if (isPlayerClose)
            {
                transform.localScale = originalScale; // Return to original scale
                isPlayerClose = false;
            }
        }
    }
}
