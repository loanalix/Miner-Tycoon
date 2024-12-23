using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CustomButtonScale : CustomButtonBase
{
    private const float OriginalScale = 1.0f;
    [SerializeField] private float toScale = 1.1f;
    [SerializeField] private float duration = 0.2f;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        transform.DOScale(toScale, duration)
            .SetEase(Ease.InOutSine);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        transform.DOScale(OriginalScale, duration)
            .SetEase(Ease.InOutSine);
    }
}
