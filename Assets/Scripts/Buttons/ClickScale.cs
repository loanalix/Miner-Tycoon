using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ClickScale : CustomButtonBase
{
    private const float OriginalScale = 1.0f;
    [SerializeField] private float toScale = 1.1f;
    [SerializeField] private float duration = 0.2f;
    private bool zoomed = false;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (zoomed == false)
        {
            Debug.Log("Zoom in");
            transform.DOScale(toScale, duration)
                .SetEase(Ease.InOutSine);

            zoomed = true;
        } else
        {
            Debug.Log("Zoom out");
            transform.DOScale(OriginalScale, duration)
                .SetEase(Ease.InOutSine);

            zoomed = false;
        }
    }
}
