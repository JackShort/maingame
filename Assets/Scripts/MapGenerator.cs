using Unity.Mathematics;

public class MapGenerator {
    private readonly Resource _empty;
    private readonly Resource _mountains;
    private readonly NoiseFilter _noiseFilter;
    private readonly Resource _trees;
    private readonly Resource _water;

    public MapGenerator(NoiseSettings noiseSettings, Resource empty, Resource mountains, Resource trees,
        Resource water) {
        _noiseFilter = new NoiseFilter(noiseSettings);
        _empty = empty;
        _mountains = mountains;
        _trees = trees;
        _water = water;
    }


    public Resource[,] Generate(int mapSize) {
        var map = new Resource[mapSize, mapSize];

        for (var i = 0; i < mapSize; i++) {
            for (var j = 0; j < mapSize; j++) {
                var noiseValue = _noiseFilter.GetSimplexNoiseAtMapLocation(new float2(i, j), mapSize);
                var resource = _empty;

                if (noiseValue < 0.25) {
                    resource = _water;
                }
                else if (noiseValue < 0.35) {
                    resource = _mountains;
                }
                else if (noiseValue < 0.45) {
                    resource = _trees;
                }

                map[i, j] = resource;
            }
        }

        return map;
    }
}