using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

   // public GameObject magicPortal;
    public Collider magicPortalCollider;
    public Collider dungeonDoorCollider;
    public Collider sciFiPortalCollider;
    public Collider starFishCollider;

    void Start()
    {
       // magicPortal = GameObject.Find("magic portal");
        magicPortalCollider=GetComponent<Collider>();    
        dungeonDoorCollider=GetComponent<Collider>();
        sciFiPortalCollider=GetComponent<Collider>();
        starFishCollider=GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(magicPortalCollider.isTrigger==true)
        {
            SceneManager.LoadScene("Forest_Scene");
        }
        if(dungeonDoorCollider.isTrigger==true)
        {
            SceneManager.LoadScene("PinchDrawDemo");
        }
       /* if(sciFiPortalCollider.isTrigger==true)
        {
            SceneManager.LoadScene("Forest_Scene");
        }*/
        if(starFishCollider.isTrigger==true)
        {
            SceneManager.LoadScene("Generated_World");
        }
            
    }

}
