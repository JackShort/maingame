using Unity.Mathematics;
using UnityEngine;

public class ItemPlacer : MonoBehaviour {
    public Inventory inventory;
    [SerializeField] private Grid grid;
    [SerializeField] private Map map;

    private Camera _main;
    private Vector3Int _previouslyHoveredTile;

    private void Start() {
        _main = Camera.main;
    }

    // Update is called once per frame
    private void Update() {
        CheckHoveredTile();
    }

    private void CheckHoveredTile() {
        var hoveredTile = MapUtilities.GetHoveredTileCoordinates(_main, grid);

        if (!MapUtilities.CheckIfTilePositionIsInMapBounds(new int2(hoveredTile.x, hoveredTile.y), map)) {
            return;
        }

        if (hoveredTile == _previouslyHoveredTile) {
            return;
        }

        Debug.Log("hovering something new");
    }
}