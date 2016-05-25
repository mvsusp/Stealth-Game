using UnityEngine;
using System.Collections;

public class LaserBlinking : MonoBehaviour {
    public float OnTime;
    public float OffTime;

    public float timer;

    void Update()
    {
        timer += Time.deltaTime;

        var renderer = GetComponent<MeshRenderer>();
        if (renderer.enabled && timer >= OnTime)
        {
            SwitchBeam();
        }

        if (!renderer.enabled && timer >= OnTime)
        {
            SwitchBeam();
        }
    }

    void SwitchBeam()
    {
        timer = 0;

        var renderer = GetComponent<MeshRenderer>();
        var light = GetComponent<Light>();

        renderer.enabled = !renderer.enabled;
        light.enabled = !light.enabled;
    }
}
