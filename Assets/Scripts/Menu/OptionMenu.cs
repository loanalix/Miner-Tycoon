using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    public GameObject optionsMenuUI;
    public Slider volumeSlide;      
    public Slider musicVolumeSlide;  
    public AudioManager audioManager;   


    private void Awake()
    {
        if (volumeSlide == null || musicVolumeSlide == null)
        {
            Debug.LogError("Les sliders ne sont pas assign�s !");
            return;
        }

        Debug.Log("AudioListener.volume au d�but : " + AudioListener.volume);

        volumeSlide.value = AudioListener.volume;
        musicVolumeSlide.value = audioManager.musicSource.volume; 

        volumeSlide.onValueChanged.AddListener(SetVolume);
        musicVolumeSlide.onValueChanged.AddListener(SetMusicVolume);
    }

    public void LoadOption()
    {
        if (volumeSlide != null)
        {
            volumeSlide.value = AudioListener.volume;
        }
        if (musicVolumeSlide != null)
        {
            musicVolumeSlide.value = audioManager.musicSource.volume;
        }

        optionsMenuUI.SetActive(true);
        Debug.Log("Menu des options charg� avec volume : " + AudioListener.volume);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        //Debug.Log("Volume global r�gl� � : " + volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioManager.SetMusicVolume(volume); 
        //Debug.Log("Volume musique r�gl� � : " + volume);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Jeu r�initialis�.");
    }

    public void BackToPauseMenu()
    {
        optionsMenuUI.SetActive(false);
        Debug.Log("Retour au menu pause.");
    }
}
