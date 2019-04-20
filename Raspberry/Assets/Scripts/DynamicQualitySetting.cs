using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicQualitySetting : MonoBehaviour
{
    public float qualityChangeAtCameraSize;

    const int lowQualityIndex = 1;
    const int mediumQualityIndex = 2;
    const int veryhighQualityIndex = 4;

    public bool gameIsPaused = false;

    void Start ()
    {
	}
	
	void Update ()
    {
        if (gameIsPaused == false)
        {
            if (Camera.main.orthographicSize >= qualityChangeAtCameraSize)
                SetMediumQuality();

            else
                SetVeryHighQuality();
        }

        else
            SetLowQuality();
    }

    public void SetVeryHighQuality()
    {
        QualitySettings.SetQualityLevel(veryhighQualityIndex, false);
    }

    public void SetMediumQuality()
    {
        QualitySettings.SetQualityLevel(mediumQualityIndex, false);
    }

    public void SetLowQuality()
    {
        QualitySettings.SetQualityLevel(lowQualityIndex, false);
    }

    public void PauseControl(bool state)
    {
        if (state == true)
            gameIsPaused = true;

        if (state == false)
            gameIsPaused = false;
    }
}
