using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SelectScene))]
public class SelectSceneEditor : Editor
{
    public override void OnInspectorGUI()
	{
        GUI.backgroundColor = Color.black;
        if (GUILayout.Button("���� �� ����"))
        {
            EditorStartInit.StartFromThisScene();
        }
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("ó�� �� ����"))
        {
            EditorStartInit.SetupFromStartScene();
        }
        
    }
}
