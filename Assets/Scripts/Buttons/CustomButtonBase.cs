using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class CustomButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Pointer enter");

        // Set cursor
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Pointer click");

        // Play audio
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Pointer exit");

        // Set cursor
    }
}
