using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, Vector3.one * 1.3f, 0.5f).setEaseOutCubic();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, Vector3.one, 0.5f).setEaseOutCubic();
    }
}
