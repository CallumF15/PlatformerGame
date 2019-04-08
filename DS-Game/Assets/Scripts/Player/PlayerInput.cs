using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public float speed;
    int keyPressCount = 0;

    private TerrainGenerator m_terraingeneration;


	// Use this for initialization
	void Start () {
        m_terraingeneration = GameObject.FindObjectOfType(typeof(TerrainGenerator)) as TerrainGenerator;
    }
	
	// Update is called once per frame
	void Update () {
     
        if (Input.GetKeyDown("space"))
        {
            //GameObject.FindGameObjectWithTag("Player").transform.position += transform.right * Time.deltaTime * PlayerMovement.PlayerSpeed;
            //GameObject player = GameObject.FindGameObjectWithTag("Player");
            //int speed = player.GetComponent<PlayerMovement>().playerSpeed;

            //GameObject.FindGameObjectWithTag("Player").transform.position += transform.right * Time.deltaTime * speed; ;
         
            GameObject.FindGameObjectWithTag("Player").transform.position += new Vector3(1, 0, 0);

            keyPressCount++;
            if (keyPressCount == 2)
            {
                m_terraingeneration.DeletePreviousChunk();
                keyPressCount = 1;
            }      
        }
        else
        {

        }
    }
}
