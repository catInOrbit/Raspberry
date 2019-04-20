using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeImpactSound : MonoBehaviour {

    public AudioClip impactThunk;

    public GameObject impactSoundPlayer;

    private void Start()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Rope")
        {
            impactSoundPlayer.GetComponent<AudioSource>().clip = impactThunk;
            impactSoundPlayer.GetComponent<AudioSource>().Play(0);
        }
    }
}
