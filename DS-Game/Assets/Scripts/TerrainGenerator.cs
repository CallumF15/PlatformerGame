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
    public GameObject cubeObject;
    public int offsetX; //offset for when to generate next block (player position dependent)
    public List<GameObject> cubeObjectList;
    private int totalBlocks;
    private int StartChunkPosition;
    private int LastYPosition;

    #endregion

    #region Getters & Setters

    public int _LastCPosition
    {
        get
        {
            return StartChunkPosition;
        }
    }

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
        StartChunkPosition = 0; //starting position for [0] item in chunk
        cubeObjectList = new List<GameObject>();
        LastYPosition = 0;

        //bool isElevated = (Random.value > 0.5f); //generate random bool value (determines if stairs go up or down)
        GenerateNextChunk(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearEnd() == true)
        { //Check player position before generating next chunk
          //bool isElevated = (Random.value > 0.5f); //generate random bool value (determines if stairs go up or down)
            bool isElevated = true;
            GenerateNextChunk(isElevated);
        }

        test();
    }

    /// <summary>
    /// Generates a set number of blocks for player attached with movement script
    /// </summary>
    private void GenerateNextChunk(bool elevated)
    {
        Vector3 cubePosition = cubeObject.transform.position;

        bool isStairsUp = (Random.value > 0.5f); //generate random bool value (determines if stairs go up or down)
        int NextChunkPosition = _TotalBlocks + StartChunkPosition; //calc next chunk position

        for (int i = StartChunkPosition; i < NextChunkPosition; i++)
        {
            Vector3 newPosition;
            if (elevated == true)
                if (isStairsUp == true)
                {
                    newPosition = new Vector3(cubePosition.x + i, (cubePosition.y + LastYPosition) + (i - StartChunkPosition), cubePosition.z);
                    if (i == NextChunkPosition - 1)
                        LastYPosition = (int)cubePosition.y + (i - StartChunkPosition);
                }
                else {           
                    newPosition = new Vector3(cubePosition.x + i, (cubePosition.y + LastYPosition) - (i - StartChunkPosition), cubePosition.z);
                    if (i == NextChunkPosition - 1)
                        LastYPosition = (int)cubePosition.y - (i - StartChunkPosition);
                }
            else
                newPosition = new Vector3(cubePosition.x + i, cubePosition.y + LastYPosition, cubePosition.z);




            var cube = Instantiate(cubeObject, newPosition, Quaternion.identity);
            cubeObjectList.Add(cube);

            if (i == 0) //remove starting platform movement script
                cube.RemoveComponent<platformMovement>();
            else
            {
                cube.GetComponent<platformMovement>().speed = Random.Range(0, 5);
                cube.GetComponent<platformMovement>().distanceToMove = 5;
                cube.GetComponent<platformMovement>().delay = Random.Range(0, 5);
            }

            if (i == (NextChunkPosition - 1)) //when i is at last position
                StartChunkPosition = NextChunkPosition; //assign new start position
        }
    }

    /// <summary>
    /// Check if player is near to end, if so returns true else false.
    /// </summary>
    /// <returns></returns>
    private bool isPlayerNearEnd()
    {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 PlayerPosition = PlayerMovement.PlayerPosition;

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

    private void test()
    {
        Vector3 PlayerPosition = PlayerMovement.PlayerPosition;

        if (PlayerPosition.x > cubeObjectList[0].transform.position.x)
        {
            RemoveScript(1);
            DeletePreviousChunk(0);
        }

        if (cubeObjectList[0].transform.position.z != 0)
        {
            cubeObjectList[0].transform.position = new Vector3(cubeObjectList[0].transform.position.x, cubeObjectList[0].transform.position.y, 0);
        }
    }

    /// <summary>
    /// Deletes the previously generated chunk of blocks
    /// </summary>
    public void DeletePreviousChunk(int index)
    {
        Destroy(cubeObjectList[index]);
        cubeObjectList.RemoveAt(index);
    }

    public Vector3 GetFirstPlatformPosition()
    {
        return cubeObjectList[1].transform.position;
    }

    public void RemoveScript(int index)
    {
        Destroy(cubeObjectList[index].GetComponent<platformMovement>());
        //cubeObjectList[index].RemoveComponent<platformMovement>();
        print("cube position after: " + cubeObjectList[index].transform.position);
    }
}

public static class ExtensionMethods
{
    public static void RemoveComponent<Component>(this GameObject obj)
    {
        Component component = obj.GetComponent<Component>();

        if (component != null)
        {

            Object.Destroy(component as Object);
        }
    }
}