using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaControlPort : MonoBehaviour
{
    Vector3 initialRotation = new Vector3(0, 0, 0);

    Vector3 touchInitialPos;
    Vector3 currentTouchPos;

    private Vector3 directionalVector;

    public int numberOfRotation;

    public AudioClip[] portPowerSounds;

  

    void Update()
    {
    }

    void OnTouchDown()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchInitialPos = Camera.main.ScreenToWorldPoint(touch.position);
        }

        PlaySounEffect("up");
    }

    void OnTouchStay()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            currentTouchPos = Camera.main.ScreenToWorldPoint(touch.position);

            directionalVector = currentTouchPos - this.transform.position;

            float rot_z = Mathf.Atan2(directionalVector.y, directionalVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }

        PlaySounEffect("loop");


#if UNITY_EDITOR

        currentTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        directionalVector = currentTouchPos - this.transform.position;

        float rot_zDebug = Mathf.Atan2(directionalVector.y, directionalVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_zDebug - 90);

        PlaySounEffect("loop");

#endif

    }

    void OnTouchUp()
    {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, 0, 0), 0.5f);
        PlaySounEffect("down");
    }

    void PlaySounEffect(string state)
    {
        

        if (state == "up")
        {
            this.GetComponent<AudioSource>().clip = portPowerSounds[0];
        }

        if (state == "loop")
        {
            this.GetComponent<AudioSource>().clip = portPowerSounds[1];

        }

        if (state == "down")
        {
            this.GetComponent<AudioSource>().clip = portPowerSounds[2];
        }

        if (!this.GetComponent<AudioSource>().isPlaying)
            this.GetComponent<AudioSource>().Play(0);
    }
}
