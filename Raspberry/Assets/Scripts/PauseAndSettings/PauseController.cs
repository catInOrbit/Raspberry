using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private Rigidbody2D[] allAvtiveObjectsOnScreen;

    public bool enablePause = false;

    private Rigidbody2D orgrinalConstraint;

    public Camera pauseScreenCam;

    private GameObject mainCamera;
    private CameraMovement cameraMovement;
    private CameraPinchZoom cameraPinchZoom;

	void Start ()
    {
        allAvtiveObjectsOnScreen = GameObject.FindObjectsOfType<Rigidbody2D>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraMovement = GameObject.FindObjectOfType<CameraMovement>();
        cameraPinchZoom = GameObject.FindObjectOfType<CameraPinchZoom>();
	}
	
	void Update ()
    {
        if (enablePause == true)
        {
            foreach (var gameObject in allAvtiveObjectsOnScreen)
            {
                gameObject.constraints = RigidbodyConstraints2D.FreezeAll;
                cameraPinchZoom.enabled = false;
            }
        }

        else if(enablePause == false)
        {
            foreach (var gameObject in allAvtiveObjectsOnScreen)
            {
                gameObject.constraints = RigidbodyConstraints2D.None;
                cameraPinchZoom.enabled = true;
            }
        }

        pauseScreenCam.transform.position = mainCamera.transform.position;
        pauseScreenCam.orthographicSize = mainCamera.GetComponent<Camera>().orthographicSize;
        pauseScreenCam.transform.rotation = mainCamera.transform.rotation;
    }

    public void EnablePause()
    {
        enablePause = true;
    }

    public void DisablePause()
    {
        enablePause = false;
    }

}
