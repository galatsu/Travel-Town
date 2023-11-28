using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mayor : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Transform player;
    public float activationDistance = 3.0f;
    private bool dialogueTriggered = false;

    // Update is called once per frame
    void Update()
    {
        if (!dialogueTriggered && Vector3.Distance(player.position, transform.position) <= activationDistance)
        {
            dialogueManager.ShowDialogue(0);
            dialogueTriggered = true;
        }
        //else if (Vector3.Distance(player.position, transform.position) > activationDistance)
        //{
        //    dialogueTriggered = false;
        //}
    }
}