using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class PathGeneratorEditor : EditorWindow
{
    [SerializeField]
    private GameObject _pathGenerator;

    [MenuItem("Window/ CameraPathGenerator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PathGeneratorEditor));
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(new Vector2(30, 30), new Vector2(150, 100)), "Create Path Generator"))
        {
            Instantiate(_pathGenerator);
        }
    }
}
