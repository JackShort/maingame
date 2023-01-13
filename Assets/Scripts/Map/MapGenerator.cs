using Unity.Mathematics;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    [SerializeField] private Resource emptyResource;
    [SerializeField] private Resource mountainsResource;
    [SerializeField] private Resource treesResource;
    [SerializeField] private Resource waterResource;
    [SerializeField] private NoiseSettings noiseSettings;

    private NoiseFilter _noiseFilter;

    private void Start() {
        _noiseFilter = new NoiseFilter(noiseSettings);
    }

    private void OnValidate() {
        _noiseFilter ??= new NoiseFilter(noiseSettings);
    }

    public Resource[,] Generate(int mapSize) {
        var map = new Resource[mapSize, mapSize];

        for (var i = 0; i < mapSize; i++) {
            for (var j = 0; j < mapSize; j++) {
                var noiseValue = _noiseFilter.GetSimplexNoiseAtMapLocation(new float2(i, j), mapSize);
                var resource = emptyResource;

                if (noiseValue < 0.25) {
                    resource = waterResource;
                }
                else if (noiseValue < 0.35) {
                    resource = mountainsResource;
                }
                else if (noiseValue < 0.45) {
                    resource = treesResource;
                }

                map[i, j] = resource;
            }
        }

        return map;
    }
}