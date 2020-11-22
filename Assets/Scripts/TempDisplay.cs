using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDisplay : MonoBehaviour
{
    public float displayTime;

    public void Activate()
    {
        StartCoroutine(DeactivateRoutine());
    }
    IEnumerator DeactivateRoutine()
    {
        yield return new WaitForSeconds(displayTime);
        Deactivate();
    }

    public void Deactivate()
    {
        
    }
}
