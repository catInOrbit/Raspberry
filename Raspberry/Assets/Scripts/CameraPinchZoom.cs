using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPinchZoom : MonoBehaviour
{
    public float orthorgraphicZoomSpeed;
    public float deltaMagnitudeDiff;
    public float cameraRollSpeed;

    [Header("UI Related")]
    public float uiEnableDistance = 2;
    public GameObject[] thisLevelUIs;
    public GameObject playAreaSprite;
    public GameObject spinDegree;


    private GameObject accelerometerControl;
    private AccelerometerControl accelerometerControlScript;

    public bool isZooming = false;

    const float cameraRepositionSpeed = 0.3f;
    void Start()
    {
        accelerometerControl = GameObject.FindGameObjectWithTag("Play Area");
        accelerometerControlScript = GameObject.FindObjectOfType<AccelerometerControl>();
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {

            isZooming = true;
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;


            // ... change the orthographic size based on the change in distance between the touches.
            this.GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthorgraphicZoomSpeed;

            // Make sure the orthographic size never drops below zero.
            this.GetComponent<Camera>().orthographicSize = Mathf.Max(this.GetComponent<Camera>().orthographicSize, 0.1f);

        }

        else if (Input.touchCount != 2)
        {
            isZooming = false;
        }

        AccelerometerManager();
        LevelUIManager(thisLevelUIs);

        //this.transform.localEulerAngles = new Vector3(0, 0, accelerometerControl.transform.localEulerAngles.z); //Camera rotation = play area rotation, making sure Vector3.up is always the same
#if UNITY_EDITOR
        MatchCameraAngleWithPlayArea(accelerometerControlScript.testValue);
#endif
        MatchCameraAngleWithPlayArea(Input.acceleration.x);

        if (this.GetComponent<Camera>().orthographicSize >= 4)
        {
            this.GetComponent<Camera>().orthographicSize = 4;
        }


#if UNITY_EDITOR
        this.GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthorgraphicZoomSpeed;
        this.GetComponent<Camera>().orthographicSize = Mathf.Max(this.GetComponent<Camera>().orthographicSize, 0.1f);
#endif
    }

    private void MatchCameraAngleWithPlayArea(float accelerationInput) //Instead of matching camera angle, we match the sprite of play area
    {
        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0,0,0), cameraRollSpeed * Mathf.Abs(accelerationInput));
        playAreaSprite.transform.rotation = Quaternion.Lerp(playAreaSprite.transform.rotation, Quaternion.Euler(0,0,0), cameraRollSpeed * Mathf.Abs(accelerationInput));
    }

    void AccelerometerManager()
    {
        if(this.gameObject.GetComponent<Camera>().orthographicSize >= 3)
        {
            //Flash animation
            this.GetComponent<Animator>().SetBool("Flash", true);

            //Smooth repositioning
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(0.103f, -0.04f, this.transform.position.z), cameraRepositionSpeed);

            //Reduce timescale
            Time.timeScale = 0.3f;

            //Enable SpinDegree
            spinDegree.GetComponent<TextMesh>().text = Mathf.Round(playAreaSprite.transform.localEulerAngles.z).ToString() + "°";
            spinDegree.gameObject.SetActive(true);

            accelerometerControl.gameObject.GetComponent<AccelerometerControl>().enabled = true;
            gameObject.GetComponent<CameraMovement>().enabled = false;
        }

        else
        {
            this.GetComponent<Animator>().SetBool("Flash", false);
            Time.timeScale = 1f;

            accelerometerControl.gameObject.GetComponent<AccelerometerControl>().enabled = false;
            gameObject.GetComponent<CameraMovement>().enabled = true;

            spinDegree.gameObject.SetActive(false);
        }
    }

    void LevelUIManager(GameObject[] levelUIs)
    {
        if(gameObject.GetComponent<Camera>().orthographicSize >= uiEnableDistance)
        {
            foreach (var UI in levelUIs)
            {
                UI.GetComponent<Animator>().SetBool("UIState", true);
                if ((UI.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("UIConfirmed") || UI.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("UIConfirmed2")) && UI.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                {
                    UI.GetComponent<Animator>().SetBool("UIStateAfterConfirmation", true);
                }
            }
        }

        else
        {
            foreach (var UI in levelUIs)
            {
                UI.GetComponent<Animator>().SetBool("UIState", false);
                UI.GetComponent<Animator>().SetBool("UIStateAfterConfirmation", false);
            }
        }
    }
}
