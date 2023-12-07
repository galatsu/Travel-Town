using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Transform player;
    public float activationDistance = 3.0f;
    private bool dialogueTriggered = false;
    public Image fadeOverlay;
    public float fadeDuration = 1.0f;
    public int dialogueIndex = 0;

    public float movementThreshold = 0f;
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, lastPosition) <= movementThreshold)
        {
            if (!dialogueTriggered && Vector3.Distance(player.position, transform.position) <= activationDistance)
            {
                StartCoroutine(StartDialogueWithFade());
            }
        }

        lastPosition = transform.position;
    }

    IEnumerator StartDialogueWithFade()
    {
        // Fade in
        yield return StartCoroutine(FadeIn(fadeOverlay, fadeDuration));

        // Show dialogue
        dialogueManager.ShowDialogue(dialogueIndex);
        dialogueTriggered = true;

        // Fade out
        yield return StartCoroutine(FadeOut(fadeOverlay, fadeDuration));
    }

    IEnumerator FadeIn(Image image, float duration)
    {
        float elapsedTime = 0;
        Color color = image.color;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / duration);
            image.color = color;
            yield return null;
        }
    }

    IEnumerator FadeOut(Image image, float duration)
    {
        float elapsedTime = 0;
        Color color = image.color;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - (elapsedTime / duration));
            image.color = color;
            yield return null;
        }
    }
}
