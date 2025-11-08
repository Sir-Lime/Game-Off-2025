using NUnit.Framework;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [UnityEngine.Range(0, 1)]
    [SerializeField] private int isSinOrCos; //0 for sin, 1 for cos
    [SerializeField] private float minValue = -250;
    [SerializeField] private float maxValue = 250;
    [SerializeField] private float movementSpeed = 1;
    private float timeElapsed = 0f;
    private float waveValue;

    void Update()
    {
        DrawCycle();
    }
    void DrawCycle()
    {
        timeElapsed += Time.deltaTime * movementSpeed;

        float amplitude = (maxValue - minValue) / 2.0f;
        float midPoint = (maxValue + minValue) / 2.0f;
        waveValue = 0f;

        if (isSinOrCos == 0)
            waveValue = midPoint + amplitude * Mathf.Sin(timeElapsed);
        else
            waveValue = midPoint + amplitude * Mathf.Cos(timeElapsed);
        Debug.Log(waveValue);
        transform.localPosition = new Vector3(transform.localPosition.x, waveValue, transform.localPosition.z);
    }
    
    
   public float WaveValue { get { return waveValue; } }
}
