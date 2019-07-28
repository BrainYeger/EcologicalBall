using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWater : MonoBehaviour
{
    public Color Color1;
    public Color Color2;
    private MeshRenderer mr = null;
    private MeshFilter mf = null;
    private Mesh mesh = null;
    private List<Vector3> point = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mf = GetComponent<MeshFilter>();
        mesh = mf.mesh;
        
        for(int i = 0; i < mesh.vertexCount; i++)
        {

            point.Add(transform.TransformPoint(mesh.vertices[i]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        List<Color> lc = new List<Color>();
        foreach(Vector3 p in point)
        {
            double c;
            c = GameManager.Instance.GetH2O(p);
            lc.Add(Color.Lerp(Color1,Color2,(float)c/255));
        }
        mesh.SetColors(lc);
    }
}
