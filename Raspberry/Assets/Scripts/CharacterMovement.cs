using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public Vector3 screenPos;
    public Vector3 movePos;
    public Vector3 finalMovePos;

    private bool moveFlag = false;

    public float moveSpeed;
    public float yPositionFixed;


    void Update ()
    {
        movePos = Camera.main.ScreenToWorldPoint(screenPos);
        finalMovePos = new Vector3(movePos.x, transform.position.y, transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            screenPos = new Vector3(Input.mousePosition.x, gameObject.transform.position.y - Camera.main.transform.position.y, gameObject.transform.position.z - Camera.main.transform.position.z); // mousePos works for touch too
            moveFlag = true;
        }



        if (moveFlag == true)
        {
            MoveToPosition();
        }
	}

    void OnTouchDown()
    {
        screenPos = new Vector3(Input.mousePosition.x, yPositionFixed, 0); // mousePos works for touch too

        moveFlag = true;
    }

    void MoveToPosition()
    {
        this.gameObject.transform.position = Vector3.Lerp(this.transform.position, finalMovePos, moveSpeed);
    }
}
