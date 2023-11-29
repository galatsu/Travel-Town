using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(DialogueManager))]
public class DialogueManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get the target object
        DialogueManager dialogueManager = (DialogueManager)target;

        // Ensure the serialized object is up-to-date
        serializedObject.Update();

        // Draw the default inspector for other public fields
        DrawDefaultInspector();

        // Display each dialogue node with its index
        SerializedProperty dialogueNodesProperty = serializedObject.FindProperty("dialogueNodes");
        for (int i = 0; i < dialogueNodesProperty.arraySize; i++)
        {
            SerializedProperty dialogueNodeProperty = dialogueNodesProperty.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Node " + i.ToString(), EditorStyles.boldLabel);

            // Display the default inspector for each field in the dialogue node
            EditorGUILayout.PropertyField(dialogueNodeProperty, true);

            EditorGUILayout.EndVertical();
        }

        // Button to add a new dialogue node
        if (GUILayout.Button("Add New Node"))
        {
            dialogueManager.dialogueNodes.Add(new DialogueNode());
        }

        // Apply changes to the serialized object
        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(dialogueManager);
        }
    }
}