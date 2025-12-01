using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class Wave : MonoBehaviour
{
    public enum WaveType {Sin,Cos,Tan, Cot, NegativeSin, NegativeCos, NegativeTan, NegativeCot}


    [SerializeField] private UnityEngine.UI.Image jumpImage, dashImage;
    [SerializeField] private Sprite sin, cos, tan, cot, neg_sin, neg_cos, neg_tan, neg_cot;

    [SerializeField] public WaveType waveType = WaveType.Sin;

    public float movementSpeed = 1f;
    public float timeElapsed = 0f;
    public float waveValue;
    public float normalizedWave;
    private float minValue = -250f;
    private float maxValue = 250f;
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
                maxValue = 250f; minValue = -250f;
                break;
            case WaveType.Cos:
                waveValue = midPoint + amplitude * Mathf.Cos(timeElapsed);
                maxValue = 250f; minValue = -250f;
                break;
            case WaveType.Tan:
                waveValue = midPoint + amplitude * Mathf.Tan(timeElapsed);
                maxValue = 10000000f; minValue = -10000000f;
                break;
            case WaveType.Cot:
                waveValue = midPoint + amplitude * -Mathf.Tan(timeElapsed - (Mathf.PI / 2));
                maxValue = 10000000f; minValue = -10000000f;
                break;
            case WaveType.NegativeSin:
                waveValue = midPoint + amplitude * Mathf.Sin(timeElapsed) * -1;
                maxValue = 250f; minValue = -250f;
                break;
            case WaveType.NegativeCos:
                waveValue = midPoint + amplitude * Mathf.Cos(timeElapsed) * -1;
                maxValue = 250f; minValue = -250f;
                break;
            case WaveType.NegativeTan:
                waveValue = midPoint + amplitude * Mathf.Tan(timeElapsed) * -1;
                maxValue = 10000000f; minValue = -10000000f;
                break;
            case WaveType.NegativeCot:
                waveValue = midPoint + amplitude * -Mathf.Tan(timeElapsed - (Mathf.PI / 2)) * -1;
                maxValue = 10000000f; minValue = -10000000f;
                break;
        }
    
        normalizedWave = Mathf.InverseLerp(minValue, maxValue, waveValue);
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
            case WaveType.Tan:
                jumpImage.sprite = tan;
                break;
            case WaveType.Cot:
                jumpImage.sprite = cot;
                break;
            case WaveType.NegativeSin:
                jumpImage.sprite = neg_sin;
                break;
            case WaveType.NegativeCos:
                jumpImage.sprite = neg_cos;
                break;
            case WaveType.NegativeTan:
                jumpImage.sprite = neg_tan;
                break;
            case WaveType.NegativeCot:
                jumpImage.sprite = neg_cot;
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
            case WaveType.Tan:
                dashImage.sprite = tan;
                break;
            case WaveType.Cot:
                dashImage.sprite = cot;
                break;
            case WaveType.NegativeSin:
                dashImage.sprite = neg_sin;
                break;
            case WaveType.NegativeCos:
                dashImage.sprite = neg_cos;
                break;
            case WaveType.NegativeTan:
                dashImage.sprite = neg_tan;
                break;
            case WaveType.NegativeCot:
                dashImage.sprite = neg_cot;
                break;
        }
    }
}
