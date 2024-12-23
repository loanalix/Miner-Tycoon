using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;
using UnityEngine.UI;

public class CustomCursor : CustomButtonBase
{
    [SerializeField] private Texture2D clickableCursorTexture;
    [SerializeField] private Texture2D disabledCursorTexture;
    [SerializeField] private Vector2 cursorHotspot = Vector2.zero;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if(GetComponent<Button>().interactable)
            Cursor.SetCursor(clickableCursorTexture, cursorHotspot, CursorMode.Auto);
        else
            Cursor.SetCursor(disabledCursorTexture, cursorHotspot, CursorMode.Auto);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        Cursor.SetCursor(null, cursorHotspot, CursorMode.Auto);
}
}
