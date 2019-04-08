using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    #region Fields
    [Range(1, 5)]
    public int totalChunks; //multiplies amount of number of blocks by x amount
    [Range(5, 30)]
    public int numberOfBlocks; //number of blocks generated per chunk
    private int totalBlocks;

    public GameObject cubeObject;

    //public GameObject _cubeObject //consider adding logic
    //{
    //    get
    //    {
    //        return cubeObject;
    //    }
    //    set
    //    {
    //        cubeObject = value;
    //    }
    //}


    public int offsetX; //offset for when to generate next block (player position dependent)

    List<GameObject> cubeObjectList;
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


    private int _TotalBlocks
    {
        get
        {
            totalBlocks = numberOfBlocks * totalChunks;
            return totalBlocks;
        }
    }

    #endregion


    // Use this for initialization
    void Start()
    {
        lastChunkPosition = 0; //starting position for [0] item in chunk
        cubeObjectList = new List<GameObject>();
        GenerateNextChunk(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearEnd() == true) //Check player position before generating next chunk
        {
            GenerateNextChunk(false);
        }
    }

    /// <summary>
    /// Generates a set number of blocks for player
    /// </summary>
    private void GenerateNextChunk(bool elevated)
    {
        int position = 0;
        Vector3 cubePosition = cubeObject.transform.position;
   
        print(cubePosition.x);
        for (int i = lastChunkPosition; i < (_TotalBlocks + lastChunkPosition); i++)
        {
            Vector3 newPosition;
            if (elevated == true)
            {
                newPosition = new Vector3(cubePosition.x + i, cubePosition.y + (i - _TotalBlocks), cubePosition.z);
            }
            else
            {

                newPosition = new Vector3(cubePosition.x + i, cubePosition.y, cubePosition.z);
            }

            cubeObjectList.Add(Instantiate(cubeObject, newPosition, Quaternion.identity));
            // if (i == lastChunkPosition + 1)
            // {
            cubeObject.RemoveComponent<platformMovement>();
            // }
            //else
            //{
            cubeObject.GetComponent<platformMovement>().speed = Random.Range(1, 5);
            cubeObject.GetComponent<platformMovement>().distanceToMove = 5;
            cubeObject.GetComponent<platformMovement>().delay = Random.Range(1, 5);
            //}

            if (i == ((_TotalBlocks + lastChunkPosition) - 1)) //check last position for storing
                position = (int)cubeObject.transform.position.x + i;
        }
        lastChunkPosition = position + 1;
    }

    private void StairGeneration()
    {

    }

    /// <summary>
    /// Check if player is near to end, if so returns true else false.
    /// </summary>
    /// <returns></returns>
    private bool isPlayerNearEnd()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 PlayerPosition = PlayerMovement.PlayerPosition;
        //int playerspeed = PlayerMovement.PlayerSpeed;

        int arraySize = cubeObjectList.Count - 1;
        Vector3 lastObjectPosition = cubeObjectList[arraySize].transform.position;

        if (PlayerPosition.x > (lastObjectPosition.x - offsetX)) // add offset to lastobject position here
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Deletes the previously generated chunk of blocks
    /// </summary>
    public void DeletePreviousChunk()
    {
        Destroy(cubeObjectList[0]);
        cubeObjectList.RemoveAt(0);

        //for (int i = 0; i < _TotalBlocks; i++)
        //{
        //    Destroy(cubeObjectList[i]);
        //}
        //cubeObjectList.RemoveRange(0, _TotalBlocks);
    }
}

public static class ExtensionMethods
{
    public static void RemoveComponent<Component>(this GameObject obj, bool immediate = false)
    {
        Component component = obj.GetComponent<Component>();

        if (component != null)
        {
            if (immediate)
            {
                Object.DestroyImmediate(component as Object, true);
            }
            else
            {
                Object.Destroy(component as Object);
            }

        }
    }
}