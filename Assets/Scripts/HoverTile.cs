using UnityEngine;
using UnityEngine.Tilemaps;

public class HoverTile : MonoBehaviour {
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Grid grid;
    [SerializeField] private Map map;

    private Camera _main;
    private Vector3Int _previouslyHoveredCell;

    private void Start() {
        _main = Camera.main;
    }

    // Update is called once per frame
    private void Update() {
        var hoveredCell = GetHoveredTile();
        if (hoveredCell == _previouslyHoveredCell) {
            return;
        }

        if (tileMap.GetTile(_previouslyHoveredCell)) {
            var resource = GetTileResource(_previouslyHoveredCell);

            if (resource) {
                tileMap.SetColor(_previouslyHoveredCell, resource.color);
            }
        }

        _previouslyHoveredCell = hoveredCell;
        if (!tileMap.GetTile(hoveredCell)) {
            return;
        }

        tileMap.SetColor(hoveredCell, Color.yellow);
    }

    private Vector3Int GetHoveredTile() {
        var mousePosition = _main.ScreenToWorldPoint(Input.mousePosition);
        var cellPosition = grid.WorldToCell(mousePosition);
        return cellPosition;
    }

    private Resource GetTileResource(Vector3Int cellPosition) {
        var horizontalCenter = map.Tiles.GetLength(0) / 2;
        var verticalCenter = map.Tiles.GetLength(1) / 2;

        var x = cellPosition.x + verticalCenter;
        var y = cellPosition.y + horizontalCenter;


        if (x < 0 || x >= map.Tiles.GetLength(0) || y < 0 || y >= map.Tiles.GetLength(1)) {
            return null;
        }

        return map.Tiles[x, y];
    }
}