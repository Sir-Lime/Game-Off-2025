using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class Wave : MonoBehaviour
{
    public enum WaveType {Sin,Cos,NegativeSin}

    [SerializeField] private float minValue = -250f;
    [SerializeField] private float maxValue = 250f;
    [SerializeField] private UnityEngine.UI.Image jumpImage;
    [SerializeField] private UnityEngine.UI.Image dashImage;
    [SerializeField] private Sprite sin, cos, neg_sin;

    [SerializeField] public WaveType waveType = WaveType.Sin;

    public float movementSpeed = 1f;
    public float timeElapsed = 0f;
    private float waveValue;

    void Update()
    {
        timeElapsed += Time.deltaTime * movementSpeed;
        if (timeElapsed > 2 * Mathf.PI) timeElapsed -= 2 * Mathf.PI;
        GetWaveValue();
    }

    public float GetWaveValue()
    {
        float amplitude = (maxValue - minValue) / 2.0f;
        float midPoint = (maxValue + minValue) / 2.0f;

        switch (waveType)
        {
            case WaveType.Sin:
                waveValue = midPoint + amplitude * Mathf.Sin(timeElapsed);
                break;
            case WaveType.Cos:
                waveValue = midPoint + amplitude * Mathf.Cos(timeElapsed);
                break;
            case WaveType.NegativeSin:
                waveValue = midPoint + amplitude * -Mathf.Sin(timeElapsed);
                break;
        }

        float normalizedWave = Mathf.InverseLerp(-250f, 250f, waveValue);
        return normalizedWave;
    }

    public void ChangeJumpWave(WaveType newWaveType)
    {
        waveType = newWaveType;

        switch (waveType)
        {
            case WaveType.Sin:
                jumpImage.sprite = sin;
                break;
            case WaveType.Cos:
                jumpImage.sprite = cos;
                break;
            case WaveType.NegativeSin:
                jumpImage.sprite = neg_sin;
                break;
        }
    }

    public void ChangeDashWave(WaveType newWaveType)
    {
        waveType = newWaveType;

        switch (waveType)
        {
            case WaveType.Sin:
                dashImage.sprite = sin;
                break;
            case WaveType.Cos:
                dashImage.sprite = cos;
                break;
            case WaveType.NegativeSin:
                dashImage.sprite = neg_sin;
                break;
        }
    }
}
