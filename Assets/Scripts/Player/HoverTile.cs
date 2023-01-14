using UnityEngine;
using UnityEngine.Tilemaps;

public class HoverTile : MonoBehaviour {
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Grid grid;
    [SerializeField] private Map<Resource> map;
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject itemPlacer;

    private Camera _main;
    private Vector3Int _previouslyHoveredTileCoordinates;

    private void Start() {
        _main = Camera.main;
    }

    // TODO: make function on item placer that updates the sprite etc. this is hacky
    private void Update() {
        var hoveredTileCoordinates = MapUtilities<Resource>.GetHoveredTileCoordinates(_main, grid);

        if (hoveredTileCoordinates == _previouslyHoveredTileCoordinates) {
            return;
        }

        if (!MapUtilities<Resource>.CheckIfTilePositionIsInMapBounds(hoveredTileCoordinates, map)) {
            if (itemPlacer.activeSelf) {
                itemPlacer.SetActive(false);
            }

            return;
        }

        if (!itemPlacer.activeSelf) {
            itemPlacer.SetActive(true);
        }

        var worldCoordinates = MapUtilities<Resource>.GetWorldPositionFromTileCoordinates(hoveredTileCoordinates, grid);
        itemPlacer.transform.position = new Vector3(worldCoordinates.x + 0.5f, worldCoordinates.y + 0.5f, 0);

        var activeItem = inventory.GetActiveItem();
        if (activeItem != null) {
            itemPlacer.GetComponent<SpriteRenderer>().sprite = activeItem.sprite;
        }

        _previouslyHoveredTileCoordinates = hoveredTileCoordinates;
    }
}