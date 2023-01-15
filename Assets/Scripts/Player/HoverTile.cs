using UnityEngine;

public class HoverTile : MonoBehaviour {
    [SerializeField] private Grid grid;
    [SerializeField] private Map<Resource> map;
    [SerializeField] private Inventory inventory;

    [SerializeField] private GameObject itemOverlay;

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
            if (itemOverlay.activeSelf) {
                itemOverlay.SetActive(false);
            }

            return;
        }

        var activeItem = inventory.GetActiveItem();
        if (activeItem != null) {
            if (activeItem.structure != null) {
                itemOverlay.GetComponent<SpriteRenderer>().sprite = activeItem.structure.sprite;

                var worldCoordinates =
                    MapUtilities<Resource>.GetWorldPositionFromTileCoordinates(hoveredTileCoordinates, grid);
                itemOverlay.transform.position = worldCoordinates;

                if (!itemOverlay.activeSelf) {
                    itemOverlay.SetActive(true);
                }
            }
            else {
                if (itemOverlay.activeSelf) {
                    itemOverlay.SetActive(false);
                }
            }
        }


        _previouslyHoveredTileCoordinates = hoveredTileCoordinates;
    }
}