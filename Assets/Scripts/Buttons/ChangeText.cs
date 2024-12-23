using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Rendering;

public class ChangeText : CustomButtonBase
{
    private string _originalText;
    public string toText = "HoverText";

    private bool hover = false; 

    [SerializeField] private TMP_Text tmp;
    

    private void Awake()
    {
        if(tmp == null)
            tmp = GetComponentInChildren<TMP_Text>();
        _originalText = tmp.text;
    }

    private void Update()
    {

        if (hover)
        {
            tmp.text = toText;
        }
        else
        {
            tmp.text = _originalText;
        }


    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
        base.OnPointerEnter(eventData);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
        base.OnPointerExit(eventData);
       
    }

}
