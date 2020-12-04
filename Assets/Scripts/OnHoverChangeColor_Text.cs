using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class OnHoverChangeColor_Text : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color defaultColor;
    public Color hoverColor;

    public TMP_Text text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = defaultColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        text.color = defaultColor;
    }


}
