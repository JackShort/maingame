using Unity.Mathematics;

public class NoiseFilter {
    private readonly NoiseSettings _settings;

    public NoiseFilter(NoiseSettings settings) {
        _settings = settings;
    }

    private static float GetBaseNoise(float nx, float ny, float nz = 0f) {
        return (noise.snoise(new float3(nx, ny, nz)) + 1) * 0.5f;
    }

    private float GetSimplexNoise(float nx, float ny) {
        var frequency = _settings.baseFrequency;
        var amplitude = _settings.baseAmplitude;
        var amplitudeSum = 0f;
        var noiseValue = 0f;

        for (var octave = 0;
             octave < _settings.octaves;
             octave++, amplitude *= _settings.persistence, frequency *= _settings.frequencyScalar) {
            noiseValue += GetBaseNoise(nx * frequency, ny * frequency, octave) * amplitude;
            amplitudeSum += amplitude;
        }

        return noiseValue / amplitudeSum;
    }

    public float GetSimplexNoiseAtMapLocation(float2 location, int mapSize) {
        var nx = location.x / mapSize - 0.5f;
        var ny = location.y / mapSize - 0.5f;
        return GetSimplexNoise(nx, ny);
    }
}