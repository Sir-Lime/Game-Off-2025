using System;
using UnityEngine;

public class masterPressurePlateScript : MonoBehaviour {

    private GameObject[] pressurePlates;
    [SerializeField] private float timer = 5;
    private float time = -1;
    private int counter = 0;

    void Start() {
        pressurePlates = new GameObject[gameObject.transform.childCount];

        for (int i = 0; i< pressurePlates.Length; ++i) {
            pressurePlates[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    void Update() {
        if (time > timer) {
            for (int i = 0; i < pressurePlates.Length; ++i) {
                pressurePlates[i].gameObject.tag = "Deactivated";
            } time = -1; counter = 0;
        }

        if (time == -1) {
            for (int i = 0;i < pressurePlates.Length; ++i) {
                if (pressurePlates[i].gameObject.tag == "Activated") {
                    time = 0;  ++counter;
                }
            }
        } else {
            time += Time.deltaTime;
            counter = 0;
            for (int i = 0; i < pressurePlates.Length; ++i)
            {
                if (pressurePlates[i].gameObject.tag == "Activated")
                {
                    ++counter;
                }
            }
        }

        if (counter == pressurePlates.Length) {
            gameObject.tag = "Activated";
        }
    }
}
