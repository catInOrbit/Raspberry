using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedZoneSoundStorage : MonoBehaviour {

    public AudioClip[] audioClips;

    void Update()
    {
        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("EnableAreaController"))
        {
            this.GetComponent<AudioSource>().clip = audioClips[0];
            this.GetComponent<AudioSource>().Play(0);
        }
    }
}
