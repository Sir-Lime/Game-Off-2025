using UnityEngine;

public class waveModifierScript : MonoBehaviour {

    [SerializeField] private bool bo = false; // @lime feel free to delete it when you integrate your activators thingy
    [SerializeField] private Wave jumpWave;
    [SerializeField] private Wave dashWave;
    [SerializeField] private float jumpWaveModifier;
    [SerializeField] private float dashWaveModifier;
    private float time = 0;
    [SerializeField] private float countDown = 3;
    private float lastActivated;
    private bool activated = false;

    void Start() {
        lastActivated = -countDown;
    }

    // Update is called once per frame
    void Update() {
        time += Time.deltaTime;
        if (bo && (time - lastActivated) >= countDown) {
            lastActivated = time;
            if (activated){
                activated = false;
                jumpWave.timeElapsed = 0; jumpWave.movementSpeed /= jumpWaveModifier;
                dashWave.timeElapsed = 0; dashWave.movementSpeed /= dashWaveModifier;
            }
            else {
                activated = true; ;
                jumpWave.timeElapsed = 0; jumpWave.movementSpeed *= jumpWaveModifier;
                dashWave.timeElapsed = 0; dashWave.movementSpeed *= dashWaveModifier;
            }
        }
    }
}
