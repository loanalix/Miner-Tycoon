using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; 
    public AudioSource musicSource; 
    public AudioClip backgroundMusic; 
    public AudioClip MenuMusic; 

    public float maxVolume = 1f;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>(); 
            if (audioSource == null)
                Debug.LogError("Aucun AudioSource trouvé pour les effets sonores sur l'AudioManager.");
        }

        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>(); 
            musicSource.loop = true; 
        }

        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic; 
            musicSource.Play(); 
        }
    }

    // Play sound effect
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip); 
        }
        else
        {
            Debug.LogWarning("Aucun AudioClip fourni.");
        }
    }

    // Set The Music Volume 
    public void SetMusicVolume(float volume)
    {
        maxVolume = volume;
        musicSource.volume = volume;
    }

    public void ChangeMusic(AudioClip newmusic)
    {
        if (newmusic != null)
        {
            musicSource.DOFade(0, 1).OnComplete(() =>
            {
                musicSource.clip = newmusic;
                musicSource.Play();
                musicSource.DOFade(maxVolume, 1);
            });
        }
        else
        {
            Debug.LogWarning("Aucun AudioClip fourni.");
        }
    }


    // Stop The Music
    public void StopMusic()
    {
        musicSource.Stop();
    }
}
