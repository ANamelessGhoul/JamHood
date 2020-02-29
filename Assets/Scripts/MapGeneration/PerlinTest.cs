using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinTest : MonoBehaviour
{
    [SerializeField] private int width = 256;
    [SerializeField] private int height = 256;
    [SerializeField] private float scale;
    [SerializeField] private int offsetX = 100;
    [SerializeField] private int offsetY = 100;
    private void Start()
    {
        StartCoroutine(LoadTexture());
    }

    private IEnumerator LoadTexture()
    {
        var render = GetComponent<Renderer>();

        render.material.mainTexture = GenereteTexture();
        
        yield break;
    }
    
    private Texture2D GenereteTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                Color color = GenerateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        
        texture.Apply();

        return texture;
    }

    private Color GenerateColor(int x, int y)
    {
        var perlinX = (float) x / width * scale + offsetX;
        var perlinY = (float) y / height * scale + offsetY;

        var colorSample = Mathf.PerlinNoise(perlinX, perlinY);
        var color = new Color(colorSample,colorSample,colorSample);

        return color;
    }
}
