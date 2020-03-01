using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ProceduralMesh : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uv;
    [SerializeField] private MeshRenderer meshR;
    [SerializeField] private Vector3 vec1 = new Vector3(0, 0, 0);
    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeMashData();
        CreateMesh();
    }

    void MakeMashData()
    {
        vertices = new Vector3[]{ vec1, new Vector3(0,0,1), new Vector3(1,0,0), new Vector3(1,0,1) };
        triangles = new int[] {0, 1, 2, 2, 1, 3};
        uv = new Vector2[]{ new Vector2(0,0), new Vector2(1,0), new Vector2(0,1), new Vector2(1, 1) };
    }

    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
    }
}
