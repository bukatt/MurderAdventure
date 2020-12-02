using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransition : MonoBehaviour
{
    public GameObject from;
    public GameObject to;

    public void Transition()
    {
        from.SetActive(false);
        to.SetActive(true);
    }

    public void TurnOff()
    {
        from.SetActive(false);
        to.SetActive(false);
    }
}
