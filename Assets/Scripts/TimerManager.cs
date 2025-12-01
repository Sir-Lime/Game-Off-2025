using UnityEngine;
using Dan.Main;
public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;
    public float startTime;
    private bool isTiming = false;

    private void Awake()
    {
        // Singleton setup
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
        // Start the run the moment the first scene loads
        startTime = Time.time;
        isTiming = true;
    }

    public float GetTime(bool raw = false)
    {
        if (!isTiming) return 0f;
        if (raw) return Time.time;
        return Time.time - startTime;
    }
    public void Update()
    {
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
