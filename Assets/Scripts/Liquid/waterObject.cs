using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Linq.Enumerable;

public class waterObject : MonoBehaviour
{

    private GameObject drop;
    public GameObject spawnee;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "waterObj")
        {
            Destroy(other.gameObject);
            cubeToWater(other.gameObject);
        }
    }

    public void cubeToWater(GameObject cube)
    {
        float halfSizex = cube.transform.localScale.x / 2;
        float halfSizey = cube.transform.localScale.y / 2;
        float halfSizez = cube.transform.localScale.z / 2;

        float minx = cube.transform.position.x;
        float maxx = cube.transform.position.x + halfSizex;
        float miny = cube.transform.position.y - halfSizey;
        float maxy = cube.transform.position.y + halfSizey;
        float minz = cube.transform.position.z;
        float maxz = cube.transform.position.z + halfSizez;

        SpawnObject(cube.transform.position);
        foreach (int i in Range(0, 4))
        {
            foreach (int j in Range(0, 4))
            {
                foreach (int k in Range(0, 4))
                {
                    SpawnObject(new Vector3(minx + (spawnee.transform.localScale.x * i), miny + (spawnee.transform.localScale.y * j) + 0.5f, minz + (spawnee.transform.localScale.z * k)));
                }
            }

        }

    }

    public void SpawnObject(Vector3 position)
    {
        drop = Instantiate(spawnee, position, new Quaternion(0,0,0,0));
        drop.GetComponent<Rigidbody>().velocity = new Vector3(1, 1f, 1.5f);
        drop.GetComponent<Rigidbody>().useGravity = true;
        drop.transform.position = position;
    }

}
