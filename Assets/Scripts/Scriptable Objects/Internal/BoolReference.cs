using System;

[Serializable]
public class BoolReference {
    public bool useConstant = true;
    public bool constantValue;
    public BoolVariable variable;

    public bool Value => useConstant ? constantValue : variable.value;

    public void ToggleValue() {
        if (useConstant) {
            constantValue = !constantValue;
        } else {
            variable.value = !variable.value;
        }
    }

    public void SetValue(bool value) {
        if (useConstant) {
            constantValue = value;
        } else {
            variable.value = value;
        }
    }
}