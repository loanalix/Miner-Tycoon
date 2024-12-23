using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ConsoleManager : MonoBehaviour
{
    public static ConsoleManager instance { get; private set; }
    [SerializeField] private TextMeshProUGUI console;
    [SerializeField] private string defaultText = "> Hello World";
    private ScrollRect scrollView;

    private void Awake()
    {
        // Gestion du singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        scrollView = GetComponent<ScrollRect>();
        if (scrollView == null)
        {
            Debug.LogError("ConsoleScrollView not found");
        }
    }

    void Start()
    {
        // clean text
        Clean();
        console.text = defaultText;

        InvokeRepeating("BlinkConsole", 1f, 1f);
    }


    public void Clean()
    {
        console.text = "";
    }

    void BlinkConsole()
    {
        if (console.text[console.text.Length - 1] != '█')
        {
            console.text += '█';
        }
        else
        {
            console.text = console.text.Remove(console.text.Length - 1);
        }
    }

    public void Log(string message)
    {
        // remove █ from previous messages
        console.text = console.text.Replace("█", "");
        console.text += "\n> " + message;
        ScrollToBottom();
    }
    private void ScrollToBottom()
    {
        // Force the ScrollRect to scroll to the bottom
        Canvas.ForceUpdateCanvases(); // Ensure the layout updates first
        scrollView.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }
}
