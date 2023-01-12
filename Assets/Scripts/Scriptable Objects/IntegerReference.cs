using System;

[Serializable]
public class IntegerReference {
    public bool useConstant = true;
    public int constantValue;
    public IntegerVariable variable;

    public int Value => useConstant ? constantValue : variable.value;
}