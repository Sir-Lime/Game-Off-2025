using NUnit.Framework;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private float minValue = -250;
    [SerializeField] private float maxValue = 250;
    public float movementSpeed = 1;   
    public float timeElapsed = 0f;
    private float waveValue;

    void Update()
    {
        timeElapsed += Time.deltaTime * movementSpeed;
        if (timeElapsed > (2*Mathf.PI)) timeElapsed -= (2*Mathf.PI); 
        GetWaveValue(0);
    }
    public float GetWaveValue(int waveType)
    {
        float amplitude = (maxValue - minValue) / 2.0f;
        float midPoint = (maxValue + minValue) / 2.0f;
        waveValue = 0f;
        switch (waveType)  
        {
            case 0:
                waveValue = midPoint + amplitude * Mathf.Sin(timeElapsed);
                break;

            case 1:
                waveValue = midPoint + amplitude * Mathf.Cos(timeElapsed);
                break;
            case 2:
                waveValue = midPoint + amplitude * Mathf.Tan(timeElapsed);
                break;
        }
        float normalizedWave = Mathf.InverseLerp(-250f, 250f, waveValue);
        return normalizedWave;
    }
    public void SetWaveSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }
    
}
