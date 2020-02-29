using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGenerator : MonoBehaviour
{
    [Header("Noise")] [SerializeField] private int width = 256;
    [SerializeField] private int height = 256;
    [SerializeField] private float scale;
    [SerializeField] private int offsetX = 100;
    [SerializeField] private int offsetY = 100;
    [Header("Map")]
    [SerializeField] private GameObject block;
    [SerializeField] private Transform environment;


    private void Start()
    {
        offsetX = Random.Range(0, 100000);
        offsetY = Random.Range(0, 100000);
        StartCoroutine(LoadTexture());
    }

    private IEnumerator LoadTexture()
    {
        GenereteSquare();

        yield break;
    }

    private void GenereteSquare()
    {
        for (int i = 0; i < 360; i += 5)
        {
            var locationVector = Quaternion.Euler(0, i, 0) * Vector3.forward * 10;
            
            var noiseValue = GenereteNoise(locationVector.x,locationVector.z);
            
            Instantiate(block, locationVector * noiseValue , Quaternion.identity, environment);

        }
            
        
    }

    private float GenereteNoise(float x, float y)
    {
        var perlinX =  x / width * scale + offsetX;
        var perlinY =  y / height * scale + offsetY;

        return Mathf.PerlinNoise(perlinX, perlinY);
    }
    
}