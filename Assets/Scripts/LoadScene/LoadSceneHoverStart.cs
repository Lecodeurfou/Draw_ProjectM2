using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneHoverStart : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Start_Scene");
    }

}
