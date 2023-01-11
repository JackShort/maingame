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
                var noiseValue = _noiseFilter.GetSimplexNoise(new float2(i, j));
                var tileType = TileType.Base;

                if (noiseValue < 0.25) {
                    tileType = TileType.River;
                }
                else if (noiseValue < 0.35) {
                    tileType = TileType.Mountain;
                }
                else if (noiseValue < 0.45) {
                    tileType = TileType.Tree;
                }

                map[i, j] = tileType;
            }
        }

        return map;
    }
}