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

        var horizontalOffset = map.Tiles.GetLength(1) / 2;
        var verticalOffset = map.Tiles.GetLength(0) / 2;

        for (var y = 0; y < map.Tiles.GetLength(0); y++) {
            for (var x = 0; x < map.Tiles.GetLength(1); x++) {
                var baseTileClone = Instantiate(dummyTile);
                baseTileClone.color = map.Tiles[y, x].color;
                baseTileClone.sprite = map.Tiles[y, x].tileSprite;
                tileMap.SetTile(new Vector3Int(x - horizontalOffset, y - verticalOffset, 0), baseTileClone);
            }
        }
    }

    public void GenerateAndRenderMap() {
        GenerateMap();
        RenderMap();
    }
}