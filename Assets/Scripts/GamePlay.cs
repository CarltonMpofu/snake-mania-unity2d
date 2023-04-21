using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{

    [SerializeField] bool playGame;

    [SerializeField] GameObject menuItemsCanvas;
    [SerializeField] GameObject gameCanvas;

    SceneLoader sceneLoader;

    PlayThemeSong playThemeSong;

    bool soundOn;

    // Start is called before the first frame update
    void Start()
    {
        playThemeSong = FindObjectOfType<PlayThemeSong>();
        if(!playThemeSong)
            Debug.LogError("NO PLAY THEME SONG SCRIPT FOUND!");
        else
            playThemeSong.StopSong();

        menuItemsCanvas.SetActive(false);

        playGame = true;

        sceneLoader = FindObjectOfType<SceneLoader>();

        soundOn = FindObjectOfType<GameManager>().IsSoundOn();
    }


    void Update()
    {
        //Debug.Log("Hello");
        if (Input.GetKeyDown(KeyCode.Escape) && playGame == true)
        {
            PausGame();

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && playGame == false)
        {
            UnPauseGame();
            //sceneLoader.LoadLevelScene();
        }
    }

    public bool IsGamePlaying()
    {
        return playGame;
    }

    public void PausGame()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        playGame = false;
        gameCanvas.GetComponent<GraphicRaycaster>().enabled = false;
        menuItemsCanvas.SetActive(true);
    }

    public void UnPauseGame()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        menuItemsCanvas.SetActive(false);
        gameCanvas.GetComponent<GraphicRaycaster>().enabled = true;
        playGame = true;
    }

    private void OnDestroy() 
    {
        if(soundOn)
            playThemeSong.StartSong();
    }

}
