using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Reception))]
public class ReceptionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var reception = (Reception)target;
        base.OnInspectorGUI();
        EditorGUILayout.IntField("# Patrons", reception.CountPatronGroups());
    }
}
