using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnableUtil : MonoBehaviour
{
    public void Disable(GameObject go)
    {
        foreach (Transform c in go.transform)
        {
            c.gameObject.SetActive(false);
        }
        go.SetActive(false);
    }

    public void Enable(GameObject go)
    {
        foreach (Transform c in go.transform)
        {
            c.gameObject.SetActive(true);
        }
        go.SetActive(true);
    }
}
