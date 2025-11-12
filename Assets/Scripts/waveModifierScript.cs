using UnityEngine;

public class waveModifierScript : MonoBehaviour
{
    [SerializeField] private GameObject[] Activators;
    [SerializeField] private Wave jumpWave;
    [SerializeField] private Wave dashWave;
    [SerializeField] private float jumpWaveModifier;
    [SerializeField] private float dashWaveModifier;
    [SerializeField] private Wave.WaveType jumpWaveType, dashWaveType;
    private Wave.WaveType jumpWaveTypeBefore, dashWaveTypeBefore;

    private bool activated = false;
    void Awake()
    {
        jumpWave = GameObject.FindWithTag("JumpWave")?.GetComponent<Wave>();
        dashWave = GameObject.FindWithTag("DashWave")?.GetComponent<Wave>();
    }
    void Update()
    {
        bool anyActivated = false;

        foreach (GameObject activator in Activators)
        {
            if (activator != null && activator.CompareTag("Activated"))
            {
                anyActivated = true;
                break;
            }
        }

        if (anyActivated && !activated)
        {
            activated = true;
            jumpWave.timeElapsed = 0;
            dashWave.timeElapsed = 0;
            jumpWave.movementSpeed *= jumpWaveModifier;
            dashWave.movementSpeed *= dashWaveModifier;
            jumpWaveTypeBefore = jumpWave.waveType;
            dashWaveTypeBefore = dashWave.waveType;
            jumpWave.ChangeJumpWave(jumpWaveType);
            dashWave.ChangeDashWave(dashWaveType);
        }
        else if (!anyActivated && activated)
        {
            activated = false;
            jumpWave.timeElapsed = 0;
            dashWave.timeElapsed = 0;
            jumpWave.movementSpeed /= jumpWaveModifier;
            dashWave.movementSpeed /= dashWaveModifier;
            jumpWave.ChangeJumpWave(jumpWaveTypeBefore);
            dashWave.ChangeDashWave(dashWaveTypeBefore);
        }
    }
}
