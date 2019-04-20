using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;

    public float cameraRepositionSpeed = 0.5f;

    void Start ()
    {
	}
	
	void Update ()
    {
        //this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
        Vector3 targetMoveVector = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
        this.transform.position = Vector3.Lerp(this.transform.position, targetMoveVector, cameraRepositionSpeed);
	}
}
