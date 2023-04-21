using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Start() {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadSnakeScene()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SceneManager.LoadScene("Snake Scene");
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("Gameover Scene");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void LoadStartScene()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SceneManager.LoadScene("Start Scene");
    }

    public void LoadLevelScene()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SceneManager.LoadScene("Maze Scene");
    }

    public void LoadLevel1()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SceneManager.LoadScene("Level 99");
    }

    public void LoadLevel2()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SetupMazeScene setupMazeScene = FindObjectOfType<SetupMazeScene>();
        setupMazeScene.SetSelectedMazeIndex(0);
        
        if(setupMazeScene.IsSelectedMazeLocked() == false)
            SceneManager.LoadScene("Level 100");
        else
        {
            setupMazeScene.UnlockSelectedMaze(); 
        }
    }

    public void LoadLevel3()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SetupMazeScene setupMazeScene = FindObjectOfType<SetupMazeScene>();
        setupMazeScene.SetSelectedMazeIndex(1);
        
        if(setupMazeScene.IsSelectedMazeLocked() == false)
            SceneManager.LoadScene("Level 101");
        else
            setupMazeScene.UnlockSelectedMaze(); 

    }

    public void LoadLevel4()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SetupMazeScene setupMazeScene = FindObjectOfType<SetupMazeScene>();
        setupMazeScene.SetSelectedMazeIndex(2);
        
        if(setupMazeScene.IsSelectedMazeLocked() == false)
            SceneManager.LoadScene("Level 102");
        else
            setupMazeScene.UnlockSelectedMaze();
    }

    public void LoadLevel5()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SetupMazeScene setupMazeScene = FindObjectOfType<SetupMazeScene>();
        setupMazeScene.SetSelectedMazeIndex(3);
        
        if(setupMazeScene.IsSelectedMazeLocked() == false)
            SceneManager.LoadScene("Level 103");
        else
            setupMazeScene.UnlockSelectedMaze();
    }

    public void LoadLevel6()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SetupMazeScene setupMazeScene = FindObjectOfType<SetupMazeScene>();
        setupMazeScene.SetSelectedMazeIndex(4);
        
        if(setupMazeScene.IsSelectedMazeLocked() == false)
            SceneManager.LoadScene("Level 104");
        else
            setupMazeScene.UnlockSelectedMaze();
    }

    public void LoadLevel7()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SetupMazeScene setupMazeScene = FindObjectOfType<SetupMazeScene>();
        setupMazeScene.SetSelectedMazeIndex(5);
        
        if(setupMazeScene.IsSelectedMazeLocked() == false)
            SceneManager.LoadScene("Level 105");
        else
            setupMazeScene.UnlockSelectedMaze();
    }

    public void LoadLevel8()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SetupMazeScene setupMazeScene = FindObjectOfType<SetupMazeScene>();
        setupMazeScene.SetSelectedMazeIndex(6);
        
        if(setupMazeScene.IsSelectedMazeLocked() == false)
            SceneManager.LoadScene("Level 106");
        else
            setupMazeScene.UnlockSelectedMaze();
    }

    public void LoadLevel9()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SetupMazeScene setupMazeScene = FindObjectOfType<SetupMazeScene>();
        setupMazeScene.SetSelectedMazeIndex(7);
        
        if(setupMazeScene.IsSelectedMazeLocked() == false)
            SceneManager.LoadScene("Level 107");
        else
            setupMazeScene.UnlockSelectedMaze();
    }

    public void LoadLevel10()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SetupMazeScene setupMazeScene = FindObjectOfType<SetupMazeScene>();
        setupMazeScene.SetSelectedMazeIndex(8);
        
        if(setupMazeScene.IsSelectedMazeLocked() == false)
            SceneManager.LoadScene("Level 108");
        else
            setupMazeScene.UnlockSelectedMaze();
    }

    public void LoadLevel11()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SetupMazeScene setupMazeScene = FindObjectOfType<SetupMazeScene>();
        setupMazeScene.SetSelectedMazeIndex(9);
        
        if(setupMazeScene.IsSelectedMazeLocked() == false)
            SceneManager.LoadScene("Level 109");
        else
            setupMazeScene.UnlockSelectedMaze();
    }

    public void LoadLevel12()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SetupMazeScene setupMazeScene = FindObjectOfType<SetupMazeScene>();
        setupMazeScene.SetSelectedMazeIndex(10);
        
        if(setupMazeScene.IsSelectedMazeLocked() == false)
            SceneManager.LoadScene("Level 110");
        else
            setupMazeScene.UnlockSelectedMaze();
    }

    public void LoadLevel13()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SetupMazeScene setupMazeScene = FindObjectOfType<SetupMazeScene>();
        setupMazeScene.SetSelectedMazeIndex(11);
        
        if(setupMazeScene.IsSelectedMazeLocked() == false)
            SceneManager.LoadScene("Level 111");
        else
            setupMazeScene.UnlockSelectedMaze();
    }

    public void LoadLevel14()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SetupMazeScene setupMazeScene = FindObjectOfType<SetupMazeScene>();
        setupMazeScene.SetSelectedMazeIndex(12);
        
        if(setupMazeScene.IsSelectedMazeLocked() == false)
            SceneManager.LoadScene("Level 112");
        else
            setupMazeScene.UnlockSelectedMaze();
    }

    public void LoadLevel15()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SetupMazeScene setupMazeScene = FindObjectOfType<SetupMazeScene>();
        setupMazeScene.SetSelectedMazeIndex(13);
        
        if(setupMazeScene.IsSelectedMazeLocked() == false)
            SceneManager.LoadScene("Level 113");
        else
            setupMazeScene.UnlockSelectedMaze();
    }

    public void LoadJoypadScene()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.SetChangeDpadPosition(true);
        SceneManager.LoadScene("Joypad Scene");

    }

    public void LoadSettingsScene()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SceneManager.LoadScene("Settings Scene");
    }

    public void ReloadScene()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
