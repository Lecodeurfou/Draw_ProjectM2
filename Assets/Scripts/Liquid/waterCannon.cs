using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class waterCannon : MonoBehaviour
{
    public GameObject spawnee;
    public List<GameObject> dropList;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;
    private GameObject drop;
    private Vector3 pose;
    private Vector3 pose2;
	public GameObject Robinet;
    public int maxObj = 50;
    float speed;
    // Use this for initialization
    void Start ()
    {
        pose = Robinet.transform.position;
        //pose2 = spawnee.transform.position;
        //pose2.z += 2;
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }
    private void Update()
    {
        speed = Mathf.Lerp(0, 20, 0.1f);
        if (dropList.Count > maxObj)
        {
            GameObject rmObj = dropList[0];
            dropList.RemoveAt(0);
            Destroy(rmObj);
        }
        Debug.Log(dropList.Count);
    }

    public void SpawnObject() {
        drop = Instantiate(spawnee, pose, Robinet.transform.rotation);
        drop.GetComponent<Rigidbody>().velocity = new Vector3 (speed, 0, 0);
		drop.GetComponent<Rigidbody>().useGravity = true;
		drop.transform.position = Robinet.transform.position;
        //drop = Instantiate(spawnee, pose2, transform.rotation);
        //drop.GetComponent<Rigidbody>().velocity = new Vector3 (10, 0, 0); 
        if(stopSpawning) {
            CancelInvoke("SpawnObject");
        }


        dropList.Add(drop);
    }
}