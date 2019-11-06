/*
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class load : MonoBehaviour
{
    private GameObject mangerScript;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    public void Load()
    {
        player.transform.position = ES2.Load<Vector3>("playerPosition");

        ManagerScript script = managerScript.GetComponent<ManagerScript>();
        script.ammo = ES2.Load<int>("magazines");
        script.magazines = ES2.Load<int>("magazines");
        script.brokenArm = ES2.Load<bool>("brokenArm");
    }
}
*/
