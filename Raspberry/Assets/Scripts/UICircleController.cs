using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICircleController : MonoBehaviour
{
    public GameObject affectedObject;
    public float positionToCameraOffset;
    public float xPosOffsetLineToCircle;

    private LineRenderer lineRenderer;
    private CameraPinchZoom cameraPinchZoom;
	void Start ()
    {
        lineRenderer = GetComponent<LineRenderer>();
        cameraPinchZoom = FindObjectOfType<CameraPinchZoom>();
	}
	
	void Update ()
    {

        Vector3[] positions = new Vector3[4];
        positions[0] = this.gameObject.transform.position - new Vector3(xPosOffsetLineToCircle, 0, this.transform.position.z - 1);
        positions[1] = new Vector3((this.transform.position.x + affectedObject.transform.position.x) / 2, this.gameObject.transform.position.y, this.transform.position.z - 1);
        positions[2] = new Vector3(positions[1].x, affectedObject.transform.position.y, this.transform.position.z - 1);
        positions[3] = new Vector3(affectedObject.transform.position.x, positions[2].y, this.transform.position.z - 1);


        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);

        //if(cameraPinchZoom.isZooming  == true)
        //{
        //    ScaleWithScreen(Camera.main.orthographicSize / positionToCameraOffset);
        //}
    }

    void ScaleWithScreen(float offSet)
    {
        this.transform.position = new Vector3(transform.position.x + offSet, 0, 0);
    }

}
