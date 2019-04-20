using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{

    public InputField strength;
    public InputField multiply;
    public InputField grav;
    public InputField tilt;
    public InputField orthoSpeed;
    public InputField ropeBreakForce;
    public InputField playerMass;
    public InputField tiltRange;
    public InputField tiltDampening;
    public InputField thrustParticleEffectPadding;
    public InputField maxForceAllowed;
    public InputField cameraRollSpeed;
    public InputField areaRollSpeed;




    public Button graphicDefault;
    public Button graphicVertex;

    public Text FPS;

    public GameObject rope;

    public Text acceData;

    public bool spinLockBool = false;

    [Header("Camera")]
    public float deltaMagnitude;

    private ThrustControl thrustControl;
    private CameraPinchZoom cameraPinchZoom;
    private AccelerometerControl accelerometerControl;
    private GameObject player;
    private GameObject playArea;

    // Use this for initialization
    void Start ()
    {
        thrustControl = GameObject.FindObjectOfType<ThrustControl>();
        cameraPinchZoom = GameObject.FindObjectOfType<CameraPinchZoom>();
        accelerometerControl = GameObject.FindObjectOfType<AccelerometerControl>();
        rope = GameObject.FindGameObjectWithTag("Rope");
        player = GameObject.FindGameObjectWithTag("Player");
        playArea = GameObject.FindGameObjectWithTag("Play Area");
    }

    // Update is called once per frame
    void Update ()
    {
        acceData.text = Input.acceleration.x.ToString();
        cameraPinchZoom.deltaMagnitudeDiff = deltaMagnitude;
        FPS.text = (1f / Time.deltaTime).ToString();

        if(spinLockBool == true)
        {
            playArea.GetComponent<AccelerometerControl>().enabled = false;
        }
	}

    public void UIDebugUpdate()
    {
        if (strength.text != null && multiply.text != null)
        {
            thrustControl.forceStrength = float.Parse(strength.text, NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
            thrustControl.forceMultiply = float.Parse(multiply.text, NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
        }
    }

    public void EnableGravity()
    {
        float gravSetting = float.Parse(grav.text, NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
        Physics2D.gravity = new Vector2(0, -gravSetting);
    }

    public void DisableGrav()
    {
        Physics2D.gravity = new Vector2(0, 0);
    }

    public void UpdateAcce()
    {
        accelerometerControl.methodUpdateTimer = float.Parse(tilt.text, NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
    }

    public void UpdateOrthoSpeed()
    {
       cameraPinchZoom.orthorgraphicZoomSpeed = float.Parse(orthoSpeed.text, NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
    }

    public void UpdateDistanceJointBreakForce()
    {
        rope.transform.GetChild(rope.transform.childCount).GetComponent<DistanceJoint2D>().breakForce = float.Parse(ropeBreakForce.text, NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
        rope.transform.GetChild(rope.transform.childCount).GetComponent<HingeJoint2D>().breakForce = float.Parse(ropeBreakForce.text, NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
    }

    public void UpdateCameraGraphicModeDefault()
    {
        Camera.main.renderingPath = RenderingPath.UsePlayerSettings;
    }

    public void UpdateCameraGraphicVertexLit()
    {
        Camera.main.renderingPath = RenderingPath.VertexLit;
    }

    public void ChangePlayerMass()
    {
        player.GetComponent<Rigidbody2D>().mass = float.Parse(playerMass.text, NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
    }

    public void ChangeAcceTiltRange()
    {
        accelerometerControl.tiltRange = float.Parse(tiltRange.text, NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
    }

    public void ChangeTiltDampening()
    {
        accelerometerControl.tiltDampeningValue = float.Parse(tiltDampening.text, NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
    }

    public void ChangeThrusterParticleEffect()
    {
        thrustControl.thrustEffectPadding = float.Parse(thrustParticleEffectPadding.text, NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
    }

    public void UpdateMaximumAllowedForce()
    {
        thrustControl.maximumAllowedMoveForce = float.Parse(maxForceAllowed.text, NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
    }

    public void UpdateCameraRollSpeed()
    {
        cameraPinchZoom.cameraRollSpeed = float.Parse(cameraRollSpeed.text, NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
    }

    public void UpdateAreaRollSpeed()
    {
        accelerometerControl.areaRollSpeed = float.Parse(areaRollSpeed.text, NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture);
    }
}
