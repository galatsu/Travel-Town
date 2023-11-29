using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DialogueNode
{
    public string speakerName;
    public Sprite characterSprite;
    public Sprite dialogueBG;
    public string dialogueText;
    public List<string> choices;
    public List<int> nextNode;
    public bool hasChoices;
    public bool isFinalNode;
    public bool isReceivedCash;
}

public class DialogueManager : MonoBehaviour
{
    public Image characterImage;
    public Image dialogueBG;
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText; // Use Text if not using TextMeshPro
    public GameObject[] choiceButtons; // An array of choice buttons
    public GameObject dialoguePanel;
    public float typingSpeed = 0.05f;

    private bool waitingForUserInput = false;
    private int nextNodeIndex = 0;
    public List<DialogueNode> dialogueNodes;

    public bool isReceivedCash = false;

    public Image fadeOverlay;
    public float fadeDuration = 1.0f;

    void Start()
    {
        HideDialoguePanel();
    }

    void Update()
    {
        if (waitingForUserInput && Input.GetMouseButtonDown(0))
        {
            waitingForUserInput = false;
            ShowDialogue(nextNodeIndex);
        }
    }

    public void ShowDialogue(int nodeIndex)
    {
        DialogueNode node = dialogueNodes[nodeIndex];

        if (node.isFinalNode)
        {
            StartCoroutine(ClosePanelAfterDialogue(node.dialogueText));
        }
        else
        {
            ShowDialoguePanel();

            speakerNameText.text = node.speakerName;
            characterImage.sprite = node.characterSprite;
            dialogueBG.sprite = node.dialogueBG;


            StopAllCoroutines(); // Stop previous typewriter effects if any
            StartCoroutine(TypeSentence(node.dialogueText));

            if (node.hasChoices)
            {
                for (int i = 0; i < node.choices.Count; i++)
                {
                    choiceButtons[i].SetActive(true);
                    choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = node.choices[i];
                    int nextNodeIndex = node.nextNode[i];
                    choiceButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                    choiceButtons[i].GetComponent<Button>().onClick.AddListener(() => ShowDialogue(nextNodeIndex));
                }

                for (int i = node.choices.Count; i < choiceButtons.Length; i++)
                {
                    choiceButtons[i].SetActive(false);
                }

                waitingForUserInput = false;
            }
            else
            {
                for (int i = 0; i < choiceButtons.Length; i++)
                {
                    choiceButtons[i].SetActive(false);
                }

                if (node.nextNode.Count > 0)
                {
                    waitingForUserInput = true;
                    nextNodeIndex = node.nextNode[0];
                }
                else
                {
                    Debug.LogWarning("No next node specified for node: " + nodeIndex);
                    waitingForUserInput = false;
                }
            }
        }

        if(node.isReceivedCash)
        {
            isReceivedCash = true;
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator ClosePanelAfterDialogue(string sentence)
    {
        yield return StartCoroutine(TypeSentence(sentence));

        yield return StartCoroutine(FadeIn(fadeOverlay, fadeDuration));

        HideDialoguePanel();

        // Fade out after closing the panel
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

    void HideDialoguePanel()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void ShowDialoguePanel()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);
    }
}
