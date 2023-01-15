using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceStructure : MonoBehaviour {
    [SerializeField] private BoolReference inPlacingMode;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Map<Structure> structureMap;
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject structurePrefab;

    private Camera _main;

    private bool _shouldPlaceStructure;

    private void Start() {
        _main = Camera.main;
    }

    private void Update() {
        if (!_shouldPlaceStructure) {
            return;
        }

        if (!inPlacingMode.Value) {
            _shouldPlaceStructure = false;
            return;
        }

        PlaceActiveStructureAtMouseLocation();
    }

    [UsedImplicitly]
    public void OnPrimaryClick(InputAction.CallbackContext context) {
        if (!inPlacingMode.Value) {
            return;
        }

        if (context.performed) {
            _shouldPlaceStructure = true;
        } else if (context.canceled) {
            _shouldPlaceStructure = false;
        }
    }

    private void PlaceNewStructure(ArrayIndex2 mapCoords, Structure structure) {
        structureMap.Elements[mapCoords.r, mapCoords.c] = structure;
        var structureObject = Instantiate(structurePrefab,
            MapUtilities<Structure>.GetWorldPositionFromMapCoordinates(mapCoords, grid, structureMap),
            Quaternion.identity);
        structureObject.GetComponent<SpriteRenderer>().sprite = structure.sprite;
    }

    private void PlaceActiveStructureAtMouseLocation() {
        var activeItemStructure = inventory.GetActiveItem()?.structure;
        if (activeItemStructure == null) {
            return;
        }

        var mapCoords = MapUtilities<Structure>.GetHoveredMapCoordinates(_main, grid, structureMap);
        if (mapCoords == null || structureMap.ContainsElementAtIndex(mapCoords.Value)) {
            return;
        }

        PlaceNewStructure(mapCoords.Value, activeItemStructure);
    }
}