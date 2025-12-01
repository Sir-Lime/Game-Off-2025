using UnityEngine;
using TMPro;


public class BestTimeUIScript : MonoBehaviour
{

    private TextMeshProUGUI bestTimeText;
    void Start()
    {
        bestTimeText = GetComponent<TextMeshProUGUI>();
        bestTimeText.text = "";
    }
    void Update()
    {
        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTimeText.text = FormatTime(PlayerPrefs.GetFloat("BestTime"));
        }
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
