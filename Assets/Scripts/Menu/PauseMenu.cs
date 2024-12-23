using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;  
    public GameObject pauseMenuUI;                  

    public AudioManager audioManager;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        if (optionMenu.activeSelf)
        {
            optionMenu.SetActive(false);

        }
        else
        {
            pauseMenuUI.SetActive(false);
            isPaused = false;
            audioManager.ChangeMusic(audioManager.backgroundMusic);
        }

        Debug.Log("Jeu repris.");
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        optionMenu.SetActive(false); 
        isPaused = true;
        audioManager.ChangeMusic(audioManager.MenuMusic);
        Debug.Log("Jeu en pause.");
    }

    public void LoadOption()
    {
        optionMenu.SetActive(true);

        OptionMenu optionMenuScript = optionMenu.GetComponent<OptionMenu>();
        if (optionMenuScript != null)
        {
            optionMenuScript.LoadOption();

            audioManager.ChangeMusic(audioManager.MenuMusic);

        }
        else
        {
            Debug.LogError("Script OptionMenu introuvable sur l'objet optionMenu !");
        }

        Debug.Log("Menu des options chargé.");
    }

}