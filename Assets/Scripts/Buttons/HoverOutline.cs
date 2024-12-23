using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class HoverOutline : CustomButtonBase
{
    [SerializeField] private float toScale = 1.1f;
    [SerializeField] private float duration = 0.2f;
    private GameObject outline;
    [SerializeField] UnityEvent onHoverEnd;
    [SerializeField] UnityEvent onExitEnd;

    private void Awake()
    {
        outline = transform.GetChild(0).gameObject; // Assuming the outline is the first child
        outline.transform.localScale = Vector3.zero;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        onHoverEnd.Invoke();
        outline.transform.DOScale(toScale, duration).SetEase(Ease.InOutSine)
            //.OnComplete(onHoverEnd.Invoke)
            ;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        onExitEnd.Invoke();
        outline.transform.DOScale(0, duration)
            .SetEase(Ease.InOutSine)
            //.OnComplete(onExitEnd.Invoke)
            ;
    }
}
