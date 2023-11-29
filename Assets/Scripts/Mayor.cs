using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mayor : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Transform player;
    public float activationDistance = 3.0f;
    private bool dialogueTriggered = false;
    public Image fadeOverlay; // Reference to the fade overlay image
    public float fadeDuration = 1.0f; // Duration of the fade effect

    // Update is called once per frame
    void Update()
    {
        if (!dialogueTriggered && Vector3.Distance(player.position, transform.position) <= activationDistance)
        {
            StartCoroutine(StartDialogueWithFade());
        }
    }

    IEnumerator StartDialogueWithFade()
    {
        // Fade in
        yield return StartCoroutine(FadeIn(fadeOverlay, fadeDuration));

        // Show dialogue
        dialogueManager.ShowDialogue(0);
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
