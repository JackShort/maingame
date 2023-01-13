using System;
using UnityEngine;

[Serializable]
public struct ArrayIndex2 {
    // ReSharper disable once InconsistentNaming
    [SerializeField] public int r;

    // ReSharper disable once InconsistentNaming
    [SerializeField] public int c;

    public ArrayIndex2(int r, int c) {
        this.r = r;
        this.c = c;
    }

    public override string ToString() {
        return $"ArrayIndex2(r: {r}, c: {c})";
    }
}