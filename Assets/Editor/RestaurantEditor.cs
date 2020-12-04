using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Restaurant))]
public class RestaurantEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var self = (Restaurant) target;
        base.OnInspectorGUI();
        EditorGUILayout.IntField("# Tables", self.GetTables().Count);
        EditorGUILayout.IntField("# PGroups", self.GetPatronGroups().Count);
    }
}
