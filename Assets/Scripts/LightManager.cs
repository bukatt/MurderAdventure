using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class LightManager : MonoBehaviour
{
    public bool lightOn;
    private bool lightTurningOff = false;
    private bool lightTurningOn = false;
    public float toggleSpeed = .01f;

    public Light2D myLight;
    private float random;
    public float minIntensity;
    public float maxIntensity;

    private float minIntensityFlex;
    private float maxIntensityFlex;

    void Start()
    {
        random = Random.Range(0.0f, 65535.0f);
        minIntensityFlex = minIntensity;
        maxIntensityFlex = maxIntensity;
    }

    void Update()
    {
        if (lightTurningOn)
        {
            minIntensityFlex += Time.deltaTime * toggleSpeed;
            maxIntensityFlex += Time.deltaTime * toggleSpeed;
            if (minIntensityFlex >= minIntensity)
            {
                minIntensityFlex = minIntensity;
            }

            if (maxIntensityFlex >= maxIntensity)
            {
                maxIntensityFlex = maxIntensity;
            }

            if(maxIntensity == maxIntensityFlex && minIntensity == minIntensityFlex)
            {
                lightTurningOn = false;
            }

        } else if (lightTurningOff)
        {
            minIntensityFlex -= Time.deltaTime * toggleSpeed;
            maxIntensityFlex -= Time.deltaTime * toggleSpeed;
            if (minIntensityFlex <= 0)
            {
                minIntensityFlex = 0;
            }

            if (maxIntensityFlex <= 0)
            {
                maxIntensityFlex = 0;
            }

            if (0 == maxIntensityFlex && 0 == minIntensityFlex)
            {
                lightTurningOff = false;
            }
        }
        float noise = Mathf.PerlinNoise(random, Time.time);
        myLight.intensity = Mathf.Lerp(minIntensityFlex, maxIntensityFlex, noise);
    }

    public void ToggleLight()
    {
        if (myLight.intensity > 0 && !lightTurningOff && !lightTurningOn)
        {
            lightTurningOff = true;
        } else if (myLight.intensity == 0 && !lightTurningOn && !lightTurningOff)
        {
            lightTurningOn = true;
        }
    }
}
