using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObject : MonoBehaviour
{
    public void Disable(GameObject go)
    {
        foreach(Transform c in go.transform)
        {
            c.gameObject.SetActive(false);
        }
        go.SetActive(false);
    }
}
