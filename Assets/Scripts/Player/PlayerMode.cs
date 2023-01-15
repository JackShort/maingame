using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMode : MonoBehaviour {
    [SerializeField] private BoolReference inPlacingMode;
    [SerializeField] private BoolVariable inPlacingModeDefault;

    private void Start() {
        inPlacingMode.SetValue(inPlacingModeDefault.value);
    }

    [UsedImplicitly]
    public void TogglePlacingMode(InputAction.CallbackContext context) {
        if (context.performed) {
            inPlacingMode.ToggleValue();
        }
    }
}