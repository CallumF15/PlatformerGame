using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float offsetX = 30;
    public float offsetY = 0;

	// Update is called once per frame
	void Update () {
        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<Camera>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        var playerPos = player.transform.position;
        float playerPositionX = playerPos.x;
        float playerPositionY = playerPos.y;

        Vector3 cameraPos = new Vector3(playerPositionX - offsetX, playerPositionY + offsetY, 0);
        camera.transform.position = cameraPos;
    }
}
