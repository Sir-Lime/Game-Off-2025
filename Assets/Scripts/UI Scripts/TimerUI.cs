using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (TimerManager.Instance == null)
            return;

        float time = TimerManager.Instance.GetTime();
        timerText.text = FormatTime(time);
    }

    private string FormatTime(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60f);
        float seconds = t % 60f;
        float secondsWithDecimal = Mathf.Floor(seconds * 10f) / 10f;

        if (minutes > 0)
        {
            return $"{minutes:00}:{secondsWithDecimal}";
        }
        else
        {
            return $"{secondsWithDecimal}";
        }
    }
}
