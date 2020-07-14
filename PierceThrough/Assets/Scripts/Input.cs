using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input : MonoBehaviour
{

    public Text text;

    private float timeTemp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	    if (UnityEngine.Input.GetMouseButtonDown(0))
	    {
	        text.text = UnityEngine.Input.mousePosition.ToString();
	        timeTemp = 0f;
	    }
	    else
	    {
            timeTemp += Time.deltaTime;
	        text.text = timeTemp.ToString();
	    }
	}
}
