using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColor : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        RandomiseColor();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void RandomiseColor()
    {
        Color background2 = new Color(
    Random.Range(0f, 1f),
    Random.Range(0f, 1f),
    Random.Range(0f, 1f));

        GetComponent<Renderer>().material.color = background2;
    }
}
