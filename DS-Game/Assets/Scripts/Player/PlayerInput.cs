using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            GameObject.FindGameObjectWithTag("Player").transform.position += new Vector3(5, 0, 0);
        }
        else
        {

        }
        //]        switch(Input.inputString){
        //    case "space":
        //        {

        //        }
        //}
    }
}
