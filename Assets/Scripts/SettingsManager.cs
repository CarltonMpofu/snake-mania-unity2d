using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI controlsText;
    [SerializeField] Button controlsButton;
    [SerializeField] Button customizeButton;

    Toggle toggle;
    GameManager gameManager;

    Button button;

    bool settingUp;

    private void Awake() 
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = false;
        //toggle.interactable = false;
    }
    private void Start() 
    {
        // Debug.Log("Started");
        settingUp = true;
        //toggle = GetComponent<Toggle>();

        if(toggle)
            toggle.onValueChanged.AddListener(OnToggleValueChanged);

        // if(!toggle)
        //     Debug.Log("NO TOGGLE FOUND");

        gameManager = FindObjectOfType<GameManager>();

        if(!gameManager)
            Debug.Log("NO GAME MANAGER FOUND");

            
        if(gameObject.CompareTag("ToggleVibration"))
            toggle.isOn = gameManager.IsVibrationOn();
        else if(gameObject.CompareTag("ToggleSound"))
            toggle.isOn = gameManager.IsSoundOn();
        
        settingUp = false;
    }

    private void OnToggleValueChanged(bool isOn)
    {
         if(gameObject.CompareTag("ToggleVibration"))
        {
            // Debug.Log("Full screen");
            
            gameManager.SetIsVibrationOn(isOn);
            if(!settingUp)
                FindObjectOfType<SoundManager>().PlayClickSound();

            
        }
        else if(gameObject.CompareTag("ToggleSound"))
        {
            // Debug.Log("Full screen");
            gameManager.SetIsSoundOn(isOn);
            if(!settingUp)
            {
                FindObjectOfType<SoundManager>().PlayClickSound();

                if(isOn)
                    FindObjectOfType<PlayThemeSong>().StartSong();
                else
                    FindObjectOfType<PlayThemeSong>().StopSong();
            }
        }
    }

    private void ShowChangeControlsOption(bool isOn)
    {
        if(isOn)
        {

        }
        else
        {

        }
    }
    public void SaveJoyPadPosition()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.SetChangeDpadPosition(false);

        Joypad[] joypads = FindObjectsOfType<Joypad>();

        foreach(Joypad joypad in joypads)
        {
            if(joypad.gameObject.CompareTag("Up"))
            {
                gameManager.SetTopPadPositon(joypad.transform.position);
                // Debug.Log("Save 1");
            }
            else if(joypad.gameObject.CompareTag("Down"))
            {
                gameManager.SetBottomPadPositon(joypad.transform.position);
                // Debug.Log("Save 2");
            }
            else if(joypad.gameObject.CompareTag("Right"))
            {
                gameManager.SetRightPadPositon(joypad.transform.position);
                // Debug.Log("Save 3");
            }
            else if(joypad.gameObject.CompareTag("Left"))
            {
                gameManager.SetLeftPadPositon(joypad.transform.position);
                // Debug.Log("Save 4");
            }
        }

        SceneManager.LoadScene("Settings Scene");
    }

    public void ResetJoyPadPositions()
    {
        FindObjectOfType<SoundManager>().PlayClickSound();
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.ResetPadPositions();
        gameManager.SetChangeDpadPosition(false);

        Joypad[] joypads = FindObjectsOfType<Joypad>();

        foreach(Joypad joypad in joypads)
        {
            if(joypad.gameObject.CompareTag("Up"))
            {
                joypad.transform.position = gameManager.GetTopPadPositon();
                // Debug.Log("Save 1");
            }
            else if(joypad.gameObject.CompareTag("Down"))
            {
                joypad.transform.position = gameManager.GetBottomPadPositon();
                // Debug.Log("Save 2");
            }
            else if(joypad.gameObject.CompareTag("Right"))
            {
                joypad.transform.position = gameManager.GetRightPadPositon();
                // Debug.Log("Save 3");
            }
            else if(joypad.gameObject.CompareTag("Left"))
            {
                joypad.transform.position = gameManager.GetLeftPadPositon();
                // Debug.Log("Save 4");
            }
        }
    }

   
}
