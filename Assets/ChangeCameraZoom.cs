using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCameraZoom : MonoBehaviour
{
    public float[] distances;
    private int index = 0;
    private CinemachineVirtualCamera cvc;
    private void Start()
    {
        cvc = GameObject.FindGameObjectWithTag(Constants.Tags.virtualCamera).GetComponent<CinemachineVirtualCamera>();
    }

    public void toggleCameraZoom()
    {
        Debug.Log("toggling cam zoom");
        if(distances.Length > 0)
        {
            cvc.m_Lens.OrthographicSize = distances[index];
            if(index >= distances.Length - 1)
            {
                index = 0;
            } else
            {
                index++;
            }
        }
    }
}
