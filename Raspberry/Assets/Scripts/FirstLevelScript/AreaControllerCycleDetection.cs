using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaControllerCycleDetection : MonoBehaviour
{


    public GameObject startPosition;
    public GameObject cycleCheckpoint;

    public int numberOfRotation;

    private bool halfRotation;
    private bool newCycle;


    void Start ()
    {
		
	}
	
	void Update ()
    {
        //Debug.Log(newCycle);
        //Debug.Log(halfRotation);

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "cycleStartPos")
        {
            newCycle = true;
            Debug.Log("New cycle");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "halfRotation")
        {
            if (newCycle == true)
                halfRotation = true;

            Debug.Log("Half rot");

        }

        if (collision.gameObject.tag == "cycleStartPos")
        {
            if (newCycle == true && halfRotation == true)
                numberOfRotation += 1;
        }
    }

}
