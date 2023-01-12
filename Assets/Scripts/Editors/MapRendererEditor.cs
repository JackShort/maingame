using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CustomEditor(typeof(MapRenderer))]
public class MapRendererEditor : Editor {
    [FormerlySerializedAs("mapController")]
    public MapRenderer mapRenderer;

    private void OnEnable() {
        mapRenderer = (MapRenderer)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        GUILayout.Space(8);

        var generateMapButton = GUILayout.Button("Generate Map");
        if (generateMapButton) {
            mapRenderer.GenerateAndRenderMap();
        }
    }
}