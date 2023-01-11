using UnityEditor;
using UnityEngine;

public class MapEditorWindow : EditorWindow {
    private Grid _grid;

    private void OnGUI() {
        GUILayout.Label("Map Generator", EditorStyles.boldLabel);
        GUILayout.Space(8);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid");
        _grid = (Grid)EditorGUILayout.ObjectField(_grid, typeof(Grid), true);
        GUILayout.EndHorizontal();
        GUILayout.Space(8);

        var mapControllerScript = _grid.GetComponent<MapController>();

        GUI.enabled = mapControllerScript != null;
        var generateMapButton = GUILayout.Button("Generate Map");

        if (!string.IsNullOrEmpty(_grid.name)) {
            GUI.enabled = true;
        }

        if (generateMapButton) {
            mapControllerScript.GenerateAndRenderMap();
        }
    }

    [MenuItem("Window/Map Editor")]
    private static void Init() {
        var window = (MapEditorWindow)GetWindow(typeof(MapEditorWindow));
        window.Show();
    }
}