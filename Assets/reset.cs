using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.DetectionExamples;

public class reset : MonoBehaviour
{    // Start is called before the first frame update
	public GameObject obj;
    public void resetAll()
    {
    	int i = 0;
    	string s = "line" + i;
    	while(GameObject.Find(s) != null){
    		Destroy(GameObject.Find(s).gameObject);
    		s = "line" + i;
    		i++;   
    	} 		
    	obj.GetComponent<PinchDraw>().Line = 0;
    	obj.GetComponent<PinchDraw>().Ytab = new List<int>();
    	obj.GetComponent<PinchDraw>().Ztab = new List<int>();
    }
    // Update is called once per frame
}
