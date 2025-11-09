using UnityEngine;
using UnityEngine.UI;

public class wavePanelScript : MonoBehaviour
{
    private RectMask2D rm;
    [SerializeField] private Wave waveScript;

    void Start() {
        rm = GetComponent<RectMask2D>();
    }

    void Update() {
        float x = (waveScript.timeElapsed * 65) / (2*Mathf.PI);
        // z = right 
        rm.padding = new Vector4(rm.padding.x, rm.padding.y, 65 - x,rm.padding.w); 
    }
}
