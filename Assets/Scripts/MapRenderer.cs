using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRenderer : MonoBehaviour {
    public Map map;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private IntegerReference mapSize;
    [SerializeField] private BaseTile baseTile;
    [SerializeField] private NoiseSettings noiseSettings;

    private MapGenerator _mapGenerator;

    private void Start() {
        _mapGenerator = new MapGenerator(noiseSettings);
        GenerateAndRenderMap();
    }

    private void GenerateMap() {
        _mapGenerator ??= new MapGenerator(noiseSettings);
        map.Tiles = MapGenerator.Generate(mapSize.Value);
    }

    private void RenderMap() {
        tileMap.ClearAllTiles();

        var center = mapSize.Value / 2;

        for (var i = 0; i < mapSize.Value; i++) {
            for (var j = 0; j < mapSize.Value; j++) {
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