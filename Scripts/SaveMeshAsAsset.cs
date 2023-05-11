using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class SaveMeshAsAsset : MonoBehaviour
{
        public string assetName = "MyMesh";
        public string folderPath = "Assets/===MergeCastle===/RayFireMesh/";

        private void Awake()
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            Mesh mesh = meshFilter.sharedMesh;
            string assetPath = folderPath + assetName + gameObject.name.ToString() + ".asset";
            //AssetDatabase.CreateAsset(mesh, assetPath);
           // meshFilter.mesh = AssetDatabase.LoadAssetAtPath<Mesh>(assetPath);
        }
}
