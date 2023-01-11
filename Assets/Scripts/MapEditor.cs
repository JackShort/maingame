using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapController))]
public class MapEditor : Editor {
    public MapController mapController;

    private void OnEnable() {
        mapController = (MapController)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        GUILayout.Space(8);

        var generateMapButton = GUILayout.Button("Generate Map");
        if (generateMapButton) {
            mapController.GenerateAndRenderMap();
        }
    }
}