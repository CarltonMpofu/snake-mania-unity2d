using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip eatSound;
    [SerializeField] AudioClip largeSound;
    [SerializeField] AudioClip chainSound;

    AudioSource audioSource;
    GameManager gameManager;
    
    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }
    
    public void PlayClickSound()
    {
        if(gameManager.IsSoundOn())
        {
            audioSource.Stop();
            audioSource.clip = clickSound;
            audioSource.Play();
        }
    }

    public void PlayEatSound()
    {
        if(gameManager.IsSoundOn())
        {
            audioSource.Stop();
            audioSource.clip = eatSound;
            audioSource.Play();
        }
    }

    public void PlayLargeSound()
    {
        if(gameManager.IsSoundOn())
        {
            audioSource.Stop();
            audioSource.clip = largeSound;
            audioSource.Play();
        }
    }

    public void PlayChainSound()
    {
        if(gameManager.IsSoundOn())
        {
            audioSource.Stop();
            audioSource.clip = chainSound;
            audioSource.Play();
        }
    }
}
