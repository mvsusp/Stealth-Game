using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour
{
    public float fadeSpeed = 2f;
    public float highIntensity = 2f;
    public float lowIntensity = 0.5f;
    public float changeMargin = 0.2f;
    public bool alarmOn;

    private float targetIntensity;

    void Awake()
    {
        GetComponent<Light>().intensity = 0;
        targetIntensity = highIntensity;
    }

    void Update()
    {
        var light = GetComponent<Light>();
        if (alarmOn)
        {
            light.intensity = Mathf.Lerp(light.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
            CheckTargetIntensity();
        }
        else
        {
            light.intensity = Mathf.Lerp(light.intensity, 0, fadeSpeed * Time.deltaTime);
        }
    }

    void CheckTargetIntensity()
    {
        if (Mathf.Abs(targetIntensity - GetComponent<Light>().intensity) < changeMargin)
        {
            if (targetIntensity == highIntensity)
            {
                targetIntensity = lowIntensity;
            }
            else
            {
                targetIntensity = highIntensity;
            }
        }
    }
}
