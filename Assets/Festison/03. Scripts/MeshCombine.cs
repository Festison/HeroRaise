using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class MeshCombine : MonoBehaviour
{
    public GameObject[] MeshObj;

    void Start()
    {
        MeshFilter[] meshFilters = new MeshFilter[MeshObj.Length];
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = 0; i < meshFilters.Length; i++)
        {
            meshFilters[i] = MeshObj[i].GetComponent<MeshFilter>();
            combine[i].mesh = meshFilters[i].mesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        Mesh mesh = this.transform.GetComponent<MeshFilter>().mesh;

        mesh.Clear();
        mesh.CombineMeshes(combine);

        this.gameObject.AddComponent<MeshCollider>();

#if UNITY_EDITOR
        { // Mesh ¿˙¿Â
            string path = "Assets/MyMesh.asset";
            AssetDatabase.CreateAsset(transform.GetComponent<MeshFilter>().mesh, AssetDatabase.GenerateUniqueAssetPath(path));
            AssetDatabase.SaveAssets();
        }
#endif
    }

}