using JetBrains.Annotations;
using UnityEngine;

/// <summary>
///     Utility class for map functions
/// </summary>
public static class MapUtilities {
    /// <summary>
    ///     Gets hovered tile coordinates
    /// </summary>
    /// <param name="main">The main camera</param>
    /// <param name="grid">The grid of the tilemap</param>
    /// <returns>The coordinates of the tile</returns>
    public static Vector3Int GetHoveredTileCoordinates(Camera main, Grid grid) {
        return grid.WorldToCell(main.ScreenToWorldPoint(Input.mousePosition));
    }

    /// <summary>
    ///     Gets the map indices from tile coordinates
    /// </summary>
    /// <param name="tileCoordinates">Coordinates of the tile</param>
    /// <param name="map">The map to check</param>
    /// <returns>Map indices (row, col)</returns>
    private static ArrayIndex2? GetMapIndexFromTileCoordinates(Vector3Int tileCoordinates, Map map) {
        var mapWidth = map.Tiles.GetLength(1);
        var mapHeight = map.Tiles.GetLength(0);

        var horizontalOffset = mapWidth / 2;
        var verticalOffset = mapHeight / 2;

        var r = tileCoordinates.y + verticalOffset;
        var c = tileCoordinates.x + horizontalOffset;

        if (c < 0 || c >= mapWidth || r < 0 || r >= mapHeight) {
            return null;
        }

        return new ArrayIndex2(r, c);
    }

    /// <summary>
    ///     Gets hovered tile coordinates
    /// </summary>
    /// <param name="tileCoordinates">Coordinates of the tile</param>
    /// <param name="map">The map to check</param>
    /// <returns>if the tile is in the bounds of the tilemap</returns>
    public static bool CheckIfTilePositionIsInMapBounds(Vector3Int tileCoordinates, Map map) {
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
    private static ArrayIndex2? GetHoveredMapCoordinates(Camera main, Grid grid, Map map) {
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
    [CanBeNull]
    public static Resource GetResourceAtTileCoordinates(Vector3Int tileCoordinates, Map map) {
        var coordinates = GetMapIndexFromTileCoordinates(tileCoordinates, map);

        return coordinates != null ? map.Tiles[coordinates.Value.r, coordinates.Value.c] : null;
    }

    /// <summary>
    ///     Gets resource at hovered tile coordinates. Returns null if out of bounds
    /// </summary>
    /// <param name="main">Main camera</param>
    /// <param name="grid">The tilemap grid</param>
    /// <param name="map">The map to check</param>
    /// <returns>The resource if in bounds</returns>
    [CanBeNull]
    public static Resource GetResourceFromHoveredTile(Camera main, Grid grid, Map map) {
        var mapCoordinates = GetHoveredMapCoordinates(main, grid, map);
        return mapCoordinates != null ? map.Tiles[mapCoordinates.Value.r, mapCoordinates.Value.c] : null;
    }

    public static Vector3 GetWorldPositionFromTileCoordinates(Vector3Int tileCoordinates, Grid grid) {
        return grid.CellToWorld(tileCoordinates);
    }

    public static Vector3 GetWorldPositionFromMapCoordinates(Vector3Int mapCoordinates, Grid grid, Map map) {
        var centerX = map.Tiles.GetLength(1) / 2;
        var centerY = map.Tiles.GetLength(0) / 2;

        var tileCoordinates = new Vector3Int(mapCoordinates.x - centerX, mapCoordinates.y - centerY, 0);
        return grid.CellToWorld(tileCoordinates);
    }
}