using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControlScript : MonoBehaviour
{

    public LayerMask touchInputMask;

	
	void Update ()
    {

#if UNITY_EDITOR
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, touchInputMask))
            {
                GameObject touchUI = hit.transform.gameObject;

                if (Input.GetMouseButtonDown(0))
                {
                    touchUI.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    touchUI.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                }

                if (Input.GetMouseButton(0))
                {
                    touchUI.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }


#endif


        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit, touchInputMask))
                {
                    GameObject touchUI = raycastHit.transform.gameObject;

                    if (touch.phase == TouchPhase.Began)
                    {
                        touchUI.SendMessage("OnTouchDown", raycastHit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        touchUI.SendMessage("OnTouchUp", raycastHit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        touchUI.SendMessage("OnTouchStay", raycastHit.point, SendMessageOptions.DontRequireReceiver); //Press and hold
                    }

                    if (touch.phase == TouchPhase.Canceled)
                    {
                        touchUI.SendMessage("OnTouchExit", raycastHit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
	}
}
