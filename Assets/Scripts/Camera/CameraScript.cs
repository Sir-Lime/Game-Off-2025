using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(PixelPerfectCamera))]
public class CameraScript : MonoBehaviour
{
    // -------------------- SINGLETON --------------------
    public static CameraScript Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // -------------------- EXISTING FIELDS --------------------
    public playerController.PlayerController playerController;
    [SerializeField] private Transform followTarget;
    [SerializeField] public float followSpeedX = 5f;
    [SerializeField] public float followSpeedY = 5f;
    [SerializeField] private float yOffsetPos = 2f;
    [SerializeField] private float xOffsetPos = 3f;
    private float xOffset;
    public PlayerInput input;
    private CameraFocusScript[] cameraFocusNodes;
    private PixelPerfectCamera ppCamera;
    private int resolutionX, resolutionY;
    private Vector3 originalLocalPos;

    // -------------------- CAMERA SHAKE --------------------
    [Header("Camera Shake")]
    [SerializeField] private float shakeDecay = 1.8f;

    private float shakeTimer = 0f;
    private float shakeIntensity = 0f;
    private Vector3 shakeOffset = Vector3.zero;

    // -------------------- UNITY --------------------
    private void Start()
    {
        cameraFocusNodes = GameObject.FindGameObjectsWithTag("CameraFocus")
            .Select(obj => obj.GetComponent<CameraFocusScript>())
            .ToArray();

        ppCamera = GetComponent<PixelPerfectCamera>();

        resolutionX = ppCamera.refResolutionX / ppCamera.assetsPPU;
        resolutionY = ppCamera.refResolutionY / ppCamera.assetsPPU;

        originalLocalPos = transform.localPosition;
    }

    private void FixedUpdate()
    {
        if (playerController.facingDir == 1f)
            xOffset = xOffsetPos;
        else
            xOffset = -xOffsetPos;

        float yOffset = input.actions["Down"].IsPressed() ? -yOffsetPos : yOffsetPos;

        var target = new Vector2(
            followTarget.position.x + xOffset,
            followTarget.position.y + yOffset
        );

        target = GetFocusedTarget(target);

        float smoothedX = Mathf.Lerp(transform.position.x, target.x, followSpeedX * Time.deltaTime);
        float smoothedY = Mathf.Lerp(transform.position.y, target.y, followSpeedY * Time.deltaTime);
        Vector3 newPos = new Vector3(smoothedX, smoothedY, transform.position.z);

        // -------------------- APPLY CAMERA SHAKE --------------------
        HandleCameraShake();

        transform.localPosition = new Vector3(newPos.x, newPos.y, newPos.z) + shakeOffset;
    }

    // -------------------- SHAKE LOGIC --------------------
    public void ShakeCamera(float intensity, float duration)
    {
        shakeIntensity = Mathf.Max(shakeIntensity, intensity);
        shakeTimer = Mathf.Max(shakeTimer, duration);
    }

    private void HandleCameraShake()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;

            float current = shakeIntensity;

            shakeOffset = new Vector3(
                Random.Range(-current, current),
                Random.Range(-current, current),
                0f
            );

            shakeIntensity = Mathf.Lerp(shakeIntensity, 0f, shakeDecay * Time.deltaTime);
        }
        else
        {
            shakeOffset = Vector3.zero;
            shakeIntensity = 0f;
        }
    }

    // -------------------- FOCUS ZONE LOGIC --------------------
    private Vector2 GetFocusedTarget(Vector2 target)
    {
        foreach (CameraFocusScript focusNode in cameraFocusNodes)
        {
            var focusRect = focusNode.GetRect();

            if (focusRect.Contains(followTarget.position))
            {
                var maxY = float.MaxValue;
                var minY = float.MinValue;
                var maxX = float.MaxValue;
                var minX = float.MinValue;

                if (focusNode.lockTop) maxY = focusRect.yMax - resolutionY / 2;
                if (focusNode.lockBottom) minY = focusRect.yMin + resolutionY / 2;
                if (focusNode.lockRight) maxX = focusRect.xMax - resolutionX / 2;
                if (focusNode.lockLeft) minX = focusRect.xMin + resolutionX / 2;

                target.x = Mathf.Clamp(target.x, minX, maxX);
                target.y = Mathf.Clamp(target.y, minY, maxY);

                if (focusNode.lockTop && focusNode.lockBottom && focusRect.height < resolutionY)
                    target.y = focusRect.center.y;

                if (focusNode.lockRight && focusNode.lockLeft && focusRect.width < resolutionX)
                    target.x = focusRect.center.x;

                if (input.actions["Down"].IsPressed())
                    target.y = followTarget.position.y - yOffsetPos;

                break;
            }
        }
        return target;
    }
}
