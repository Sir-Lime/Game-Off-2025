using UnityEngine;
using UnityEngine.UI;


public class wavePanelScript : MonoBehaviour
{
    private RectMask2D rm;
    [SerializeField] private Wave waveScript;
    [SerializeField] private float cst;

    void Start() {
        rm = GetComponent<RectMask2D>();
    }

    void Update() {
        float x = (waveScript.timeElapsed * cst) / (2*Mathf.PI);
        // z = right 
        rm.padding = new Vector4(rm.padding.x, rm.padding.y, cst - x + 12,rm.padding.w); 
    }
}
