using UnityEngine;

public class waveModifierScript : MonoBehaviour
{
    [SerializeField] private GameObject[] Activators;
    [SerializeField] private Wave jumpWave, dashWave;
    [SerializeField] private float jumpWaveSpeedNew = 1, dashWaveSpeedNew = 1;
    [SerializeField] private Wave.WaveType jumpWaveType = Wave.WaveType.Sin, dashWaveType = Wave.WaveType.Cos;
    private Wave.WaveType jumpWaveTypeBefore, dashWaveTypeBefore;
    private float jumpWaveSpeedBefore, dashWaveSpeedBefore;
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
            jumpWave.movementSpeed = jumpWaveSpeedNew;
            dashWave.movementSpeed = dashWaveSpeedNew;
            jumpWaveTypeBefore = jumpWave.waveType;
            dashWaveTypeBefore = dashWave.waveType;
            jumpWaveSpeedBefore = jumpWave.movementSpeed;
            dashWaveSpeedBefore = dashWave.movementSpeed;
            jumpWave.ChangeJumpWave(jumpWaveType);
            dashWave.ChangeDashWave(dashWaveType);
        }
        else if (!anyActivated && activated)
        {
            activated = false;
            jumpWave.timeElapsed = 0;
            dashWave.timeElapsed = 0;
            jumpWave.movementSpeed = jumpWaveSpeedBefore;
            dashWave.movementSpeed = dashWaveSpeedBefore;
            jumpWave.ChangeJumpWave(jumpWaveTypeBefore);
            dashWave.ChangeDashWave(dashWaveTypeBefore);
        }
    }
}
