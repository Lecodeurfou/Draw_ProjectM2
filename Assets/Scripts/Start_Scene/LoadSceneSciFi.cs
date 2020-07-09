using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneSciFi : MonoBehaviour
{
    public Collider sciFiPortalCollider;

    void Start() 
    {
        sciFiPortalCollider=GetComponent<Collider>();    
    }
    private void OnTriggerEnter(Collider sciFiPortalCollider)
    {  
        if(sciFiPortalCollider.isTrigger==true)
        {
            SceneManager.LoadScene("SciFiScene");
        }
    }
}
