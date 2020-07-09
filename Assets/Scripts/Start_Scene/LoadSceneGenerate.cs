using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneGenerate : MonoBehaviour
{
    public Collider starFishCollider;

    void Start() 
    {
        starFishCollider=GetComponent<Collider>();    
    }
    private void OnTriggerEnter(Collider starFishCollider)
    {  
        if(starFishCollider.isTrigger==true)
        {
            SceneManager.LoadScene("Generated_World");
        }
    }
}
