using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [System.Serializable]
    public class GameObjectActivation
    {
        public GameObject targetObject;
        [Range(0, 24)] public float startHour;
        [Range(0, 24)] public float stopHour;
    }

    public dayNightCycle dayNightCycle; // Assign this reference through the Unity Inspector
    public List<GameObjectActivation> activationList;

    private void Start()
    {
        foreach (var activation in activationList)
        {
            if (activation.targetObject != null)
            {
                activation.targetObject.SetActive(false);
            }
        }

        if (dayNightCycle == null)
        {
            Debug.LogError("dayNightCycle script not assigned through the Unity Inspector.");
        }
    }

    private void Update()
    {
        if (dayNightCycle == null)
        {
            Debug.LogError("dayNightCycle is not assigned.");
            return;
        }

        float timeOfDay = dayNightCycle.timeOfDay;

        foreach (var activation in activationList)
        {
            bool isTimeInRange = IsCurrentHourInRange(timeOfDay, activation.startHour, activation.stopHour);
            activation.targetObject.SetActive(isTimeInRange);

            Debug.Log($"Current Hour: {timeOfDay}, Is Time In Range: {isTimeInRange}, Target Object Active: {activation.targetObject.activeSelf}");
        }
    }

    private bool IsCurrentHourInRange(float currentHour, float startHour, float stopHour)
    {
        return currentHour >= startHour && currentHour < stopHour;
    }
}
