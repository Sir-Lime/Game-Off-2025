using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer master;
    [SerializeField] private Slider Masterslider;
    [SerializeField] private Slider Musicslider;
    [SerializeField] private Slider SFXslider;

    private void Start()
    {
        LoadVolume();
    }   
    public void SetMasterVolume()
    {
        float volume = Masterslider.value;
        master.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MASTER", volume);
    }
    public void SetSFXVolume()
    {
        float volume = SFXslider.value;
        master.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX", volume);
    }
    public void SetMusicVolume()
    {
        float volume = Musicslider.value;
        master.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MUSIC", volume);
    }

    public void LoadVolume()
    {
        float masterVol, musicVol, SFXVol;
        if (!PlayerPrefs.HasKey("MASTER") || !PlayerPrefs.HasKey("MUSIC") || !PlayerPrefs.HasKey("SFX"))
            masterVol = musicVol = SFXVol = 1;
        else
        {
            masterVol = PlayerPrefs.GetFloat("MASTER");
            musicVol = PlayerPrefs.GetFloat("MUSIC");
            SFXVol = PlayerPrefs.GetFloat("SFX");
        }

        Masterslider.value = masterVol;
        Musicslider.value = musicVol;
        SFXslider.value = SFXVol;
    }

}
