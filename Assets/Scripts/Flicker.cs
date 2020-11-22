using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental;
using UnityEngine.Experimental.Rendering.Universal;

public class Flicker : MonoBehaviour
{
    public Light2D myLight;
    private float random;
    public float minIntensity;
    public float maxIntensity;
    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0.0f, 65535.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float noise = Mathf.PerlinNoise(random, Time.time);
        myLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}
