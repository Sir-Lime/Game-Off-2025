using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class mainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXslider; 
    [SerializeField] private VolumeManager volumeManager;
    public TMP_InputField nameField;
    public GameObject nameFieldObject;

    private PlayerInput input;

    void Start() 
    {
        if (!PlayerPrefs.HasKey("MASTER")) {
            PlayerPrefs.SetFloat("MASTER", 1);
            PlayerPrefs.SetFloat("SFX", 1);
            PlayerPrefs.SetFloat("MUSIC", 1);
        }
        masterSlider.value = PlayerPrefs.GetFloat("MASTER");
        musicSlider.value = PlayerPrefs.GetFloat("MUSIC");
        SFXslider.value = PlayerPrefs.GetFloat("SFX");
        input = gameObject.GetComponent<PlayerInput>();
    }

    void Update() 
    {
        if(input.actions["Leaderboard"].WasPerformedThisFrame()) 
        {
            if (!leaderboardPanel.activeSelf)
            {
                leaderboardButton();
            }
            else
            {
                quitLeaderboardButton();
            }
        }
    }

    public void playButton() {
        SceneManager.LoadScene(1); 
    }
    public void settingsButton() {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void leaderboardButton() {
        mainPanel.SetActive(false); 
        leaderboardPanel.SetActive(true);
    }
    public void creditsButton() {
        mainPanel.SetActive(false); 
        creditsPanel.SetActive(true);
    }
    public void quitButton() {
        Application.Quit();
    }
    public void quitSettingsButton() {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    public void quitCreditsButton() {
        creditsPanel.SetActive(false); 
        mainPanel.SetActive(true);
    }
    public void quitLeaderboardButton() {
        leaderboardPanel.SetActive(false); 
        mainPanel.SetActive(true);
    }
    public void changeMasterSlider() {
        PlayerPrefs.SetFloat("MASTER",masterSlider.value);
        volumeManager.SetMasterVolume();
    }
    public void changeMusicSlider() {
        PlayerPrefs.SetFloat("MUSIC", musicSlider.value);
        volumeManager.SetMusicVolume();
    }
    public void changeSFXslider() {
        PlayerPrefs.SetFloat("SFX", SFXslider.value); 
        volumeManager.SetSFXVolume();
    }
    public void StoreInputText()
    {
        PlayerPrefs.SetString("Name", nameField.text);
        Debug.Log( PlayerPrefs.GetString("Name"));
        string userName = PlayerPrefs.GetString("Name", "nullName");
        if (userName != "nullName")
        {
            nameFieldObject.SetActive(false);
        }
    }
}
