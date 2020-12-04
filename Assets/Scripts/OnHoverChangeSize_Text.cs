using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class OnHoverChangeSize_Text : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public float hoverSize = 1.2f;
    public float duration = 1f;
    public TMP_Text text;
    public LeanTweenType easeType;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //text.fontSize = hoverSize;
        LeanTween.scale(gameObject, new Vector2(hoverSize, hoverSize), duration).setEase(easeType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //text.fontSize = defaultSize;
        LeanTween.scale(gameObject, new Vector2(1, 1), duration);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


}
