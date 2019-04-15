using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public float speed;

    private TerrainGenerator m_terraingeneration;

	// Use this for initialization
	void Start () {
        m_terraingeneration = GameObject.FindObjectOfType(typeof(TerrainGenerator)) as TerrainGenerator;
    }
	
	// Update is called once per frame
	void Update () {
     
        if (Input.GetKeyDown("space"))
        {
            int b = m_terraingeneration._LastCPosition;
            Vector3 platformPosition = m_terraingeneration.GetFirstPlatformPosition();
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(platformPosition.x, platformPosition.y + 1, 0);
            //GameObject.FindGameObjectWithTag("Player").transform.position += new Vector3(1, 0, 0);
        }
    }
}
