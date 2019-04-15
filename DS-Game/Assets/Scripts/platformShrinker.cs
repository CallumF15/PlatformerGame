using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformShrinker : MonoBehaviour {

    public float scaleSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ShrinkGameObject();
    }

    /// <summary>
    /// Adjusts objects scaling by increasing and decreasing size by desired speed.
    /// </summary>
    void ShrinkGameObject()
    {
        gameObject.transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.deltaTime; //speed of scaling

        //print("ScaleX: " + gameObject.transform.localScale.x);

        var objectScaleX = gameObject.transform.localScale.x;
        if (objectScaleX <= 0.01f || objectScaleX >= 0.99) //reached certain size
        {
            scaleSpeed *= -1; //invert scaling
            //Destroy(gameObject);
        }
    
    }
}
