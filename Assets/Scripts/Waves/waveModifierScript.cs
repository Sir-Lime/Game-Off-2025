using UnityEngine;

public class waveModifierScript : MonoBehaviour
{
    [SerializeField] private GameObject[] Activators;
    [SerializeField] private float jumpWaveSpeedNew = 1, dashWaveSpeedNew = 1;
    [SerializeField] private Wave.WaveType jumpWaveType = Wave.WaveType.Sin, dashWaveType = Wave.WaveType.Cos;
    private Wave.WaveType jumpWaveTypeBefore, dashWaveTypeBefore;
    private Wave jumpWave, dashWave;

    private float jumpWaveSpeedBefore, dashWaveSpeedBefore;
    private bool activated = false;
    private Animator animator;
    private AudioSource audioSource;
    void Awake()
    {
        jumpWave = GameObject.FindWithTag("JumpWave")?.GetComponent<Wave>();
        dashWave = GameObject.FindWithTag("DashWave")?.GetComponent<Wave>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

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
            audioSource.enabled = true;
            jumpWave.timeElapsed = 0;
            dashWave.timeElapsed = 0;
            jumpWaveTypeBefore = jumpWave.waveType;
            dashWaveTypeBefore = dashWave.waveType;
            jumpWaveSpeedBefore = jumpWave.movementSpeed;
            dashWaveSpeedBefore = dashWave.movementSpeed;
            jumpWave.movementSpeed = jumpWaveSpeedNew;
            dashWave.movementSpeed = dashWaveSpeedNew;
            jumpWave.ChangeJumpWave(jumpWaveType);
            dashWave.ChangeDashWave(dashWaveType);
            animator.SetBool("Activated", true);
        }
        else if (!anyActivated && activated)
        {
            activated = false;
            audioSource.enabled = false;
            jumpWave.timeElapsed = 0;
            dashWave.timeElapsed = 0;
            jumpWave.movementSpeed = jumpWaveSpeedBefore;
            dashWave.movementSpeed = dashWaveSpeedBefore;
            jumpWave.ChangeJumpWave(jumpWaveTypeBefore);
            dashWave.ChangeDashWave(dashWaveTypeBefore);
            animator.SetBool("Activated", false);
        }
    }
}
