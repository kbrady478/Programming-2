using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Sphere_Generator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Material sphere_Material;
    [SerializeField] private float sphere_Size = 1f;
    private Mesh sphere_Mesh;

    [Header("Noise Values")] 
    [SerializeField] private float noise_Scale = 1f;
    [SerializeField] private float noise_Speed;
    [SerializeField] private float noise_Height;
    
    [Header("Poly Arrays")]
    private Vector3[] start_Sphere_Verts;
    private Vector3[] new_Sphere_Verts;
    private int[] sphere_Tris;
    
    [Header("Components to add")]
    private MeshRenderer sphere_Mesh_Renderer;
    private MeshFilter sphere_Mesh_Filter;
    private MeshCollider sphere_Mesh_Collider;


    #region --- Unity Functions ---
    
    private void Start()
    {
        Create_Sphere_Object();
    }// end Start()

    private void Update()
    {
        Calculate_Wave();
        Recalculate_Mesh();
    }// end Update()

    #endregion
    
    #region --- Generate Mesh ---
    private void Create_Sphere()
    {
        Create_Sphere_Object();
        
    }// Create_Sphere

    private void Create_Sphere_Object()
    {
        // Set components
        sphere_Mesh_Filter = gameObject.AddComponent<MeshFilter>();
        sphere_Mesh = sphere_Mesh_Filter.mesh;
        sphere_Mesh_Renderer = gameObject.AddComponent<MeshRenderer>();
        sphere_Mesh_Renderer.material = sphere_Material;
        gameObject.transform.localScale = new Vector3(sphere_Size, sphere_Size, sphere_Size);
        
        Calculate_IcoSphere.Calculate_Mesh(gameObject);
        
        start_Sphere_Verts = sphere_Mesh_Filter.mesh.vertices;
        
        Recalculate_Mesh();
    }// end Create_Sphere_Object()

    #endregion

    void Calculate_Wave()
    {
        start_Sphere_Verts = sphere_Mesh_Filter.mesh.vertices;
        
        for (int i = 0; i < start_Sphere_Verts.Length; i++)
        {
            /* float point_X = (sphere_Verts[i].x * noise_Scale) + (Time.deltaTime * noise_Speed);
            float point_Y = (sphere_Verts[i].y * noise_Scale) + (Time.deltaTime * noise_Speed);
            float point_Z = (sphere_Verts[i].z * noise_Scale) + (Time.deltaTime * noise_Speed);
            
            sphere_Verts[i].x = Mathf.PerlinNoise(point_X, point_Y) * noise_Height;
            sphere_Verts[i].y = Mathf.PerlinNoise(point_Y, point_Z) * noise_Height;
            sphere_Verts[i].z = Mathf.PerlinNoise(point_Z, point_X) * noise_Height;
        
            */
            
            new_Sphere_Verts = new Vector3[start_Sphere_Verts.Length];
            
            float timex = Time.time * noise_Speed + 2.5564f;
            float timey = Time.time * noise_Speed + 1.21688f;
            float timez = Time.time * noise_Speed + 0.1365143f;
            
            for (int j = 0; j < new_Sphere_Verts.Length; j++) 
            {
                Vector3 vertex = start_Sphere_Verts [j];
                vertex.x += Mathf.PerlinNoise (timex + vertex.x, timex + vertex.y) * noise_Scale;
                vertex.y += Mathf.PerlinNoise (timey + vertex.x, timey + vertex.y) * noise_Scale;
                vertex.z += Mathf.PerlinNoise (timez + vertex.x, timez + vertex.y) * noise_Scale;
                new_Sphere_Verts[j] = vertex;
            }
        }
        
        sphere_Mesh_Filter.mesh.vertices = start_Sphere_Verts;
        
    }// end Calculate_Wave()
    
    void Recalculate_Mesh()
    {
        sphere_Mesh.RecalculateBounds();
        sphere_Mesh.RecalculateTangents();
        sphere_Mesh.RecalculateNormals();
    }// end Recalculate_Mesh()
    
    
}// end Sphere_Generator
