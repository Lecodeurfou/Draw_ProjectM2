using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "colorPicker"){
            col.transform.position = GameObject.Find("CursorRenderers").transform.position;
        }

    }
}
