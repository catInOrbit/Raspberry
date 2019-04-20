using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPowerOnAnimation : MonoBehaviour {

    private GameObject player;
    private GameObject animatedZone;
    private AreaControllerCycleDetection areaControllerCycleDetection;

    private bool beginLevelAnim = false;
    private bool returnControl;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animatedZone = GameObject.FindGameObjectWithTag("FirstLevelAnimatedZone");
        areaControllerCycleDetection = GameObject.FindObjectOfType<AreaControllerCycleDetection>();
	}

    void Update()
    {
        if(beginLevelAnim == true)
        {
            Vector3 thisObject = new Vector3(this.transform.position.x, this.transform.position.y, player.transform.position.z);
            player.transform.position = Vector3.Lerp(player.transform.position, thisObject, 0.07f);
        }

        if(areaControllerCycleDetection.isActiveAndEnabled == true)
        {
            if (areaControllerCycleDetection.numberOfRotation >= 6)
            {
                animatedZone.GetComponent<Animator>().SetTrigger("PowerOnCompleted");
            }
        }
        

        if (animatedZone.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("EnableAreaController"))
        {
            beginLevelAnim = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            returnControl = true;


            if (animatedZone.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animatedZone.GetComponent<Animator>().IsInTransition(0))
            {
                animatedZone.GetComponent<Animator>().enabled = false;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            beginLevelAnim = true;
            animatedZone.GetComponent<Animator>().SetTrigger("PowerOn");
        }

    }
}
