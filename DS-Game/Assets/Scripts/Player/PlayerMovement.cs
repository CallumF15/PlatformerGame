using System;
using System.Reflection;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    #region fields

    public static Vector3 PlayerPosition;
    public int PlayerSpeed;

    #endregion

    #region Getters / Setters

    [Obsolete("this proper is obselete. use this method instead", false)]
    private Vector3 _PlayerPosition
    {
        get
        {
            return gameObject.transform.position;
        }
        set
        {
            PlayerPosition = value;
        }
    }

    private int _PlayerSpeed //add logic
    {
        get
        {
            return playerSpeed;
        }
        set
        {
            playerSpeed = value;
        }
    }

    public int playerSpeed
    {
        get;
        set;
    }

    #endregion

    #region Methods

    void Update()
    {
        PlayerPosition = gameObject.transform.position;
    }

    public void setSpawnLocation()
    {
        GameObject stairPosition = new GameObject();
        stairPosition.GetComponent<TerrainGenerator>();
        

        gameObject.transform.position = PlayerPosition;
    }
    #endregion
}
