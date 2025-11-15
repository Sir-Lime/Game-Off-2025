using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(PixelPerfectCamera))]
public class CameraScript : MonoBehaviour
{
    public playerController.PlayerController playerController;
    [SerializeField] private Transform followTarget;
    [SerializeField] public float followSpeedX = 5f; // changed from private to public for pause menu 
    [SerializeField] public float followSpeedY = 5f;
    [SerializeField] private float yOffsetPos = 2f;
    [SerializeField] private float xOffsetPos = 3f;
    private float xOffset;
    public PlayerInput input;
    private CameraFocusScript[] cameraFocusNodes;
    private PixelPerfectCamera ppCamera;
    private int resolutionX, resolutionY;

    private void Start()
    {
        cameraFocusNodes = GameObject.FindGameObjectsWithTag("CameraFocus").Select(obj => obj.GetComponent<CameraFocusScript>()).ToArray(); 
        ppCamera = GetComponent<PixelPerfectCamera>();
        
        resolutionX = ppCamera.refResolutionX / ppCamera.assetsPPU; 
        resolutionY = ppCamera.refResolutionY / ppCamera.assetsPPU;
    }

    private void FixedUpdate()
    {
        if (playerController.facingDir == 1f) xOffset = xOffsetPos; else xOffset = -xOffsetPos;
        float yOffset;
        if (input.actions["Down"].IsPressed()) yOffset = -yOffsetPos; 
        else yOffset = yOffsetPos;

        var target = new Vector2(followTarget.position.x + xOffset, followTarget.position.y + yOffset);
        target = GetFocusedTarget(target);

        float smoothedX = Mathf.Lerp(transform.position.x, target.x, followSpeedX * Time.deltaTime);
        float smoothedY = Mathf.Lerp(transform.position.y, target.y, followSpeedY * Time.deltaTime);
        Vector3 newPos = new Vector3(smoothedX, smoothedY, transform.position.z);
        transform.position = newPos;
    }

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

                // If the CameraFocus rectangle is too thin, then simply center the camera in the middle of it.
                if (focusNode.lockTop && focusNode.lockBottom && focusRect.height < resolutionY) target.y = focusRect.center.y;
                if (focusNode.lockRight && focusNode.lockLeft && focusRect.width < resolutionX) target.x = focusRect.center.x;

                if (input.actions["Down"].IsPressed()) target.y = followTarget.position.y - yOffsetPos;

                break;
            }
        }

        return target;
    }
}