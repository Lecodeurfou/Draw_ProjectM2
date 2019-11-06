using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activerdessin : MonoBehaviour
{

    public GameObject PinchDrawing;
    // Start is called before the first frame update
    void SetDrawingOn()
    {
        PinchDrawing.SetActive(false);
    }


}
