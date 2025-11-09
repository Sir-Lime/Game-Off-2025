using NUnit.Framework;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private float minValue = -250;
    [SerializeField] private float maxValue = 250;
    public float movementSpeed = 1;   
    private float timeElapsed = 0f;
    private float waveValue;

    void Update()
    {
        GetWaveValue(0);
    }
    public float GetWaveValue(int waveType)
    {
        timeElapsed += Time.deltaTime * movementSpeed;

        float amplitude = (maxValue - minValue) / 2.0f;
        float midPoint = (maxValue + minValue) / 2.0f;
        waveValue = 0f;
        switch (waveType)   ////// just made this a switch for cool code purposes, we will probably have more wave types so switch makes sense
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
        transform.localPosition = new Vector3(transform.localPosition.x, normalizedWave, transform.localPosition.z);
        return Mathf.Round(normalizedWave * 10f) / 10f;
    }
    public void SetWaveSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }
    
}
