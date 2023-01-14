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

    private void Update() {
        var hoveredCell = MapUtilities.GetHoveredTileCoordinates(_main, grid);
        if (hoveredCell == _previouslyHoveredCell) {
            return;
        }

        if (tileMap.GetTile(_previouslyHoveredCell)) {
            var resource =
                MapUtilities.GetResourceAtTileCoordinates(_previouslyHoveredCell, map);

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
}