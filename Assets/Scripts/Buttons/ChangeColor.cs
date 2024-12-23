using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;
using UnityEngine.UI;

public class CustomButtonColor : CustomButtonBase
{
    private TMP_Text _buttonText;
    private Button _button;
    private Color _originalTextColor;
    [SerializeField] private Color toColorText = Color.red;

    private Color _originalBtnColor;
    [SerializeField] private Color toColorBtn = Color.white;

    [SerializeField] private float duration = 0.2f;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _buttonText = GetComponentInChildren<TMP_Text>();
        _originalTextColor = _buttonText.color;
        _originalBtnColor = _button.image.color;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        _buttonText.DOColor(toColorText, duration);
        _button.image.DOColor(toColorBtn, duration);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        _buttonText.DOColor(_originalTextColor, duration);
        _button.image.DOColor(_originalBtnColor, duration);
    }
}
