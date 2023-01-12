using UnityEngine;
using UnityEngine.Tilemaps;

public class HoverTile : MonoBehaviour {
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Grid grid;
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
        var cellPosition = grid.WorldToCell(mousePosition);
        return cellPosition;
    }
}