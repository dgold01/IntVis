using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]



public class MeshGenerator : MonoBehaviour
{
    
    
    Mesh mesh;

    Vector3[] vertices;
   
    int[] triangles;

    public int xSize = 1;
    public string _ColourName = "_EmissionColor";
    public int ySize = 1;
    public int _FFTIndex = 0;
    public AudioSpectrum _FFT;
    float FreqValue;
    
    
    public float Scaling_Strength;
    public float _StrengthScaler = 3;
   
    
    public float Colour_Strength;
   

    public int i;
    private void Start()
    {
        _FFT = GameObject.FindWithTag("Audio").GetComponent<AudioSpectrum>();
        mesh = new Mesh();
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
      
        CreateShape();
        
    }
    private void Update()
    {   
       
        UpdateMesh();
        CreateShape();

    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize+1)*(ySize+1)];
        Vector2[] uvs = new Vector2[vertices.Length];
       

        for (int i=0,p=0,y = 0; y <= ySize; y++)
        {   
            for (int x = 0; x <= xSize; x++)
            {
                FreqValue = _FFT._amplitude;
                float z = Mathf.PerlinNoise(x*.3f, y*.3f) * 2f * FreqValue*Scaling_Strength; 
                vertices[i] = new Vector3(x, y, z);
                
                i++;
                p++;
                if (p==63)
                {
                    p = 0;
                }

            }
        }
        int vert = 0;
        int tris = 0;
        triangles = new int[xSize*ySize*6];
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[0 + tris] = 0 + vert;
                triangles[1 + tris] = xSize + 1 + vert;
                triangles[2 + tris] = 1 + vert;
                triangles[3 + tris] = xSize + 1 + vert;
                triangles[4 + tris] = xSize + 2 + vert;
                triangles[5 + tris] = 1 + vert;
                vert++;
                tris += 6;

                
            }

            vert++;

        }

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].y);

        }
        
      mesh.uv = uvs;
        


    }



    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();



    }
 

    


}
