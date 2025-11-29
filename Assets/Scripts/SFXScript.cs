using UnityEngine;
using UnityEngine.UI;

public class SFXScript : MonoBehaviour
{
    public static SFXScript instance;
    [SerializeField] private AudioSource SFXObject;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip closeDoorSound;
    [SerializeField] private AudioClip openDoorSound;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip interactSound;
    [SerializeField] private AudioClip interact2Sound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip leverSound;
    [SerializeField] private AudioClip portalSound;
    [SerializeField] private AudioClip pressurePlateSound;
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private AudioClip spawnSound;
    [SerializeField] private AudioClip walkSound;    [SerializeField] private Slider Masterslider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;


    void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void PlaySFX(AudioClip clip, Transform spawnTrans, float volume = 1, float pitch = 1)
    {
        AudioSource audioSource = Instantiate(SFXObject, spawnTrans.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
    public void buttonSFX()
    {

        PlaySFX(buttonSound, transform, 0.5f, Random.Range(0.9f, 1.1f));
    }
    public void closeDoorSFX()
    {

        PlaySFX(closeDoorSound, transform, 1, Random.Range(0.8f, 1.2f));
    }
    public void openDoorSFX(float pitch = 1)
    {
        PlaySFX(openDoorSound, transform, 0.5f, pitch);
    }
    public void dashSFX()
    {

        PlaySFX(dashSound, transform, 0.8f, Random.Range(0.8f, 1.2f));
    }
    public void jumpSFX()
    {

        PlaySFX(jumpSound, transform, 0.8f, Random.Range(0.95f, 1.05f));
    }
    public void interactSFX()
    {
        PlaySFX(interactSound, transform, 0.8f, Random.Range(0.8f, 1.2f));
    }
    public void interact2SFX()
    {
        PlaySFX(interact2Sound, transform, 1f, Random.Range(0.9f, 1.1f));
    }
    public void deathSFX()
    {

        PlaySFX(deathSound, transform, 1, Random.Range(0.8f, 1.2f));
    }
    public void leverSFX()
    {
        PlaySFX(leverSound, transform, 1, Random.Range(0.8f, 1.2f));
    }
    public void portalSFX()
    {
        PlaySFX(portalSound, transform, 0.4f, Random.Range(0.8f, 1.2f));
    }
    public void pressurePlateSFX()
    {
        PlaySFX(pressurePlateSound, transform, 1, Random.Range(0.8f, 1.2f));
    }
    public void pickUpSFX()
    {
        PlaySFX(pickUpSound, transform, 1, Random.Range(0.8f, 1.2f));
    }
    public void spawnSFX()
    {
        PlaySFX(spawnSound, transform, 1, Random.Range(0.8f, 1.2f));
    }
    public void walkSFX(float pitch = 1)
    {
        PlaySFX(walkSound, transform, 0.2f, pitch);
    }
    public void slideSFX1()
    {
        float pitch;
        pitch = 1f + Masterslider.value;
        PlaySFX(buttonSound, transform, 1, pitch);
    }
    public void slideSFX2()
    {
        float pitch;
        pitch = 1f + MusicSlider.value;
        PlaySFX(buttonSound, transform, 1, pitch);
    }
    public void slideSFX3()
    {
        float pitch;
        pitch = 1f + SFXSlider.value;
        PlaySFX(buttonSound, transform, 1, pitch);
    }
}
