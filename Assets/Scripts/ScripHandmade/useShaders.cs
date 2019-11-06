using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useShaders : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    
    public void shaGeo(Collider col)
    {
        GameObject c = GameObject.Find("Cube");
        Material material = new Material(Shader.Find("Hand_Make/GeowireShader"));
        material.color = Color.green;
        c.GetComponent<Renderer>().material = material;
    }
}