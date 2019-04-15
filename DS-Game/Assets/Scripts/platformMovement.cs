using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovement : MonoBehaviour
{

    //fields
    public int speed;
    public int distanceToMove;
    public float delay;


    private Vector3 currentPosition;
    private float newForwardPositionZ;
    private float newBackwardPositionZ;
    private bool hasMoved;
    private float step;
    private int count;

    // Use this for initialization
    void Start()
    {
        RandomStartDirecton();
        count = 1;

        currentPosition = transform.position;
        newForwardPositionZ = transform.position.z + distanceToMove;
        newBackwardPositionZ = transform.position.z - distanceToMove;
        hasMoved = false;

        step = speed * Time.deltaTime; //since this is here, speed wouldn't be changeable at runtime
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(movementDelay());
    }

    private void MoveForward()
    {
        float pos = currentPosition.z + distanceToMove;
        Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y, pos);
        transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
        //print("Has Moved: " + hasMoved);

        if (transform.position.z == newForwardPositionZ)
        {
            count = 0;
            hasMoved = true;
        }
    }

    private void MoveBackward()
    {
        float pos = currentPosition.z - distanceToMove;
        Vector3 newBackPosition = new Vector3(currentPosition.x, currentPosition.y, pos);
        transform.position = Vector3.MoveTowards(transform.position, newBackPosition, step);
        //print("Has Moved: " + hasMoved);

        if (transform.position.z == newBackwardPositionZ)
            hasMoved = false;
    }

    /// <summary>
    /// Determines whether blocks move left or right at start
    /// </summary>
    public void RandomStartDirecton()
    {
        bool Boolean = (Random.value > 0.5f); //generate random bool value
        if (Boolean == true)
            distanceToMove *= -1;
        else
            distanceToMove *= 1;
    }

    IEnumerator movementDelay()
    {
        if (hasMoved == false)
        {
            if (count == 0)
            {
                yield return new WaitForSeconds(delay);
            }
            MoveForward();
        }
        else
        {
            yield return new WaitForSeconds(delay);
            MoveBackward();
        }
    }

}
