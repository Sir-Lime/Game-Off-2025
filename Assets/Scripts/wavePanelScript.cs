using UnityEngine;
using UnityEngine.UI;

public class wavePanelScript : MonoBehaviour
{
    private RectMask2D rm;
    [SerializeField] private Wave waveScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rm = GetComponent<RectMask2D>();
        //waveScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<Wave>();
    }

    // Update is called once per frame
    void Update() {
        float x = (waveScript.timeElapsed * 247) / (2*Mathf.PI);
        // z = right 
        rm.padding = new Vector4(rm.padding.x, rm.padding.y, 247 - x,rm.padding.w); 
    }
}
