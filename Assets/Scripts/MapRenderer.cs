using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRenderer : MonoBehaviour {
    public Map map;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private IntegerReference mapSize;
    [SerializeField] private DummyTile dummyTile;

    private void Start() {
        GenerateAndRenderMap();
    }

    private void GenerateMap() {
        var mapGenerator = GetComponent<MapGenerator>();
        map.Tiles = mapGenerator.Generate(mapSize.Value);
    }

    private void RenderMap() {
        tileMap.ClearAllTiles();

        var center = map.Tiles.GetLength(0) / 2;

        for (var i = 0; i < map.Tiles.GetLength(0); i++) {
            for (var j = 0; j < map.Tiles.GetLength(1); j++) {
                var baseTileClone = Instantiate(dummyTile);
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