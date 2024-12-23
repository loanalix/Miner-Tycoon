using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using NUnit.Framework;
using UnityEngine.Events;

public class ClickZoom : CustomButtonBase
{
    private GameObject viewport;

    private float OriginalZoom = 1.0f;
    [SerializeField] private float toZoom = 5f;
    [SerializeField] private float duration = 0.2f;
    private bool zoomed = false;
    [SerializeField] UnityEvent onZoom;
    [SerializeField] UnityEvent onDezoom;

    private void Awake()
    {
        viewport = GameObject.FindWithTag("PlanetsViewport");
        if(viewport == null)
        {
            Debug.LogError("PlanetsViewport not found");
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        //base.OnPointerClick(eventData);

        if (zoomed == false)
        {
            // move the camera to the target position
            //mainCamera.transform.DOMove(transform.position, duration)
                //.SetEase(Ease.InOutSine);

            //Debug.Log("Zoom in");
            viewport.transform.DOScale(toZoom, duration)
                .SetEase(Ease.InOutSine).OnComplete(() => {
                    viewport.transform.localScale = new Vector3(toZoom, toZoom, toZoom);
                    viewport.GetComponent<ScrollZoom>().currentScale = toZoom;
                    onZoom.Invoke();
                });

            zoomed = true;
            viewport.GetComponent<ScrollZoom>().enabled = false;
        } else
        {
            //Debug.Log("Zoom out");
            viewport.transform.DOScale(OriginalZoom, duration)
                .SetEase(Ease.InOutSine).OnComplete(() => { 
                    viewport.transform.localScale = new Vector3(OriginalZoom, OriginalZoom, OriginalZoom);
                    viewport.GetComponent<ScrollZoom>().currentScale = OriginalZoom;
                    onDezoom.Invoke();
                });

            zoomed = false;
            viewport.GetComponent<ScrollZoom>().enabled = true;
        }
    }
}
