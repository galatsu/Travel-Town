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
    public string dialogueText;
    public List<string> choices;
    public List<int> nextNode;
    public bool hasChoices;
    public bool isFinalNode;
}

public class DialogueManager : MonoBehaviour
{
    public Image characterImage;
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText; // Use Text if not using TextMeshPro
    public GameObject[] choiceButtons; // An array of choice buttons
    public GameObject dialoguePanel;
    public float typingSpeed = 0.05f;

    private bool waitingForUserInput = false;
    private int nextNodeIndex = 0;
    public List<DialogueNode> dialogueNodes;

    private AudioSource Talking;


    void Start()
    {
        HideDialoguePanel();
        Talking = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (waitingForUserInput && Input.GetMouseButtonDown(0))
        {
            waitingForUserInput = false;
            ShowDialogue(nextNodeIndex);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            ShowDialogue(nextNodeIndex);

        }
    }
    
    public void ShowDialogue(int nodeIndex)
    {
        DialogueNode node = dialogueNodes[nodeIndex];
        Talking.Play();

        if (node.isFinalNode)
        {
            StartCoroutine(ClosePanelAfterDialogue(node.dialogueText));
        }
        else
        {
            ShowDialoguePanel();

            speakerNameText.text = node.speakerName;
            characterImage.sprite = node.characterSprite;

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
        HideDialoguePanel();
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
