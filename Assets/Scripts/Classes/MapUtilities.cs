using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///     Utility class for map functions
/// </summary>
public static class MapUtilities<T> {
    /// <summary>
    ///     Gets hovered tile coordinates
    /// </summary>
    /// <param name="main">The main camera</param>
    /// <param name="grid">The grid of the tilemap</param>
    /// <returns>The coordinates of the tile</returns>
    public static Vector3Int GetHoveredTileCoordinates(Camera main, Grid grid) {
        return grid.WorldToCell(main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
    }

    /// <summary>
    ///     Gets the map indices from tile coordinates
    /// </summary>
    /// <param name="tileCoordinates">Coordinates of the tile</param>
    /// <param name="map">The map to check</param>
    /// <returns>Map indices (row, col)</returns>
    private static ArrayIndex2? GetMapIndexFromTileCoordinates(Vector3Int tileCoordinates, Map<T> map) {
        var mapWidth = map.Elements.GetLength(1);
        var mapHeight = map.Elements.GetLength(0);

        var horizontalOffset = mapWidth / 2;
        var verticalOffset = mapHeight / 2;

        var r = tileCoordinates.y + verticalOffset;
        var c = tileCoordinates.x + horizontalOffset;

        var mapCoords = new ArrayIndex2(r, c);

        return map.IsInBounds(mapCoords) ? mapCoords : null;
    }

    /// <summary>
    ///     Gets hovered tile coordinates
    /// </summary>
    /// <param name="tileCoordinates">Coordinates of the tile</param>
    /// <param name="map">The map to check</param>
    /// <returns>if the tile is in the bounds of the tilemap</returns>
    public static bool CheckIfTilePositionIsInMapBounds(Vector3Int tileCoordinates, Map<T> map) {
        var mapCoords = GetMapIndexFromTileCoordinates(tileCoordinates, map);

        return mapCoords != null;
    }

    /// <summary>
    ///     Gets hovered map coordinates null if not in bounds
    /// </summary>
    /// <param name="main">Main camera</param>
    /// <param name="grid">The tilemap grid</param>
    /// <param name="map">The map to check</param>
    /// <returns>Coordinates of map if in bounds</returns>
    public static ArrayIndex2? GetHoveredMapCoordinates(Camera main, Grid grid, Map<T> map) {
        var tileCoordinates = GetHoveredTileCoordinates(main, grid);
        var mapCoordinates = GetMapIndexFromTileCoordinates(tileCoordinates, map);

        return mapCoordinates;
    }

    /// <summary>
    ///     Gets the resource at specified tile coordinates. Returns null if out of bounds
    /// </summary>
    /// <param name="tileCoordinates">The coordinates of the requested tile</param>
    /// <param name="map">The map to check against</param>
    /// <returns>Resource at tile location</returns>
    public static T GetElementAtTileCoordinates(Vector3Int tileCoordinates, Map<T> map) {
        var coordinates = GetMapIndexFromTileCoordinates(tileCoordinates, map);

        return coordinates != null ? map.Elements[coordinates.Value.r, coordinates.Value.c] : default;
    }

    /// <summary>
    ///     Gets resource at hovered tile coordinates. Returns null if out of bounds
    /// </summary>
    /// <param name="main">Main camera</param>
    /// <param name="grid">The tilemap grid</param>
    /// <param name="map">The map to check</param>
    /// <returns>The resource if in bounds</returns>
    public static T GetElementFromHoveredTile(Camera main, Grid grid, Map<T> map) {
        var mapCoordinates = GetHoveredMapCoordinates(main, grid, map);
        return mapCoordinates != null ? map.Elements[mapCoordinates.Value.r, mapCoordinates.Value.c] : default;
    }

    public static Vector3 GetWorldPositionFromTileCoordinates(Vector3Int tileCoordinates, Grid grid) {
        var worldCoordinates = grid.CellToWorld(tileCoordinates);

        // applying offset to center the tile
        return new Vector3(worldCoordinates.x + 0.5f, worldCoordinates.y + 0.5f, worldCoordinates.z);
    }

    public static Vector3 GetWorldPositionFromMapCoordinates(ArrayIndex2 mapCoordinates, Grid grid, Map<T> map) {
        var horizontalOffset = map.Elements.GetLength(1) / 2;
        var verticalOffset = map.Elements.GetLength(0) / 2;

        var tileCoordinates = new Vector3Int(mapCoordinates.c - horizontalOffset, mapCoordinates.r - verticalOffset, 0);
        return GetWorldPositionFromTileCoordinates(tileCoordinates, grid);
    }
}