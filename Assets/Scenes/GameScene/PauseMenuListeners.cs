using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuListeners : MonoBehaviour
{
    private GUIManager guiManager;

    private Button resumeButton;
    private Button settingsButton;
    private Button quitButton;

    void Start()
    {
        guiManager = GameObject.Find("GUIManager").GetComponent<GUIManager>();

        resumeButton = transform.Find("Buttons/Resume").GetComponent<Button>();
        settingsButton = transform.Find("Buttons/Settings").GetComponent<Button>();
        quitButton = transform.Find("Buttons/Quit").GetComponent<Button>();

        resumeButton.onClick.AddListener(OnResumeButtonClick);
        settingsButton.onClick.AddListener(OnSettingsButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    
    void Update()
    {
        
    }

    void OnResumeButtonClick()
    {
        gameObject.SetActive(false);
        guiManager.actionable = true;
    }

    void OnSettingsButtonClick()
    {

    }

    void OnQuitButtonClick()
    {

    }
}
