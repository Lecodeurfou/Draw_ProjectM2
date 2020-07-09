using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneForest : MonoBehaviour
{
    public Collider magicPortalCollider;

    void Start() 
    {
        magicPortalCollider=GetComponent<Collider>();    
    }
    private void OnTriggerEnter(Collider magicPortalCollider)
    {  
        if(magicPortalCollider.isTrigger==true)
        {
            SceneManager.LoadScene("Forest_Scene");
        }
    }
}
