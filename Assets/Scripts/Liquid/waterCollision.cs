using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterCollision : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        //GetComponent<Rigidbody>().drag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("test");
    }
    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "center")
        {
            Destroy(other.gameObject);
            gameObject.transform.position += new Vector3(0, 0.005f, 0);
            gameObject.transform.localScale += new Vector3(0,0.01f,0);
            
        }
    }


    /*private void OnTriggerExit(Collider other)
    {
        GetComponent<Rigidbody>().drag = 10;
    }*/

}
