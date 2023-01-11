using System;
using UnityEngine;

[Serializable]
public class NoiseSettings {
    [Range(1, 8)] public int octaves = 1;
    public float baseFrequency = 1;
    public float frequencyScalar = 2;
    public float baseAmplitude = 1;
    public float amplitudeScalar = 0.5f;
    public float amplitude = 1;
}