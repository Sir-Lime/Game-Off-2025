using UnityEngine;
using Dan.Main;
public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;
    public float startTime;
    private bool isTiming = false;
    public float currentTime;
    public float levelEndTime = 0f;
    public float deathTime;
    public float time;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        isTiming = true;
    }

    public float GetTime()
    {
        if (!isTiming) return 0f;
        return currentTime;
    }


    public void StopAndSave()
    {
        if (!isTiming) return;

        float finalTime = GetTime();

        if (PlayerPrefs.HasKey("BestTime"))
        {
            float previousBest = PlayerPrefs.GetFloat("BestTime");

            if (finalTime < previousBest)
            {   
               PlayerPrefs.SetFloat("BestTime", finalTime);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("BestTime", finalTime);
        }
        isTiming = false;
        Leaderboards.Sinful.UploadNewEntry(PlayerPrefs.GetString("Name", "nullName"), Mathf.RoundToInt(PlayerPrefs.GetFloat("BestTime") * 10));
        Destroy(gameObject);

    }
}
