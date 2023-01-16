using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPan : MonoBehaviour {
    [SerializeField] private float panSpeed = 10f;
    [SerializeField] private float deadZone = 0.1f;
    [SerializeField] private GameObject cameraRig;

    private bool _isPanning;
    private Vector2 _oldMouseState;
    private bool _panStarted;

    // TODO: add momentum to panning to add weight
    // TODO: also make it so you can't pan off the edge too far
    private void Update() {
        if (!_isPanning) {
            if (_panStarted) {
                _panStarted = false;
            }

            return;
        }

        var newMousePosition = Mouse.current.position.ReadValue();
        if (!_panStarted) {
            _oldMouseState = newMousePosition;
            _panStarted = true;
            return;
        }

        var deltaMousePosition = _oldMouseState - newMousePosition;
        _oldMouseState = newMousePosition;

        if (deltaMousePosition.magnitude < deadZone) {
            return;
        }

        cameraRig.transform.position +=
            new Vector3(deltaMousePosition.x, deltaMousePosition.y, 0) *
            (panSpeed * Time.deltaTime);
    }

    [UsedImplicitly]
    public void PanCamera(InputAction.CallbackContext context) {
        if (context.performed) {
            _isPanning = true;
        } else if (context.canceled) {
            _isPanning = false;
        }
    }
}