using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIscript : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    private bool paused = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsPanel;
   // [SerializeField] private GameObject leaderboardPanel;
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

    // Update is called once per frame
    void Update() {
        //  JumpDown = input.actions["Jump"].WasPressedThisFrame(),
        if (input.actions["Pause"].WasPressedThisFrame()) 
        {
            Debug.Log("Pause Pressed");
            if (paused) {
                resumeButton(); 
            } 
            else 
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0; paused = true;
            }
        }
       /* if (input.actions["Leaderboard"].WasPerformedThisFrame() && paused) 
        {
            if (!leaderboardPanel.activeSelf)
            {
                leaderboardButton();
            }
            else
            {
                quitLeaderboard();
            }
        }*/
    }

    public void resumeButton() {
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(false);
       // leaderboardPanel.SetActive(false);
        Time.timeScale = 1; paused = false;
    }
    public void settingsButton() {
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(true); 
    }
    public void leaderboardButton() {
        pauseMenu.SetActive(false); 
     //   leaderboardPanel.SetActive(true);
    }
    public void quitToMain() 
    {
        resumeButton();
        SceneManager.LoadScene(0); 
    }

    public void changeMasterSlider()
    {
        PlayerPrefs.SetFloat("MASTER", masterSlider.value);
        // lime pls do the sounds shit 
    }
    public void changeMusicSlider()
    {
        PlayerPrefs.SetFloat("MUSIC", musicSlider.value);
        // lime pls do the sounds shit 
    }
    public void changeSFXslider()
    {
        PlayerPrefs.SetFloat("SFX", SFXslider.value);
        // lime pls do the sounds shit 
    }
    public void quitSettings() {
        pauseMenu.SetActive(true);
        settingsPanel.SetActive(false);
    }
        public void quitLeaderboard() 
        {
      //  leaderboardPanel.SetActive(false); 
        pauseMenu.SetActive(true);
        }
}
