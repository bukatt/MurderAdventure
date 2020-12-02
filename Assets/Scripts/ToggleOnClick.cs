using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleOnClick : MonoBehaviour
{

    public TMP_Text toChange;

    public string primaryValue;
    public string secondaryValue;
    private string currentValue;

    public void Awake()
    {
        currentValue = primaryValue;
        toChange.text = currentValue;
    }

    public void Toggle()
    {
        if(currentValue == primaryValue)
        {
            currentValue = secondaryValue;
        } else
        {
            currentValue = primaryValue;
        }
        toChange.text = currentValue;
    }
}
