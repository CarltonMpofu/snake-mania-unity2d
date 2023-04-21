using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayThemeSong : MonoBehaviour
{
    GameManager gameManager;

    private void Awake() 
    {
        int playThemeSongScriptCount = FindObjectsOfType<PlayThemeSong>().Length;

        if(playThemeSongScriptCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if(gameManager.IsSoundOn())
        {
            StartSong();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopSong()
    {
        GetComponent<AudioSource>().Stop();
    }

    public void StartSong()
    {
        GetComponent<AudioSource>().Play();
    }
}
