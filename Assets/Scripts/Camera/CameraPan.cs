using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPan : MonoBehaviour {
    [SerializeField] private GameObject cameraRig;

    private bool _isPanning;
    private Camera _main;
    private bool _panStarted;
    private Vector3 _startingMousePosition;

    private void Start() {
        _main = Camera.main;
    }

    // TODO: add momentum to panning to add weight
    // TODO: also make it so you can't pan off the edge too far
    private void LateUpdate() {
        if (!_isPanning) {
            if (_panStarted) {
                _panStarted = false;
            }

            return;
        }

        var newMousePosition = _main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (!_panStarted) {
            _startingMousePosition = newMousePosition;
            _panStarted = true;
            return;
        }

        var deltaMousePosition = _startingMousePosition - newMousePosition;
        cameraRig.transform.position += deltaMousePosition;
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