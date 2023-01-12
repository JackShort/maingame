using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRenderer : MonoBehaviour {
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private int mapSize;
    [SerializeField] private BaseTile baseTile;
    public NoiseSettings noiseSettings;

    private TileType[,] _map;
    private MapGenerator _mapGenerator;

    private void Start() {
        _mapGenerator = new MapGenerator(noiseSettings);
        GenerateAndRenderMap();
    }

    private void GenerateMap() {
        _mapGenerator ??= new MapGenerator(noiseSettings);
        _map = MapGenerator.Generate(mapSize);
    }

    private void RenderMap() {
        tileMap.ClearAllTiles();

        var center = mapSize / 2;

        for (var i = 0; i < mapSize; i++) {
            for (var j = 0; j < mapSize; j++) {
                var baseTileClone = Instantiate(baseTile);
                baseTileClone.tileType = _map[i, j];
                tileMap.SetTile(new Vector3Int(i - center, j - center, 0), baseTileClone);
            }
        }
    }

    public void GenerateAndRenderMap() {
        GenerateMap();
        RenderMap();
    }
}