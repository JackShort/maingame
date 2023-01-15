using UnityEngine;

public class PlaceStructure : MonoBehaviour {
    [SerializeField] private Inventory inventory;
    [SerializeField] private Map<Structure> structureMap;
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject structurePrefab;

    private Camera _main;

    private void Start() {
        _main = Camera.main;
    }

    // TODO: update with unity input manager
    private void Update() {
        if (!Input.GetMouseButtonDown(0)) {
            return;
        }

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

    private void PlaceNewStructure(ArrayIndex2 mapCoords, Structure structure) {
        structureMap.Elements[mapCoords.r, mapCoords.c] = structure;
        var structureObject = Instantiate(structurePrefab,
            MapUtilities<Structure>.GetWorldPositionFromMapCoordinates(mapCoords, grid, structureMap),
            Quaternion.identity);
        structureObject.GetComponent<SpriteRenderer>().sprite = structure.sprite;
    }
}