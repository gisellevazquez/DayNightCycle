using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightSetting
{
    public Light light;
    public bool shouldTurnOnDuringDay;
    public bool turnOffDuringDay; // New boolean field for turning off during the day
}

public class dayNightCycle : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField, Range(0, 24)] public float timeOfDay;
    [SerializeField] private float sunRotationSpeed;

    [field: Header("LightPreset")]
    [SerializeField] private Gradient skyColor;
    [SerializeField] private Gradient equatorColor;
    [SerializeField] private Gradient sunColor;

    [SerializeField] private List<LightSetting> allLights; // All lights with on/off settings

    void UpdateSunRotation()
    {
        float sunRotation = Mathf.Lerp(-90, 270, timeOfDay / 24);
        sun.transform.rotation = Quaternion.Euler(sunRotation, sun.transform.rotation.y, sun.transform.rotation.z);
    }

    private void UpdateLighting()
    {
        float timeFraction = timeOfDay / 24;
        RenderSettings.ambientEquatorColor = equatorColor.Evaluate(timeFraction);
        RenderSettings.ambientSkyColor = skyColor.Evaluate(timeFraction);
        sun.color = sunColor.Evaluate(timeFraction);

        // Check if it's daytime (adjust the condition based on your preference)
        bool isDaytime = (timeOfDay >= 6 && timeOfDay < 18);

        foreach (var lightSetting in allLights)
        {
            bool shouldLightBeOn = isDaytime ? lightSetting.shouldTurnOnDuringDay : !lightSetting.shouldTurnOnDuringDay;
            lightSetting.light.enabled = shouldLightBeOn;

            // Turn off the light during the day if specified
            if (isDaytime && lightSetting.turnOffDuringDay)
            {
                lightSetting.light.enabled = false;
            }
        }
    }

    private void Update()
    {
        timeOfDay += Time.deltaTime * sunRotationSpeed;
        if (timeOfDay > 24)
            timeOfDay = 0;
        UpdateSunRotation();
        UpdateLighting();
    }

    private void OnValidate()
    {
        UpdateSunRotation();
        UpdateLighting();
    }
}