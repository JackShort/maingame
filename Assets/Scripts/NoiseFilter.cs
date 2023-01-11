using Unity.Mathematics;

public class NoiseFilter {
    private readonly NoiseSettings _settings;

    public NoiseFilter(NoiseSettings settings) {
        _settings = settings;
    }

    private static float GetBaseNoise(float nx, float ny, float nz = 0f) {
        return (noise.snoise(new float3(nx, ny, nz)) + 1) * 0.5f;
    }

    public float GetSimplexNoise(float2 location) {
        var frequency = _settings.baseFrequency;
        var amplitude = _settings.baseAmplitude;
        var amplitudeSum = 0f;
        var noiseValue = 0f;

        for (var octave = 0;
             octave < _settings.octaves;
             octave++, amplitude *= _settings.persistence, frequency *= _settings.frequencyScalar) {
            noiseValue += GetBaseNoise(location.x * frequency, location.y * frequency, octave) * amplitude;
            amplitudeSum += amplitude;
        }

        return noiseValue / amplitudeSum;
    }
}