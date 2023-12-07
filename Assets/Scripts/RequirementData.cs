using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RequirementsData", menuName = "Dialogue/RequirementsData", order = 1)]
public class RequirementsData : ScriptableObject
{
    public List<Requirement> requirements;

    [System.Serializable]
    public class Requirement
    {
        public string name;
        public bool isMet; // You can replace this with more complex logic if needed
    }

    public bool CheckRequirement(string requirementName)
    {
        Requirement requirement = requirements.Find(r => r.name == requirementName);
        return requirement != null && requirement.isMet;
    }
}