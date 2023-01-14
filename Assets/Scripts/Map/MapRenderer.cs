using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRenderer : MonoBehaviour {
    public Map<Resource> resourceMap;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private IntegerReference mapSize;
    [SerializeField] private DummyTile dummyTile;

    private void Start() {
        GenerateAndRenderMap();
    }

    private void GenerateMap() {
        var mapGenerator = GetComponent<MapGenerator>();
        mapGenerator.Generate(ref resourceMap, mapSize.Value);
    }

    private void RenderMap() {
        tileMap.ClearAllTiles();

        var horizontalOffset = resourceMap.Elements.GetLength(1) / 2;
        var verticalOffset = resourceMap.Elements.GetLength(0) / 2;

        for (var y = 0; y < resourceMap.Elements.GetLength(0); y++) {
            for (var x = 0; x < resourceMap.Elements.GetLength(1); x++) {
                var baseTileClone = Instantiate(dummyTile);
                baseTileClone.color = resourceMap.Elements[y, x].color;
                baseTileClone.sprite = resourceMap.Elements[y, x].tileSprite;
                tileMap.SetTile(new Vector3Int(x - horizontalOffset, y - verticalOffset, 0), baseTileClone);
            }
        }
    }

    public void GenerateAndRenderMap() {
        GenerateMap();
        RenderMap();
    }
}