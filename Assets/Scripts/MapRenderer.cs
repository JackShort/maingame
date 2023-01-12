using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRenderer : MonoBehaviour {
    public Map map;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private BaseTile baseTile;
    [SerializeField] private NoiseSettings noiseSettings;

    private MapGenerator _mapGenerator;

    private void Start() {
        _mapGenerator = new MapGenerator(noiseSettings);
        GenerateAndRenderMap();
    }

    private void GenerateMap() {
        _mapGenerator ??= new MapGenerator(noiseSettings);
        map.Tiles = MapGenerator.Generate(map.mapSize);
    }

    private void RenderMap() {
        tileMap.ClearAllTiles();

        var center = map.mapSize / 2;

        for (var i = 0; i < map.mapSize; i++) {
            for (var j = 0; j < map.mapSize; j++) {
                var baseTileClone = Instantiate(baseTile);
                baseTileClone.tileType = map.Tiles[i, j];
                tileMap.SetTile(new Vector3Int(i - center, j - center, 0), baseTileClone);
            }
        }
    }

    public void GenerateAndRenderMap() {
        GenerateMap();
        RenderMap();
    }
}