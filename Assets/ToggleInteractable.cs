using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleInteractable : MonoBehaviour
{
    private TMP_InputField input;

    private void Awake()
    {
        input = GetComponent<TMP_InputField>();
    }

    public void Toggle()
    {
        if (input.interactable)
        {
            input.interactable = false;
        } else
        {
            input.interactable = true;
        }
    }
}
