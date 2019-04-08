using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovement : MonoBehaviour {

    //fields
    public int speed;
    public int distanceToMove;
    public float delay;


    private Vector3 currentPosition;
    private float newForwardPositionZ;
    private float newBackwardPositionZ;
    private bool hasMoved;

    //Getters & Setters

    // Use this for initialization
    void Start () {
        currentPosition = transform.position;
        newForwardPositionZ = transform.position.z + distanceToMove;
        newBackwardPositionZ = transform.position.z - distanceToMove;
        hasMoved = false;
        
    }
	
	// Update is called once per frame
	void Update () {
            StartCoroutine(movementDelay());
    }

    private void MoveForward()
    {
            float step = speed * Time.deltaTime;
            Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + distanceToMove);
            transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
            print("Has Moved: " + hasMoved);

            if(transform.position.z == newForwardPositionZ)
                hasMoved = true;
    }

    private void MoveBackward()
    {
            float step = speed * Time.deltaTime;
            Vector3 newBackPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z - distanceToMove);
            transform.position = Vector3.MoveTowards(transform.position, newBackPosition, step);
            print("Has Moved: " + hasMoved);

        if (transform.position.z == newBackwardPositionZ)
            hasMoved = false;
    }

    IEnumerator movementDelay()
    {
        if (hasMoved == false)
        {    
            yield return new WaitForSeconds(delay);
            MoveForward();
        }
        else
        {
            yield return new WaitForSeconds(delay);
            MoveBackward();      
        }
       
    }

}
