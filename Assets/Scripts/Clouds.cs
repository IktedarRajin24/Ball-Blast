using UnityEngine;
using System;

public class Clouds : MonoBehaviour
{
    [Serializable] class Cloud
    {
        public MeshRenderer meshRenderer;
        public float speed = 0f;
        [HideInInspector] public Vector2 offset;
        [HideInInspector] public Material mat;
    }

    [SerializeField] Cloud[] allClouds;

    int cloudCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cloudCount = allClouds.Length;

        for (int i = 0; i < cloudCount; i++) {
            allClouds[i].offset = Vector2.zero;
            allClouds[i].mat = allClouds[i].meshRenderer.material;

        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cloudCount; i++)
        {
            allClouds[i].offset.x += allClouds[i].speed * Time.deltaTime;
            allClouds[i].mat.mainTextureOffset = allClouds[i].offset;

        }
    }
}
