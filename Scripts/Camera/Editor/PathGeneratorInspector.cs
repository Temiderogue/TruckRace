using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathGenerator))]
public class PathGeneratorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        PathGenerator target1 = target as PathGenerator;

        DrawDefaultInspector();

        if (GUILayout.Button("Create Splain Points"))
        {
            target1.GenerateSpline();
        }
    }
}
