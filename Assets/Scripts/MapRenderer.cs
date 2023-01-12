using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRenderer : MonoBehaviour {
    public Map map;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private IntegerReference mapSize;
    [SerializeField] private BaseTile baseTile;
    [SerializeField] private NoiseSettings noiseSettings;

    [SerializeField] private Resource emptyResource;
    [SerializeField] private Resource mountainsResource;
    [SerializeField] private Resource treesResource;
    [SerializeField] private Resource waterResource;


    private MapGenerator _mapGenerator;

    private void Start() {
        _mapGenerator = new MapGenerator(noiseSettings, emptyResource, mountainsResource, treesResource, waterResource);
        GenerateAndRenderMap();
    }

    private void GenerateMap() {
        _mapGenerator ??=
            new MapGenerator(noiseSettings, emptyResource, mountainsResource, treesResource, waterResource);
        map.Tiles = _mapGenerator.Generate(mapSize.Value);
    }

    private void RenderMap() {
        tileMap.ClearAllTiles();

        var center = mapSize.Value / 2;

        for (var i = 0; i < mapSize.Value; i++) {
            for (var j = 0; j < mapSize.Value; j++) {
                var baseTileClone = Instantiate(baseTile);
                baseTileClone.color = map.Tiles[i, j].color;
                baseTileClone.sprite = map.Tiles[i, j].tileSprite;
                tileMap.SetTile(new Vector3Int(i - center, j - center, 0), baseTileClone);
            }
        }
    }

    public void GenerateAndRenderMap() {
        GenerateMap();
        RenderMap();
    }
}