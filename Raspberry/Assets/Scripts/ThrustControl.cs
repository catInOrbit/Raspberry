using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ThrustControl : MonoBehaviour
{

    public Vector2 startPos;
    public Vector2 direction;
    public Vector2 endPos;
    public Vector2 gameObjectPos;
   // public GameObject attach;

    public float forceStrength;
    public float forceMultiply;
    public float debugMouseDeltaPos = 0;
    public float maximumAllowedMoveForce;

   // public bool attachSwitch;
    public ParticleSystem[] thrustEffects; // 0: up / 1: down / 2: left / 3: right
    public ParticleSystem[] thrustEffectsReverse;
    public ParticleSystem[] thrustEffectsOriginal;

    public GameObject[] headlights; // 0: up / 1: down / 2: left / 3: right


    [Header("Sound effect")]
    public AudioClip[] thrusterSoundEffects; // 0: Start effect / 1: Loop effect / 2: End effect

    [Header("SAS")]
    public float SASRotationDampening;

    public float thrustEffectPadding;
    const float maxThrustEffectRadius = 5;
    const float particleEffectEmissionRate = 263f;
    const float particleEffectLifeTime = 0.13f;

    private GameObject playArea;
    bool debugCheckInverse = false;




    void Start ()
    {
        playArea = GameObject.FindGameObjectWithTag("Play Area");

        //Disable particle effect at the start of the level
        foreach (var effect in thrustEffects)
        {
            effect.enableEmission = false;
        }
    }

    void Update()
    {
     
    }

    void FixedUpdate()
    {
#if UNITY_EDITOR
        Vector3 mouseStartPos = new Vector3(0,0,0);
        Vector3 mouseDirection;
        Vector3 mouseEndpos;
        Vector3 moveDirection;



        if(Input.GetMouseButtonDown(0))
        {
            mouseStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("mouse starting position: " + mouseStartPos);
        }

        if(Input.GetMouseButton(0))
        {
            mouseEndpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDirection = mouseEndpos - mouseStartPos;
            Debug.Log("mouse direction: " + mouseDirection + " end postion: " + mouseEndpos);


            moveDirection = this.transform.position + mouseDirection;
            Debug.Log(Vector3.Distance(mouseStartPos, mouseEndpos));

            if (Vector3.Distance(mouseStartPos, mouseEndpos) < debugMouseDeltaPos)
            {
                startPos = Input.mousePosition;
            }

            else
                forceStrength = Vector3.Distance(mouseStartPos, mouseEndpos);

            this.GetComponent<Rigidbody2D>().AddForce(moveDirection * forceStrength * forceMultiply);
        }

        if (Input.GetKey(KeyCode.W))
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector3.up * forceMultiply);
            ThrusterEffect(new Vector3(1, 1, 0) * forceMultiply,false);
            PlaySoundEffect("On");
            SASFixRotationModule();

        }

        if (Input.GetKey(KeyCode.S))
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector3.down * forceMultiply);
            ThrusterEffect(new Vector3(2, -1, 0) * forceMultiply, false);
            PlaySoundEffect("On");
            SASFixRotationModule();


        }

        if (Input.GetKey(KeyCode.A))
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector3.left * forceMultiply);
            ThrusterEffect(Vector3.left * forceMultiply, false);
            PlaySoundEffect("On");
            SASFixRotationModule();


        }

        if (Input.GetKey(KeyCode.D))
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector3.right * forceMultiply);
            ThrusterEffect(Vector3.right * forceMultiply,false );
            PlaySoundEffect("On");
            SASFixRotationModule();

        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            ThrusterEffect(new Vector3(0, 0, 0) * forceMultiply, true);
            PlaySoundEffect("Off");
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            ThrusterEffect(new Vector3(0, 0, 0) * forceMultiply, true);
            PlaySoundEffect("Off");

        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            ThrusterEffect(new Vector3(0, 0, 0) * forceMultiply, true);
            PlaySoundEffect("Off");

        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            ThrusterEffect(new Vector3(0, 0, 0) * forceMultiply, true);
            PlaySoundEffect("Off");
        }

        if(Input.anyKeyDown)
        {
            PlaySoundEffect("On");
        }

#endif

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;  
                    Debug.Log("first touch at" + startPos);
                    break;
                case TouchPhase.Moved:
                    endPos = touch.position;
                    Debug.Log("Final touch position vector:" + endPos);

                    direction = endPos - startPos;

                    forceStrength = Vector2.Distance(startPos, endPos);

                    if (forceStrength >= maximumAllowedMoveForce)
                        this.GetComponent<Rigidbody2D>().AddForce(/*InverseDirectionalVector(direction)*/ direction * maximumAllowedMoveForce * forceMultiply);
                    else
                        this.GetComponent<Rigidbody2D>().AddForce(/*InverseDirectionalVector(direction)*/ direction * forceStrength * forceMultiply);
                    ThrusterEffect(direction, false);
                    SASFixRotationModule();
                    PlaySoundEffect("On");

                    Debug.Log("Direction vector:" + direction);
                    break;
                case TouchPhase.Stationary:
                    //startPos = touch.position;
                    //this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,0));
                    //endPos = touch.position;
                    //direction = endPos - startPos;

                    if (forceStrength >= maximumAllowedMoveForce)
                        this.GetComponent<Rigidbody2D>().AddForce(direction * maximumAllowedMoveForce);
                    else
                        this.GetComponent<Rigidbody2D>().AddForce(direction * forceStrength * forceMultiply);

                    ThrusterEffect(direction, true);
                    break;
                case TouchPhase.Ended:
                    ThrusterEffect(direction, true);
                    PlaySoundEffect("Off");

                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }
        }

        //if(attachSwitch == true)
        //    this.transform.position = attach.transform.position;
    }

    void ThrusterEffect(Vector3 moveVector, bool stopEffect)
    {



        float upwardThruster = 0;
        float downwardThruster = 0;
        float leftThruster = 0;
        float rightThruster = 0;

        //Debug.Log("move vector :" + moveVector);
        //Debug.Log("angle to up vector " + Vector3.Angle(moveVector, Vector3.up));
        //Debug.Log("angle to right vector " + Vector3.Angle(moveVector, Vector3.right));

        float compareAngleToUpVector = Vector3.Angle(moveVector, Vector3.up);
        float compareAngleToRightVector = Vector3.Angle(moveVector, Vector3.right);
        float compareAngleToDownVector = Vector3.Angle(moveVector, Vector3.down);
        float compareAngleToLeftVector = Vector3.Angle(moveVector, Vector3.left);

        //Debug.Log("dotTop " + Vector2.Dot(moveVector, Vector2.up));
        //Debug.Log("dotDown " + Vector2.Dot(moveVector, Vector2.down));

        if (stopEffect == true)
        {

            foreach (var thrustEffect in thrustEffects)
            {
                thrustEffect.enableEmission = false;
            }

        }

        else
        {


            //CheckInverseThrustEffect();

            if ((compareAngleToUpVector >= 0 && compareAngleToUpVector <= 90) && (compareAngleToRightVector >= 0 && compareAngleToRightVector <= 90)) //top right 1/4 radian circle
            {


                leftThruster = Vector3.Project(moveVector, Vector3.left).magnitude;
                downwardThruster = Vector3.Project(moveVector, Vector3.down).magnitude;

                //headlights[0].transform.rotation = Quaternion.FromToRotation(headlights[0].transform.position, moveVector);

                rightThruster = 0;
                upwardThruster = 0;

                headlights[0].SetActive(false);
                headlights[1].SetActive(true);
                headlights[1].transform.rotation = Quaternion.FromToRotation(new Vector3(headlights[1].transform.rotation.x, headlights[1].transform.rotation.y, 0.13f), new Vector3(moveVector.x, moveVector.y, 0.13f));


                Debug.Log("TOP RIGHT");
            }

            else if ((compareAngleToDownVector >= 0 && compareAngleToDownVector <= 90) && (compareAngleToRightVector >= 0 && compareAngleToRightVector <= 90)) //bottom right 1/4 radian circle
            {

                leftThruster = Vector3.Project(moveVector, Vector3.left).magnitude;
                upwardThruster = Vector3.Project(moveVector, Vector3.up).magnitude;

                rightThruster = 0;
                downwardThruster = 0;

                headlights[0].SetActive(false);
                headlights[1].SetActive(true);
                headlights[1].transform.rotation = Quaternion.FromToRotation(new Vector3(headlights[1].transform.rotation.x, headlights[1].transform.rotation.y, 0.13f), new Vector3(moveVector.x, moveVector.y, 0.13f));


                Debug.Log("BOTTOM RIGHT");

            }

            else if ((compareAngleToLeftVector >= 0 && compareAngleToLeftVector <= 90) && (compareAngleToDownVector >= 0 && compareAngleToDownVector <= 90)) //bottom left 1/4 radian circle
            {
                rightThruster = Vector3.Project(moveVector, Vector3.right).magnitude;
                upwardThruster = Vector3.Project(moveVector, Vector3.up).magnitude;

                leftThruster = 0;
                downwardThruster = 0;

                headlights[1].SetActive(false);
                headlights[0].SetActive(true);

                headlights[0].transform.rotation = Quaternion.FromToRotation(new Vector3(headlights[0].transform.rotation.x, headlights[0].transform.rotation.y, 0.13f), new Vector3(moveVector.x, moveVector.y, 0.13f));




            }

            else //top left 1/4 radian circle
            {
                rightThruster = Vector3.Project(moveVector, Vector3.right).magnitude;
                downwardThruster = Vector3.Project(moveVector, Vector3.down).magnitude;

                upwardThruster = 0;
                leftThruster = 0;

                headlights[1].SetActive(false);
                headlights[0].SetActive(true);

                headlights[0].transform.rotation = Quaternion.FromToRotation(new Vector3(headlights[0].transform.rotation.x, headlights[0].transform.rotation.y, 0.13f), new Vector3(moveVector.x, moveVector.y, 0.13f));

            }

            Debug.Log("top:" + upwardThruster);
            Debug.Log("bottoM: " + downwardThruster);

            foreach (var effect in thrustEffects)
            {
                effect.enableEmission = true;

                effect.startLifetime = particleEffectLifeTime;
                effect.emissionRate = particleEffectEmissionRate;
            }

            thrustEffects[0].startLifetime = upwardThruster/thrustEffectPadding;
            thrustEffects[1].startLifetime = downwardThruster/thrustEffectPadding;
            thrustEffects[2].startLifetime = leftThruster/thrustEffectPadding;
            thrustEffects[3].startLifetime = rightThruster/thrustEffectPadding;
        }

    }

    void PlaySoundEffect(string state)
    {
        if (state == "On")
        {

            gameObject.GetComponent<AudioSource>().clip = thrusterSoundEffects[0];

            if (!gameObject.GetComponent<AudioSource>().isPlaying)
                gameObject.GetComponent<AudioSource>().Play(0);

        }

        else if(state == "Off")
        {
            gameObject.GetComponent<AudioSource>().clip = thrusterSoundEffects[2];

            if (!gameObject.GetComponent<AudioSource>().isPlaying)
                gameObject.GetComponent<AudioSource>().Play(1);
        }
    }

    void SASFixRotationModule() //Turn object to rotation (0,0,0)
    {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, 0, 0), SASRotationDampening);
    }

    Vector3 InverseDirectionalVector(Vector3 directionVector) //Method will run when PlayArea localEulers is > 90
    {
        if (playArea.transform.localEulerAngles.z >= 90 || playArea.transform.localEulerAngles.z <= -90)
        {
            directionVector = -directionVector;
            return directionVector;
        }

        else
            return directionVector;

    }

    void CheckInverseThrustEffect()
    {
        if (playArea.transform.localEulerAngles.z >= 90 || playArea.transform.localEulerAngles.z <= -90)
        {
            for (int i = 0; i < thrustEffects.Length; i++)
                thrustEffects[i] = thrustEffectsReverse[i];
        }

        else
        {
            for (int i = 0; i < thrustEffects.Length; i++)
                thrustEffects[i] = thrustEffectsOriginal[i];
        }
    }
}
