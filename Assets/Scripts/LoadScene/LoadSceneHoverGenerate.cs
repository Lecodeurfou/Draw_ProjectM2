using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHoverGenerate : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Generated_World");
    }
}
