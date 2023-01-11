using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType {
    Base,
    Mountain,
    Tree,
    River
}

public class BaseTile : Tile {
    public TileType tileType;

    private readonly Color[] _tileColors = {
        Color.white,
        Color.gray,
        Color.green,
        Color.cyan
    };

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData) {
        base.GetTileData(position, tilemap, ref tileData);
        tileData.color = _tileColors[(int)tileType];
    }

    public Color GetDefaultColor() {
        return _tileColors[(int)tileType];
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/BaseTile")]
    public static void CreateBaseTile() {
        var path = EditorUtility.SaveFilePanelInProject("Save Base Tile", "New Base Tile", "Asset", "Save Base Tile",
            "Assets/Tiles/Sprites");
        if (path == "") {
            return;
        }

        AssetDatabase.CreateAsset(CreateInstance<BaseTile>(), path);
    }
#endif
}