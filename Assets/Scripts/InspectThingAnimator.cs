using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InspectThingAnimator : CustomButtonBase
{
    [SerializeField] private Image infoSlider;
    [SerializeField] private GameObject planetInfo;
    [SerializeField] private TextMeshProUGUI Nametext;
    [SerializeField] private TextMeshProUGUI InfoText;
    [SerializeField] private GameObject PurchasedText;
    [SerializeField] private GameObject PurchaseButton;

    private Planet planetData;

    [SerializeField] private float typewriterSpeed = 15f;
    private Tween typeWriterTween;

    private void Awake()
    {
        planetData = GetComponentInParent<Planet>(); 
        if (planetData == null)
        {
            Debug.LogError("No Planet found");
        }
    }

    private void Start()
    {
        PurchaseButton.GetComponent<ChangeText>().toText = "$" + planetData.unlockPrice + "K";
    }

    private void Update()
    {
        if (planetData.unlocked)
        {
            PurchaseButton.SetActive(false);
            PurchasedText.SetActive(true);
        }
        else
        {
            PurchaseButton.SetActive(true);
            PurchasedText.SetActive(false);
        }
    }

    public void EnterAnimation()
    {
        gameObject.SetActive(true);
        //Debug.Log("Enter animation");

        PurchaseButton.SetActive(!planetData.unlocked);
        PurchaseButton.GetComponent<ChangeText>().toText = "$" + planetData.unlockPrice + "K";
        PurchasedText.SetActive(planetData.unlocked);
        
        DisplayText(planetData.description, InfoText);
        DisplayText(planetData.planetName, Nametext);

        infoSlider.DOFillAmount(planetInfo.GetComponent<RectTransform>().rect.width / infoSlider.GetComponent<RectTransform>().rect.width + 0.3f, infoSlider.GetComponent<RectTransform>().rect.width * 0.05f / typewriterSpeed)
            .OnComplete(() =>
            {
                infoSlider.fillAmount = planetInfo.GetComponent<RectTransform>().rect.width / infoSlider.GetComponent<RectTransform>().rect.width + 0.3f;
            });
    }

    public void ExitAnimation()
    {
        //Debug.Log("Exit animation");
        typeWriterTween.Kill();
        //Nametext.text = "";
        //InfoText.text = "";
        gameObject.SetActive(false);

        infoSlider.DOFillAmount(0, infoSlider.GetComponent<RectTransform>().rect.width * 0.05f / typewriterSpeed)
            .OnComplete(() => { 
                infoSlider.fillAmount = 0;
                //gameObject.SetActive(false);
                //Debug.Log("Disabled GO");
            });
        //PurchaseButton.SetActive(false);
        //PurchasedText.SetActive(false);
    }

    void DisplayText(string fullText, TextMeshProUGUI textField)
    {
        string text = "";
        typeWriterTween = DOTween.To(() => text, x => text = x, fullText, fullText.Length / typewriterSpeed).OnUpdate(() =>
        {
            // called every frame as long as the tween is running
            textField.text = text;
        });
    }
}
