using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGeneration : MonoBehaviour
{
    [Header("Noise")] [SerializeField] private int width = 256;
    [SerializeField] private int height = 256;
    [SerializeField] private float scale;
    [SerializeField] private int offsetX = 100;
    [SerializeField] private int offsetY = 100;
    [Header("Map")] [SerializeField] private int sizeX = 10;
    [SerializeField] private int sizeY = 10;
    [SerializeField] private GameObject block;
    [SerializeField] private int blockSizeX = 2;
    [SerializeField] private int blockSizeY = 2;
    [SerializeField] private float thresholdValue;
    private GameObject environment;
    [Header("Enemies")] [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject[] enemyObj;
    [Range(0f, 100f)] [SerializeField] private float enemyChance = 1.5f;


    private void Start()
    {
        StartCoroutine(GenerateTerrain());
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (environment == null) return;

            GetComponent<MeshFilter>().mesh = new Mesh();
            StartCoroutine(GenerateTerrain());
        }
    }

    private IEnumerator GenerateTerrain()
    {
        offsetX = Random.Range(0, 100000);
        offsetY = Random.Range(0, 100000);

        environment = gameObject; //new GameObject("Environment");

        GenereteSquare();

        CombineMeshes();

        SpawnEnemies();
        yield break;
    }


    private void GenereteSquare()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                var noiseValue = GenereteNoise(x, y);
                if (noiseValue > thresholdValue)
                    Instantiate(block, new Vector3(blockSizeX * x + 0.5f, 0, blockSizeY * y + 0.5f), Quaternion.identity, environment.transform);
            }
        }
    }

    private float GenereteNoise(float x, float y)
    {
        var perlinX = x / sizeX * scale + offsetX;
        var perlinY = y / sizeY * scale + offsetY;

        return Mathf.PerlinNoise(perlinX, perlinY);
    }

    private void CombineMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }

        var meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine);
        GetComponent<MeshCollider>().sharedMesh = meshFilter.mesh;
        gameObject.SetActive(true);

        foreach (Transform tile in environment.transform)
        {
            Destroy(tile.gameObject);
        }
    }

    private void SpawnEnemies()
    {
        var enemies = new GameObject("Enemies");
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                RaycastHit hit;
                Physics.Raycast(new Vector3(blockSizeX * x + 0.5f, 1.7f, blockSizeY * y + 0.5f), Vector3.down, out hit, 0.4f, groundLayer );
                //Debug.DrawRay(new Vector3(2 * x + 0.5f, 1.7f, 2 * y + 0.5f), Vector3.down * 0.4f, Color.red, 60f);
                if (hit.collider == null && Random.Range(0, 100) < enemyChance)
                {
                    Instantiate(enemyObj[Random.Range(0,enemyObj.Length)], new Vector3(blockSizeX * x + 0.5f, 0.5f, blockSizeY * y + 0.5f), Quaternion.identity, enemies.transform);
                }
            }
        }
    }
}