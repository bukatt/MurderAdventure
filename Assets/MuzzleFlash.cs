using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField]
    private Light2D myLight;
    [SerializeField]
    private SpriteRenderer mySprite;
    public float flashTime;

    public void ActivateLight()
    {
        myLight.enabled = true;
        mySprite.enabled = true;
        StartCoroutine(DeactivateRoutine());
    }
    IEnumerator DeactivateRoutine()
    {
        yield return new WaitForSeconds(flashTime);
        DeactivateLight();
    }

    public void DeactivateLight()
    {
        myLight.enabled = false;
        mySprite.enabled = false;
    }
}
