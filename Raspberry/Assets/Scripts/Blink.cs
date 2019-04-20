using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Blink : MonoBehaviour
{

    public GameObject eye1;
    public GameObject eye2;

    private float blinkTimer;
    private float timer;
    private bool timerFlag = true;


    void Start ()
    {
        //Cached ??
	}
	
	void Update ()
    {
        timer += Time.deltaTime;

        if(timerFlag == true)
        // creates a number between 1 and 15
            RandomTimerGenerator(1, 15);


        if (timer >=  blinkTimer)
        {
            timer = 0;
            timerFlag = true;

            CharacterBlink();

        }

        Debug.Log(blinkTimer);
    }

    private void RandomTimerGenerator(int rangeMin, int rangeMax)
    {
        System.Random rnd = new System.Random();
        blinkTimer = rnd.Next(rangeMin, rangeMax);
        timerFlag = false;
    }

    private void CharacterBlink()
    {
        eye1.GetComponent<Animator>().SetTrigger("Blink");
        eye2.GetComponent<Animator>().SetTrigger("Blink"); //Redundant
        eye1.GetComponent<Animator>().ResetTrigger("Blink");
    }
}
