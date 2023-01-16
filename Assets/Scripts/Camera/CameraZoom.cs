using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class CameraZoom : MonoBehaviour {
    [SerializeField] private float scrollScale = 0.1f;
    [SerializeField] private float minZoomSpeed = -6f;
    [SerializeField] private float maxZoomSpeed = 18f;
    [SerializeField] private int minPixelsPerUnit = 30;

    private PixelPerfectCamera _pixelPerfectCamera;

    private void Start() {
        if (Camera.main != null) {
            _pixelPerfectCamera = Camera.main.GetComponent<PixelPerfectCamera>();
        }
    }

    [UsedImplicitly]
    public void AdjustZoom(InputAction.CallbackContext context) {
        if (!context.performed) {
            return;
        }

        var zoom = Mathf.Clamp(context.ReadValue<Vector2>().y * scrollScale, minZoomSpeed, maxZoomSpeed);
        if (_pixelPerfectCamera != null) {
            _pixelPerfectCamera.assetsPPU =
                Mathf.Max(_pixelPerfectCamera.assetsPPU + Mathf.FloorToInt(zoom), minPixelsPerUnit);
        }
    }
}