using Unity.Mathematics;

public class NoiseFilter {
    private readonly NoiseSettings _settings;

    public NoiseFilter(NoiseSettings settings) {
        _settings = settings;
    }

    public float Evaluate(float2 location) {
        // adding 1 and multiplying by 2 - because snoise returns -1 < 1
        var noiseValue = (noise.snoise(location * _settings.frequency) + 1) * 0.5f;

        return noiseValue * _settings.amplitude;
    }
}