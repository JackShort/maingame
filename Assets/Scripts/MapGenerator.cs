using Unity.Mathematics;

public class MapGenerator {
    private static NoiseFilter _noiseFilter;

    public MapGenerator(NoiseSettings noiseSettings) {
        _noiseFilter = new NoiseFilter(noiseSettings);
    }

    public static TileType[,] Generate(int mapSize) {
        var map = new TileType[mapSize, mapSize];

        for (var i = 0; i < mapSize; i++) {
            for (var j = 0; j < mapSize; j++) {
                map[i, j] = _noiseFilter.Evaluate(new float2(i, j)) > 0.2 ? TileType.Base : TileType.Mountain;
            }
        }

        return map;
    }
}