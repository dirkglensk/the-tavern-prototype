using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PatronGroup))]
public class PatronGroupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var patronGroup = (PatronGroup)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Spawn!"))
        {
            patronGroup.Spawn();
        }

        GUILayout.Toggle(patronGroup.HasTableAssigned(), "Table assigned?");
    }
}
