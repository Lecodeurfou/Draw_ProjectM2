using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{

    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve _heightCurve, int levelOfDetail,bool useFlatShading)
    {

        AnimationCurve heightCurve = new AnimationCurve(_heightCurve.keys);

        int meshSimplificationIncrement = (levelOfDetail==0)?1:levelOfDetail*2; //pour ne pas infinite loop plus bas

        int borderedSize = heightMap.GetLength(0); //recuperation de la longueur et de la largeur soit la taille du chunk
        int meshSize = borderedSize -2*meshSimplificationIncrement; //utile pour le calcul des normales C'est la taille reelle du chunk utile borderedSize dépassant sur les triangles suivants
        int meshSizeUnsimplified = borderedSize -2;
        float topLeftX = (meshSizeUnsimplified-1)/(-2f); //pour centrer les vertices car dans le monde on a X    X    X
        float topLeftZ = (meshSizeUnsimplified-1)/(2f); //                                                x=-1  x=0  x=1

    
        int verticesPerLine= (meshSize -1)/meshSimplificationIncrement+1;//nombre de vertice pour chaque ligne en fct du meshSimplificationIncrement
        
        MeshData meshData = new MeshData(verticesPerLine,useFlatShading);
       // int vertexIndex = 0; // index des vetex parcouru pour le bouclage
       int[,] vertexIndicesMap = new int[borderedSize,borderedSize];
       int meshVertexIndex=0; //création des arrays pour le calcul des normals du chunk par le mesh et la bordure.
       int borderVertexIndex=-1;
       
        for (int y = 0 ; y < borderedSize ;y+= meshSimplificationIncrement) //parcours de la Map
        {
            for(int x = 0 ; x < borderedSize ;x+=meshSimplificationIncrement)
            {
                bool isBorderVertex = y ==0 || y == borderedSize-1 || x ==0 || x==borderedSize-1;

                if(isBorderVertex)
                {
                    vertexIndicesMap[x,y] = borderVertexIndex;
                    borderVertexIndex--;
                }
                else
                {
                    vertexIndicesMap[x,y] = meshVertexIndex;
                    meshVertexIndex++;
                }
            }
        }
       
       
       
       
       
       
       
        for (int y = 0 ; y < borderedSize ;y+= meshSimplificationIncrement) //parcours de la Map
        {
            for(int x = 0 ; x < borderedSize ;x+=meshSimplificationIncrement)
            {
                int vertexIndex=vertexIndicesMap[x,y];
                Vector2 percent = new Vector2((x-meshSimplificationIncrement)/(float)meshSize, (y-meshSimplificationIncrement) / (float) meshSize); // percent situationnel a besoin de savoir a quelle valeur il est sous forme de %
                float height = heightCurve.Evaluate(heightMap[x,y])* heightMultiplier;
                Vector3 vertexPosition = new Vector3(topLeftX + percent.x*meshSizeUnsimplified ,height, topLeftZ - percent.y*meshSizeUnsimplified); // avec x , hauteur de la position actuelle, y = qui est la profondeur


                meshData.AddVertex(vertexPosition,percent,vertexIndex);

                if(x < borderedSize -1 && y < borderedSize -1) // ignorer les vertex tout a droite et tout en bas car deja sauv par les autres
                {

                    int a = vertexIndicesMap[x,y];                                                                  //A(x;y)-------------B(x+1;y)         triangle ADC et DAB le schéma en dessous était avec le calcul des normes gérées par unity celui ci c'est avec l'ajout de la bordure dans le chunk de base
                    int b = vertexIndicesMap[x+meshSimplificationIncrement,y];                                      //  |       \           |
                    int c = vertexIndicesMap[x,y+meshSimplificationIncrement];                                      //  |        \          |
                    int d = vertexIndicesMap[x+meshSimplificationIncrement,y+meshSimplificationIncrement];          //C(x;y+i)------------D(x+i;y+i)
                   
                   
                    meshData.AddTriangle(a,d,c); // triangle sous la forme  i------i+1
                    meshData.AddTriangle(d,a,b); //                         |  \    |
                    //                                                    |    \  |
                    //                                                    i+w---i+w+1
                
                }
                vertexIndex++;
            }
        }
    
        meshData.ProcessMesh(); //gain de perf car la fct mere n'est pas sur le même thread

        return meshData;
    
    }
}


public class MeshData
{
    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;
    Vector3[] bakedNormals; // stockage des normales déja créées afin de les threads (opti)

    Vector3[] borderVertices;
    int[] borderTriangles;

    int triangleIndex;
    int borderTriangleIndex;

    bool useFlatShading;

    public MeshData(int verticesPerLine,bool useFlatShading) // constructeur des arrays pour la contenance data de la map
    {
        this.useFlatShading = useFlatShading;
        vertices = new Vector3[verticesPerLine*verticesPerLine]; //tab des data de sommets ex: pour une 3*3 on a 
                                                      // 0  1  2
                                                      // 3  4  5
                                                      // 6  7  8

        uvs = new Vector2[verticesPerLine * verticesPerLine];

        triangles = new int[((verticesPerLine-1)*(verticesPerLine-1))*6]; //tab des data des triangles soit

                                                      // 0 4 3, 4 0 1 pour les deux triangles des 4 premiers sommets. 
    
        borderVertices = new Vector3[verticesPerLine*4+4];
        borderTriangles = new int[24*verticesPerLine];

    }

    public void AddVertex(Vector3 vertexPosition, Vector2 uv, int vertexIndex)
    {
        if(vertexIndex<0)
        {
            borderVertices[-vertexIndex-1]=vertexPosition; 
        }
        else
        {
            vertices[vertexIndex] = vertexPosition;
            uvs[vertexIndex]=uv;
        }
    }

    public void AddTriangle(int a, int b, int c)
    {
        if(a<0||b<0||c<0)
        {
            borderTriangles[borderTriangleIndex]=a; //stockage du premier vertex
            borderTriangles[borderTriangleIndex+1]=b; //stockage du second vertex
            borderTriangles[borderTriangleIndex+2]=c; // stockage du troisième vertex

            borderTriangleIndex+=3; //ajout du prochain triangle dans l'array
        }
        else
        {
            triangles[triangleIndex]=a; //stockage du premier vertex
            triangles[triangleIndex+1]=b; //stockage du second vertex
            triangles[triangleIndex+2]=c; // stockage du troisième vertex

            triangleIndex+=3; //ajout du prochain triangle dans l'array
        }
        
    }

    Vector3[] CalculateNormals()
    {
        Vector3[] vertexNormals = new Vector3 [vertices.Length];
        int triangleCount = triangles.Length / 3;

        for(int i=0 ;i<triangleCount;i++)
        {
            int normalTriangleIndex= i*3;
            int vertexIndexA= triangles[normalTriangleIndex];
            int vertexIndexB= triangles[normalTriangleIndex+1];
            int vertexIndexC= triangles[normalTriangleIndex+2];

            Vector3 trianglesNormal = SurfaceNormalFromIndices(vertexIndexA,vertexIndexB,vertexIndexC);
            vertexNormals[vertexIndexA]+= trianglesNormal;
            vertexNormals[vertexIndexB]+= trianglesNormal;
            vertexNormals[vertexIndexC]+= trianglesNormal;
        }

        int borderTriangleCount = borderTriangles.Length / 3;

        for(int i=0 ;i<borderTriangleCount;i++)
        {
            int normalTriangleIndex= i*3;
            int vertexIndexA= borderTriangles[normalTriangleIndex];
            int vertexIndexB= borderTriangles[normalTriangleIndex+1];
            int vertexIndexC= borderTriangles[normalTriangleIndex+2];

            Vector3 trianglesNormal = SurfaceNormalFromIndices(vertexIndexA,vertexIndexB,vertexIndexC);
            if(vertexIndexA>= 0)
            {
                vertexNormals[vertexIndexA]+= trianglesNormal;
            }
            if(vertexIndexB>=0)
            {
                vertexNormals[vertexIndexB]+= trianglesNormal;
            }
            if(vertexIndexC>=0)
            {
                vertexNormals[vertexIndexC]+= trianglesNormal;
            } 
            
        }

        for(int i=0;i<vertexNormals.Length;i++)
        {
            vertexNormals[i].Normalize();
        }
        return vertexNormals;
    }

    Vector3 SurfaceNormalFromIndices(int indexA,int indexB, int indexC)
    {
        Vector3 pointA = (indexA<0)? borderVertices[-indexA-1] :  vertices[indexA];
        Vector3 pointB = (indexB<0)? borderVertices[-indexB-1] : vertices[indexB];
        Vector3 pointC = (indexC<0)? borderVertices[-indexC-1] : vertices[indexC];

        Vector3 sideAB = pointB-pointA;
        Vector3 sideAC = pointC-pointA;

        return Vector3.Cross(sideAB, sideAC).normalized; 
    }

    public void ProcessMesh()
    {
        if(useFlatShading)
        {
            FlatShading();
        }
        else
        {
            BakeNormals(); 
        }
    }

    private void BakeNormals()
    {
        bakedNormals = CalculateNormals();
    }

    public void FlatShading()
    {
        Vector3[] flatShadedVertices = new Vector3[triangles.Length];
        Vector2[] flatShadedUvs = new Vector2[triangles.Length];
        for (int i = 0; i < triangles.Length; i++)
        {
            flatShadedVertices[i]=vertices[triangles[i]];
            flatShadedUvs[i]=uvs[triangles[i]];
            triangles[i]=i;
        }
        vertices = flatShadedVertices;
        uvs=flatShadedUvs;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices=vertices;
        mesh.triangles=triangles;
        mesh.uv = uvs;
       // mesh.RecalculateNormals(); //recalcul des normals pour la luminosité fait par unity 
                                    //la notre prends en compte les triangles adjacent par un recalcul des normals au vertex
                                    //ce qui empeche un cassure visuelle entre les chunks
        
        if(useFlatShading)
        {
            mesh.RecalculateNormals();
        }
        else
        {
            mesh.normals = bakedNormals;
        }
        
        return mesh;
    }
}