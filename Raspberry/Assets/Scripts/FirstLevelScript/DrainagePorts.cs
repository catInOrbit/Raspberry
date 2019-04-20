using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainagePorts : MonoBehaviour
{
    private GameObject water;

    public float waterDrainageAmount;
    public float waterDrainageTime;

    public GameObject[] drainagePorts; // 0 and 1: drainagePort / 3: exit port

    private bool beginWaterAnim = false;
    private bool exitIsOpened = false;
    private bool firstPortIsOpen= false;
    private bool secondPortIsOpen = false;
    private GameObject animatedZone;

    Vector3 downwardMovement;


    void Start()
    {
        water = GameObject.FindGameObjectWithTag("Water");
        animatedZone = GameObject.FindGameObjectWithTag("FirstLevelAnimatedZone");
    }

    void Update()
    {
        if(beginWaterAnim == true)
        {
            WaterDrainageAnim();
        }

        if(firstPortIsOpen)
            drainagePorts[0].GetComponent<Animator>().Play("DrainagePortEnable");

        if(secondPortIsOpen)
            drainagePorts[1].GetComponent<Animator>().Play("DrainagePortEnable2");


        if(firstPortIsOpen == true && secondPortIsOpen == true)
            drainagePorts[2].GetComponent<Animator>().SetTrigger("Exit");
    }

    void OnTouchDown()
    {
        downwardMovement = new Vector3(water.transform.position.x, water.transform.position.y - waterDrainageAmount, water.transform.position.z);
        beginWaterAnim = true;
    }

    void WaterDrainageAnim()
    {
        water.transform.position = Vector3.Lerp(water.transform.position, downwardMovement, waterDrainageTime);
        if(this.gameObject.tag == "UICircle1")
        {
            this.GetComponent<Animator>().SetTrigger("UIConfirmed1");
            drainagePorts[2].GetComponent<DrainagePorts>().firstPortIsOpen = true;
        }

        else if(this.gameObject.tag == "UICircle2")
        {
            this.GetComponent<Animator>().SetTrigger("UIConfirmed2");
            drainagePorts[2].GetComponent<DrainagePorts>().secondPortIsOpen = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.gameObject.name == "DarkGreyPort" && collision.gameObject.tag == "Player")
        {
            animatedZone.GetComponent<Animation>().Play("DemoEnd");
        }
    }

}
