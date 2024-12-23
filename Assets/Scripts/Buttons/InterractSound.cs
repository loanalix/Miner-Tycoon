using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class InterractSound : CustomButtonBase
{
    [SerializeField] private AudioClip hoverEnterSound;
    [SerializeField] private AudioClip hoverExitSound;
    [SerializeField] private AudioClip clickSound;
    private AudioManager _audioManager;

    void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>(); 
        if (_audioManager == null)
            Debug.LogError("Aucun AudioManager trouvé dans la scène.");
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (hoverEnterSound != null)
        {
            _audioManager.PlaySound(hoverEnterSound); 
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (GetComponent<Button>().interactable)
            _audioManager.PlaySound(clickSound); 
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (hoverExitSound != null)
            _audioManager.PlaySound(hoverExitSound);
    }
}

