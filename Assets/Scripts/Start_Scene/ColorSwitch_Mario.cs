using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitch_Mario : MonoBehaviour
{

    private int nextUpdate=1;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend=GetComponent<Renderer>();
       // InvokeRepeating("UpdateEverySecond", 0, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
             if(Time.time>=nextUpdate){
             //Debug.Log(Time.time+">="+nextUpdate);
             // Change the next update (current second+1)
             nextUpdate=Mathf.FloorToInt(Time.time)+2;
             // Call your fonction
             UpdateEverySecond();
         }
    }


      void UpdateEverySecond(){
     
          rend.material.SetColor("_Color",Random.ColorHSV());
     
     }
}
