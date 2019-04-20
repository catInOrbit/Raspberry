using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerControl : MonoBehaviour
{

    public GameObject controller;
    public float tiltSpeed;
    public float tiltDampeningValue;
    public float tiltValue;
    public float tiltRange;

    [Range(-1f, 1f)]
    public float testValue;

    public float timer = 1;
    public float methodUpdateTimer;

    private float firstAcceInterval;
    private float secondAcceInterval;
    private Quaternion firstRotationValue;
    private Quaternion secondRotationValue;

    public float areaRollSpeed = 2;

    [SerializeField]
    private bool updateIntervalBool;

    private Vector3 targetRotation;

    void Start ()
    {
		
	}
	
	void FixedUpdate ()
    {
        //float tiltVar = 0;
        //float previousAcce = Input.acceleration.x;

        //timer += Time.deltaTime;
        //if(timer >= methodUpdateTimer)
        //{
        //    float currentAcce = Input.acceleration.x;

        //    if((currentAcce - previousAcce) <= 0.1 && (currentAcce - previousAcce) >= 0.1)
        //    {
        //        tiltVar = returnLowPass(currentAcce, tiltVar);
        //        Quaternion targetRotation = Quaternion.Euler(0, 0, tiltVar * 180);
        //        MoveToTargetAngle(targetRotation);
        //    }



        //}

        //float tiltVar = 0f;
        //Vector3 accelerometerVector = Input.acceleration;
        //accelerometerVector.Normalize();
        //tiltVar = Mathf.Lerp(tiltVar, accelerometerVector.x, tiltDampeningValue * Time.deltaTime);


        UpdateTiltAcceleration(Input.acceleration.x);


#if UNITY_EDITOR



        //if (updateIntervalBool == false)
        //{
        //    UpdateFirstInterval(testValue);
        //}

        //if (updateIntervalBool == true)
        //{
        //    timer += Time.deltaTime;
        //    if (timer >= methodUpdateTimer)
        //        UpdateSecondInterVal(testValue);
        //}


        UpdateTiltAcceleration(testValue);


#endif

        //if (testValue > 0)
        //    stopPoint = testValue * 360 - 0.1f;
        //else
        //    stopPoint = testValue * 360 + 0.1f;

        //Debug.Log("Stop point: " + stopPoint);

        //Vector3 targetRotationAngle = new Vector3(0, 0, stopPoint);
        //Debug.Log("tartget roation: " + targetRotationAngle);
        //Vector3 rotationVector = new Vector3(0, 0, testValue);

        ////if (transform.localEulerAngles != targetRotationAngle)
        ////    transform.localEulerAngles += targetRotationAngle * tiltDampeningValue;
        ////else
        ////    transform.localEulerAngles = transform.localEulerAngles;

        //if (testValue > 0.3f || testValue < -0.3f)
        //{
        //    transform.localEulerAngles = targetRotationAngle;
        //}

        //else
        //    transform.rotation = transform.rotation;

        //Vector3 debugAngle = new Vector3(0, 0, testValue * 180);
        //Debug.Log("Debug angle: " + debugAngle);
        //this.transform.localEulerAngles = debugAngle * tiltDampeningValue;

        //this.transform.localEulerAngles = new Vector3(0, 0, tiltVar * 360);

    }

    void UpdateFirstInterval(float firstAcce)
    {
        firstAcceInterval = firstAcce;
        firstRotationValue = this.transform.rotation;
        updateIntervalBool = true;
    }

    //void UpdateSecondInterVal(float secondAcce)
    //{
    //    secondAcceInterval = secondAcce;
    //    secondRotationValue = Quaternion.Euler(0, 0, secondAcceInterval * 180);
        
    //    if ((secondAcceInterval - firstAcceInterval) >= tiltRange || (secondAcce - firstAcceInterval) <= -tiltRange)
    //    {
    //        //this.transform.localEulerAngles = new Vector3(0, 0, -secondAcceInterval * 180);
    //        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, 0, -secondAcceInterval * 180), tiltDampeningValue);

    //        if(this.transform.localEulerAngles != new Vector3(0, 0, secondAcceInterval * 180))
    //        {
    //            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, secondRotationValue, areaRollSpeed * Mathf.Abs( secondAcceInterval));
    //        }

    //        if (this.transform.rotation == secondRotationValue)
    //        {
    //            timer = 0;
    //            updateIntervalBool = false;
    //        }
    //    }
    //} //Deprecated tilt method

    void UpdateTiltAcceleration(float secondAcce)
    {
        secondAcceInterval = secondAcce;
        secondRotationValue = Quaternion.Euler(0, 0, -secondAcceInterval * 180);

        if ((secondAcceInterval - firstAcceInterval) >= tiltRange || (secondAcce - firstAcceInterval) <= -tiltRange)
        {
            //this.transform.localEulerAngles = new Vector3(0, 0, -secondAcceInterval * 180);
            //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, 0, -secondAcceInterval * 180), tiltDampeningValue);

            if (this.transform.localEulerAngles != new Vector3(0, 0, -secondAcceInterval * 180))
            {
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, secondRotationValue, areaRollSpeed * Mathf.Abs(secondAcceInterval) * Time.deltaTime)  ;
            }

            if (this.transform.rotation == secondRotationValue)
            {
                timer = 0;
                updateIntervalBool = false;
            }
        }
    }


    //IEnumerator UpdateAcceInterval(float time, float secondAcce)
    //{
    //    secondAcceInterval = secondAcce;
    //    while ((secondAcce - firstAcceInterval) >= 0.1f || (secondAcce - firstAcceInterval) <= -0.1f )
    //    {
    //        tiltValue = testValue;
    //        this.transform.localEulerAngles = new Vector3(0, 0, tiltValue * 180);
    //    }

    //    updateIntervalBool = false;
    //    yield return new WaitForSeconds(time);
    //}

 
    //void MoveToTargetAngle(Quaternion targetRotation)
    //{
    //    this.transform.rotation = Quaternion.Slerp(this.transform.localRotation, targetRotation, Time.deltaTime * tiltSpeed);
    //    timer = 0;
    //}
}
