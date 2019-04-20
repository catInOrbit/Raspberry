using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEnvironmentalDetection : MonoBehaviour //Mange sound and thrust
{

    public bool playerIsUnderwater;
    public AudioClip[] ambientSounds; //O: underwater / 1: surface
    public GameObject ambientSoundManager;
	
	void Update ()
    {
		if(playerIsUnderwater == true)
        {
            ambientSoundManager.GetComponent<AudioSource>().clip = ambientSounds[0];
            if(!ambientSoundManager.GetComponent<AudioSource>().isPlaying)
                ambientSoundManager.GetComponent<AudioSource>().Play(0);

        }

        else if(playerIsUnderwater == false)
        {
            ambientSoundManager.GetComponent<AudioSource>().clip = ambientSounds[1];
            if (!ambientSoundManager.GetComponent<AudioSource>().isPlaying)
                ambientSoundManager.GetComponent<AudioSource>().Play(0);

            this.GetComponent<Rigidbody2D>().gravityScale = 0.1f;

        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            this.GetComponent<ThrustControl>().enabled = true;
            playerIsUnderwater = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            this.GetComponent<ThrustControl>().enabled = false;
            playerIsUnderwater = false;
        }
    }
}
