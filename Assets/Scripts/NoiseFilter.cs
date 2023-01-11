using Unity.Mathematics;

public class NoiseFilter {
    private readonly NoiseSettings _settings;

    public NoiseFilter(NoiseSettings settings) {
        _settings = settings;
    }

    public float Evaluate(float2 location) {
        var frequency = _settings.baseFrequency;
        var amplitude = _settings.baseAmplitude;
        var amplitudeSum = 0f;
        var noiseValue = 0f;

        for (var i = 0; i < _settings.octaves; i++) {
            // adding 1 and multiplying by 0.5 - because snoise returns -1 < 1
            noiseValue += (noise.snoise(location * frequency) + 1) * 0.5f * amplitude;
            frequency *= _settings.frequencyScalar;
            amplitudeSum += amplitude;
            amplitude *= _settings.amplitudeScalar;
        }

        return noiseValue * _settings.amplitude / amplitudeSum;
    }
}