using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SelectScene))]
public class SelectSceneEditor : Editor
{
    public override void OnInspectorGUI()
	{
        GUI.backgroundColor = Color.black;
        if (GUILayout.Button("현재 씬 시작"))
        {
            EditorStartInit.StartFromThisScene();
        }
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("처음 씬 시작"))
        {
            EditorStartInit.SetupFromStartScene();
        }
        
    }
}
