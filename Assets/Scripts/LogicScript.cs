using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class LogicScript : MonoBehaviour
{
    public bool[] ineractableObjects = new bool[100];
    void Awake() {
        
    }
    void Start() {
        int i;
        for (i = 0; i < 100; ++i) ineractableObjects[i] = false;
    }

    void Update() {
    }
}
