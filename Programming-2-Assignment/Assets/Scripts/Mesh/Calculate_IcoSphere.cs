using System.Collections.Generic;
using UnityEngine;

public static class Calculate_IcoSphere
{
    private struct Triangle_Indices
    {
        public int v1;
        public int v2;
        public int v3;

        public Triangle_Indices(int v1, int v2, int v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
    }// end Triangle_Indices()
    
    public static void Calculate_Mesh(GameObject gameObject)
    {
        MeshFilter filter = gameObject.GetComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        mesh.Clear();
        Vector3[] vertices = gameObject.GetComponent<MeshFilter>().mesh.vertices;
        List<Vector3> vertList = new List<Vector3>();
        Dictionary<long, int> middlePointIndexCache = new Dictionary<long, int>();
        int index = 0;

        int recursionLevel = 3;
        float radius = 1f;

        // Create 12 vertices of an icosahedron
        float t = (1f + Mathf.Sqrt(5f)) / 2f;

        #region -- Placing Faces --
        
        vertList.Add(new Vector3(-1f, t, 0f).normalized * radius);
        vertList.Add(new Vector3(1f, t, 0f).normalized * radius);
        vertList.Add(new Vector3(-1f, -t, 0f).normalized * radius);
        vertList.Add(new Vector3(1f, -t, 0f).normalized * radius);

        vertList.Add(new Vector3(0f, -1f, t).normalized * radius);
        vertList.Add(new Vector3(0f, 1f, t).normalized * radius);
        vertList.Add(new Vector3(0f, -1f, -t).normalized * radius);
        vertList.Add(new Vector3(0f, 1f, -t).normalized * radius);

        vertList.Add(new Vector3(t, 0f, -1f).normalized * radius);
        vertList.Add(new Vector3(t, 0f, 1f).normalized * radius);
        vertList.Add(new Vector3(-t, 0f, -1f).normalized * radius);
        vertList.Add(new Vector3(-t, 0f, 1f).normalized * radius);


        // Create 20 triangles of the icosahedron
        List<Triangle_Indices> faces = new List<Triangle_Indices>();

        // 5 faces around point 0
        faces.Add(new Triangle_Indices(0, 11, 5));
        faces.Add(new Triangle_Indices(0, 5, 1));
        faces.Add(new Triangle_Indices(0, 1, 7));
        faces.Add(new Triangle_Indices(0, 7, 10));
        faces.Add(new Triangle_Indices(0, 10, 11));

        // 5 adjacent faces 
        faces.Add(new Triangle_Indices(1, 5, 9));
        faces.Add(new Triangle_Indices(5, 11, 4));
        faces.Add(new Triangle_Indices(11, 10, 2));
        faces.Add(new Triangle_Indices(10, 7, 6));
        faces.Add(new Triangle_Indices(7, 1, 8));

        // 5 faces around point 3
        faces.Add(new Triangle_Indices(3, 9, 4));
        faces.Add(new Triangle_Indices(3, 4, 2));
        faces.Add(new Triangle_Indices(3, 2, 6));
        faces.Add(new Triangle_Indices(3, 6, 8));
        faces.Add(new Triangle_Indices(3, 8, 9));

        // 5 adjacent faces 
        faces.Add(new Triangle_Indices(4, 9, 5));
        faces.Add(new Triangle_Indices(2, 4, 11));
        faces.Add(new Triangle_Indices(6, 2, 10));
        faces.Add(new Triangle_Indices(8, 6, 7));
        faces.Add(new Triangle_Indices(9, 8, 1));

        #endregion

        // Subdivide faces
        for (int i = 0; i < recursionLevel; i++)
        {
            List<Triangle_Indices> faces2 = new List<Triangle_Indices>();
            foreach (var tri in faces)
            {
                // replace triangle by 4 triangles
                int a = Find_Middle_Point(tri.v1, tri.v2, ref vertList, ref middlePointIndexCache, radius);
                int b = Find_Middle_Point(tri.v2, tri.v3, ref vertList, ref middlePointIndexCache, radius);
                int c = Find_Middle_Point(tri.v3, tri.v1, ref vertList, ref middlePointIndexCache, radius);

                faces2.Add(new Triangle_Indices(tri.v1, a, c));
                faces2.Add(new Triangle_Indices(tri.v2, b, a));
                faces2.Add(new Triangle_Indices(tri.v3, c, b));
                faces2.Add(new Triangle_Indices(a, b, c));
            }
            faces = faces2;
        }// end Subdivision

        mesh.vertices = vertList.ToArray();

        List<int> triList = new List<int>();
        for (int i = 0; i < faces.Count; i++)
        {
            triList.Add(faces[i].v1);
            triList.Add(faces[i].v2);
            triList.Add(faces[i].v3);
        }
        mesh.triangles = triList.ToArray();
        mesh.uv = new Vector2[vertices.Length];

        Vector3[] normales = new Vector3[vertList.Count];
        for (int i = 0; i < normales.Length; i++)
            normales[i] = vertList[i].normalized;


        mesh.normals = normales;

        mesh.RecalculateBounds();
        mesh.RecalculateTangents();
        mesh.RecalculateNormals();
        
    }// end Calculate_Mesh()

    // Return index of center point of p1 and p2
    private static int Find_Middle_Point(int p1, int p2, ref List<Vector3> vertices, ref Dictionary<long, int> cache, float radius)
    {
        // First check its already in cache
        bool first_Is_Smaller = p1 < p2;
        long smaller_Index = first_Is_Smaller ? p1 : p2;
        long greater_Index = first_Is_Smaller ? p2 : p1;
        long key = (smaller_Index << 32) + greater_Index;

        int ret;
        if (cache.TryGetValue(key, out ret))
        {
            return ret;
        }

        // Not in cache, calculate it
        Vector3 point1 = vertices[p1];
        Vector3 point2 = vertices[p2];
        Vector3 middle = new Vector3
        (
            (point1.x + point2.x) / 2f,
            (point1.y + point2.y) / 2f,
            (point1.z + point2.z) / 2f
        );

        // Add vertex makes sure point is on unit sphere
        int i = vertices.Count;
        vertices.Add(middle.normalized * radius);

        // Store it, return index
        cache.Add(key, i);

        return i;
    }// end Find_Middle_Point()
    
}// end Calculate_IcoSphere
