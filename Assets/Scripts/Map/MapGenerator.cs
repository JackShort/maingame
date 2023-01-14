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

    public void Generate(ref Map<Resource> map, int mapSize) {
        map.Elements = new Resource[mapSize, mapSize];

        for (var y = 0; y < mapSize; y++) {
            for (var x = 0; x < mapSize; x++) {
                var noiseValue = _noiseFilter.GetSimplexNoiseAtMapLocation(new float2(x, y), mapSize);
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

                map.Elements[y, x] = resource;
            }
        }
    }
}