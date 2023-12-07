using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RequirementsData;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public RequirementsData requirementsData;

    public DialogueManager dialogueManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager.ShowDialogue(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (requirementsData.CheckRequirement("Cash") && requirementsData.CheckRequirement("Fish"))
        {
            UpdateRequirementStatus("Cash and fish", true);
        }
        else
        {
            UpdateRequirementStatus("Cash and fish", false);
        }
    }

    public void UpdateRequirementStatus(string requirementName, bool status)
    {
        RequirementsData.Requirement requirement = requirementsData.requirements.Find(r => r.name == requirementName);
        if (requirement != null)
        {
            requirement.isMet = status;
        }
        else
        {
            Debug.LogWarning("Requirement not found: " + requirementName);
        }
    }
}