using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairGenerator : MonoBehaviour {

    #region Fields
    [Range(1,5)]
    public int totalChunks; //multiplies amount of number of blocks by x amount
    [Range(5,30)]
    public int numberOfBlocks; //number of blocks generated per chunk

    public GameObject cubeObject;

    public int offsetX; //offset for when to generate next block (player position dependent)

    List<Vector3> cubePositionList;
    private int lastChunkPosition;

    #endregion

    #region Getters & Setters
    private int _offsetX //consider adding logic
    {
        get
        {
            return offsetX;
        }
        set
        {       
            offsetX = value;
        }
    }
    #endregion


    // Use this for initialization
    void Start () {
        lastChunkPosition = 0; //starting position for [0] item in chunk
        cubePositionList = new List<Vector3>();
        TerrainGeneration(false);
    }

    // Update is called once per frame
    void Update () {
        if (isPlayerNearEnd() == true) //Check player position before generating next chunk
        {
            GenerateNextChunk();
        }
    }

    /// <summary>
    /// Generates a set number of blocks for player
    /// </summary>
    private void TerrainGeneration(bool elevated)
    {
        //cubeObject.AddComponent<platformMovement>();
        //cubeObject.GetComponent<platformMovement>().speed = 5;
        //cubeObject.GetComponent<platformMovement>().distanceToMove = 5;
        //cubeObject.GetComponent<platformMovement>().delay = 5;

        int position = 0;
        Vector3 cubePosition = cubeObject.transform.position;
        for (int i = lastChunkPosition; i < (numberOfBlocks + lastChunkPosition); i++)
        {
            Vector3 newPosition;
            if (elevated == true)
            {
                newPosition = new Vector3(cubePosition.x + i, cubePosition.y + i, cubePosition.z);
            }
            else
            {
               newPosition = new Vector3(cubePosition.x + i, cubePosition.y, cubePosition.z);
            }

            Instantiate(cubeObject, newPosition, Quaternion.identity);
            cubePositionList.Add(newPosition);

            if (i == ((numberOfBlocks + lastChunkPosition) - 1)) //check last position for storing
                position = (int)cubeObject.transform.position.x + i;
        }
        lastChunkPosition = position;
    }

    /// <summary>
    /// Check if player is near to end, if so returns true else false.
    /// </summary>
    /// <returns></returns>
    private bool isPlayerNearEnd()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 PlayerPosition = PlayerMovement.PlayerPosition;
        int playerspeed = PlayerMovement.PlayerSpeed;

        int arraySize = cubePositionList.Count - 1;
        Vector3 lastObjectPosition = cubePositionList[arraySize];

        if (PlayerPosition.x > (lastObjectPosition.x - offsetX)) // add offset to lastobject position here
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void GenerateNextChunk()
    {
            for (int i = 0; i < totalChunks; i++){
                TerrainGeneration(true);
            }
    }
}
