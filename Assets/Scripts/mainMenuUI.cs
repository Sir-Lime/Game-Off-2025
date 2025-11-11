using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXslider; 

    void Start() {
        if (!PlayerPrefs.HasKey("MASTER")) {
            PlayerPrefs.SetFloat("MASTER", 1);
            PlayerPrefs.SetFloat("SFX", 1);
            PlayerPrefs.SetFloat("MUSIC", 1);
        }
        masterSlider.value = PlayerPrefs.GetFloat("MASTER");
        musicSlider.value = PlayerPrefs.GetFloat("MUSIC");
        SFXslider.value = PlayerPrefs.GetFloat("SFX");
    }

    void Update() {

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
    public void changeMasterSlider() {
        PlayerPrefs.SetFloat("MASTER",masterSlider.value);
        // lime pls do the sounds shit 
    }
    public void changeMusicSlider() {
        PlayerPrefs.SetFloat("MUSIC", musicSlider.value);
        // lime pls do the sounds shit 
    }
    public void changeSFXslider() {
        PlayerPrefs.SetFloat("SFX", SFXslider.value); 
        // lime pls do the sounds shit 
    }
}
