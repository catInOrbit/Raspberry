using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnegeryRing : MonoBehaviour
{
    public Camera mainCamera;
    public float zoomValue;
    public float offSetValue;
    public float padding;

    private float size;
    Vector2 cameraCenterPosition = new Vector2(0, 0);
	void Update ()
    {
        AutoResizeEngeryRing();
	}

    public void AutoResizeEngeryRing()
    {
        size = mainCamera.orthographicSize;
        //Vector2 currentSize = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, this.GetComponent<RectTransform>().sizeDelta.y);
       Vector2 currentSize = new Vector2(516f, 516f);
       // //this.GetComponent<RectTransform>().sizeDelta = new Vector2(currentSize.x * size * zoomValue, currentSize.y * size * zoomValue);
       // // this.GetComponent<RectTransform>().position = new Vector2(-mainCamera.transform.position.x * offSetValue, -mainCamera.transform.position.y * offSetValue);
       // float updateSize = this.GetComponent<RectTransform>().sizeDelta.x / mainCamera.orthographicSize;
       // if(mainCamera.orthographicSize > 3.3)
       //     this.GetComponent<RectTransform>().sizeDelta = new Vector2(currentSize.x - updateSize, currentSize.y - updateSize);
       // else
       //     this.GetComponent<RectTransform>().sizeDelta = new Vector2(currentSize.x + updateSize, currentSize.y + updateSize);

        float radius = this.GetComponent<RectTransform>().sizeDelta.x / 2;
        float update = radius / mainCamera.orthographicSize;
        if(mainCamera.orthographicSize > 3.17)
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(currentSize.x - update - padding, currentSize.y - update - padding);
        else
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(currentSize.x + update + padding, currentSize.y + update + padding);
    }
}
