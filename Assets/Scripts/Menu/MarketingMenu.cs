using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketingMenu : MonoBehaviour
{
    [SerializeField] private GameObject marketingPanel; 
    [SerializeField] private GameObject statPanel; 
    [SerializeField] private GameObject globalUgradePanel; 
    [SerializeField] private GameObject summaryPanel; 
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private float slideSpeed = 1f; 
    [SerializeField] private float panelAppearSpeed = 0.5f;

    private Vector3 initialPosition;
    private Vector3 targetPosition;


    void Start()
    {
        initialPosition = marketingPanel.transform.position;
        targetPosition = new Vector3(0, marketingPanel.transform.position.y, marketingPanel.transform.position.z);

        marketingPanel.transform.position = new Vector3(-Screen.width, marketingPanel.transform.position.y, marketingPanel.transform.position.z);

        statPanel.SetActive(false);
        globalUgradePanel.SetActive(false);
        summaryPanel.SetActive(false);
    }

    public void OnMarketingButtonClicked()
    {
        marketingPanel.SetActive(true);
        audioManager.ChangeMusic(audioManager.MenuMusic);

        StartCoroutine(SlideInPanel());
    }

    public void OnCloseButtonClicked()
    {
        audioManager.ChangeMusic(audioManager.backgroundMusic);
        StartCoroutine(FadeOutPanelsAndSlideOut());
    }

    private IEnumerator SlideInPanel()
    {
        float elapsedTime = 0f;

        while (elapsedTime < slideSpeed)
        {
            marketingPanel.transform.position = Vector3.Lerp(marketingPanel.transform.position, targetPosition, elapsedTime / slideSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        marketingPanel.transform.position = targetPosition;

        StartCoroutine(ShowPanels());
    }

    private IEnumerator FadeOutPanelsAndSlideOut()
    {
        yield return StartCoroutine(FadeOutPanel(statPanel));
        statPanel.SetActive(false);

        yield return StartCoroutine(FadeOutPanel(globalUgradePanel));
        globalUgradePanel.SetActive(false);

        yield return StartCoroutine(FadeOutPanel(summaryPanel));
        summaryPanel.SetActive(false);

        StartCoroutine(SlideOutPanel());
    }

    private IEnumerator SlideOutPanel()
    {
        float elapsedTime = 0f;
        Vector3 offScreenPosition = new Vector3(-Screen.width, marketingPanel.transform.position.y, marketingPanel.transform.position.z);

        while (elapsedTime < slideSpeed)
        {
            marketingPanel.transform.position = Vector3.Lerp(marketingPanel.transform.position, offScreenPosition, elapsedTime / slideSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        marketingPanel.transform.position = offScreenPosition;
        marketingPanel.SetActive(false);
    }

    private IEnumerator ShowPanels()
    {
        yield return new WaitForSeconds(0.2f);

        statPanel.SetActive(true);
        yield return StartCoroutine(FadeInPanel(statPanel));

        globalUgradePanel.SetActive(true);
        yield return StartCoroutine(FadeInPanel(globalUgradePanel));

        summaryPanel.SetActive(true);
        yield return StartCoroutine(FadeInPanel(summaryPanel));
    }

    private IEnumerator FadeInPanel(GameObject panel)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = panel.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < panelAppearSpeed)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / panelAppearSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOutPanel(GameObject panel)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = panel.AddComponent<CanvasGroup>();
        }

        float elapsedTime = 0f;

        while (elapsedTime < panelAppearSpeed)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / panelAppearSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}


