using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour {
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private int mapSize;
    [SerializeField] private BaseTile baseTile;

    private Grid _grid;
    private Camera _main;
    private TileType[,] _map;
    private Vector3Int _previouslyHoveredCell;

    private void Start() {
        _main = Camera.main;
        _grid = gameObject.GetComponent<Grid>();
        GenerateAndRenderMap();
    }

    private void Update() {
        var hoveredCell = GetHoveredTile();
        if (hoveredCell == _previouslyHoveredCell) {
            return;
        }

        if (tileMap.GetTile(_previouslyHoveredCell) is BaseTile tile) {
            tileMap.SetColor(_previouslyHoveredCell, tile.GetDefaultColor());
        }

        _previouslyHoveredCell = hoveredCell;
        if (tileMap.GetTile(hoveredCell)) {
            tileMap.SetColor(hoveredCell, Color.yellow);
        }
    }

    private Vector3Int GetHoveredTile() {
        var mousePosition = _main.ScreenToWorldPoint(Input.mousePosition);
        var cellPosition = _grid.WorldToCell(mousePosition);
        return cellPosition;
    }

    private void GenerateMap() {
        _map = new TileType[mapSize, mapSize];

        for (var i = 0; i < mapSize / 2; i++) {
            for (var j = 0; j < mapSize / 2; j++) {
                _map[i, j] = TileType.Base;
            }

            for (var k = 0; k < mapSize / 2; k++) {
                _map[i, k] = TileType.Mountain;
            }
        }

        for (var i = mapSize / 2; i < mapSize; i++) {
            for (var j = 0; j < mapSize / 2; j++) {
                _map[i, j] = TileType.Tree;
            }

            for (var k = mapSize / 2; k < mapSize; k++) {
                _map[i, k] = TileType.River;
            }
        }
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