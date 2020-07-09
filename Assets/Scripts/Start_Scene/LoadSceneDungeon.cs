using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneDungeon : MonoBehaviour
{
    public Collider dungeonDoorCollider;

    void Start() 
    {
        dungeonDoorCollider=GetComponent<Collider>();    
    }
    private void OnTriggerEnter(Collider dungeonDoorCollider)
    {  
        if(dungeonDoorCollider.isTrigger==true)
        {
            SceneManager.LoadScene("PinchDrawDemo");
        }
    }
}
