using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DialogueRequirement
{
    public string requirement; // Requirement identifier
    public int nextNodeIndex; // Corresponding next node index if the requirement is met
}

[System.Serializable]
public class DialogueNode
{
    public string speakerName;
    public Sprite MCSprite;
    public Sprite characterSprite;
    public Sprite dialogueBG;
    public string dialogueText;
    public List<string> choices;
    public List<int> nextNode;
    public bool hasChoices;
    public bool isFinalNode;
    public bool isReceivedCash;
    public bool isReceivedFish;
    public AudioClip typingSound;
    public int soundInterval = 4;

    public List<DialogueRequirement> requirements = new List<DialogueRequirement>();
}

public class DialogueManager : MonoBehaviour
{
    public Image MCImage;
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
    public bool isReceivedFish = false;

    public Image fadeOverlay;
    public float fadeDuration = 1.0f;

    public AudioSource audioSource;

    private bool isHasCashandFish;

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

        if(isReceivedCash && isReceivedFish)
        {
            isHasCashandFish = true;
        }
        else
        {
            isHasCashandFish = false;
        }
    }

    public void ShowDialogue(int nodeIndex)
    {
        DialogueNode node = dialogueNodes[nodeIndex];

        int nextNodeToUse = GetNextNodeIndexBasedOnRequirements(node);

        if (node.isFinalNode)
        {
            StartCoroutine(ClosePanelAfterDialogue(node.dialogueText, node.typingSound, node.soundInterval));
        }
        else
        {
            ShowDialoguePanel();

            speakerNameText.text = node.speakerName;
            MCImage.sprite = node.MCSprite;
            characterImage.sprite = node.characterSprite;
            dialogueBG.sprite = node.dialogueBG;

            StopAllCoroutines();
            StartCoroutine(TypeSentence(node.dialogueText, node.typingSound, node.soundInterval));

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
                    nextNodeIndex = nextNodeToUse;
                }
                else
                {
                    Debug.LogWarning("No next node specified for node: " + nodeIndex);
                    waitingForUserInput = false;
                }
            }
        }

        if (node.isReceivedCash)
        {
            isReceivedCash = true; // Handle cash received logic
        }
        if (node.isReceivedFish)
        {
            isReceivedFish = true;
        }

        // Additional handling for item interaction (if needed)
    }

    private int GetNextNodeIndexBasedOnRequirements(DialogueNode node)
    {
        foreach (var requirement in node.requirements)
        {
            if (CheckRequirement(requirement.requirement))
            {
                Debug.Log($"Requirement '{requirement.requirement}' met. Next node index: {requirement.nextNodeIndex}");
                return requirement.nextNodeIndex;
            }
        }

        int defaultNextNodeIndex = node.nextNode.Count > 0 ? node.nextNode[0] : -1;
        Debug.Log(defaultNextNodeIndex != -1 
            ? $"No requirements met. Proceeding to default next node index: {defaultNextNodeIndex}" 
            : "No requirements met and no default next node defined.");

        return defaultNextNodeIndex; // Default or error case
    }

    private bool CheckRequirement(string requirement)
    {
        switch (requirement)
        {
            case "Cash and fish":
                return isHasCashandFish;
            case "Cash":
                return isReceivedCash;
            case "Fish":
                return isReceivedFish;
            default:
                Debug.LogWarning("Unknown requirement: " + requirement);
                return false;
        }
    }

    IEnumerator TypeSentence(string sentence, AudioClip typingSound, int soundInterval)
    {
        dialogueText.text = "";
        int characterCount = 0;

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            characterCount++;

            // Play the sound at specified intervals
            if (characterCount % soundInterval == 0 && typingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typingSound);
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator ClosePanelAfterDialogue(string sentence, AudioClip typingSound, int soundInterval)
    {
        yield return StartCoroutine(TypeSentence(sentence, typingSound, soundInterval));

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
