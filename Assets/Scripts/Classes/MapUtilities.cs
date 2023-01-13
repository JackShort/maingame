using JetBrains.Annotations;
using Unity.Mathematics;
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
    /// <returns>Map indices</returns>
    private static int2? GetMapCoordinatesFromTileCoordinates(int2 tileCoordinates, Map map) {
        var mapWidth = map.Tiles.GetLength(1);
        var mapHeight = map.Tiles.GetLength(0);

        var horizontalCenter = mapWidth / 2;
        var verticalCenter = mapHeight / 2;

        var x = tileCoordinates.x + verticalCenter;
        var y = tileCoordinates.y + horizontalCenter;

        if (x < 0 || x >= mapHeight || y < 0 || y >= mapWidth) {
            return null;
        }

        return new int2(x, y);
    }

    /// <summary>
    ///     Gets hovered tile coordinates
    /// </summary>
    /// <param name="tileCoordinates">Coordinates of the tile</param>
    /// <param name="map">The map to check</param>
    /// <returns>if the tile is in the bounds of the tilemap</returns>
    public static bool CheckIfTilePositionIsInMapBounds(int2 tileCoordinates, Map map) {
        var mapCoords = GetMapCoordinatesFromTileCoordinates(tileCoordinates, map);

        return mapCoords != null;
    }

    /// <summary>
    ///     Gets hovered map coordinates null if not in bounds
    /// </summary>
    /// <param name="main">Main camera</param>
    /// <param name="grid">The tilemap grid</param>
    /// <param name="map">The map to check</param>
    /// <returns>Coordinates of map if in bounds</returns>
    private static int2? GetHoveredMapCoordinates(Camera main, Grid grid, Map map) {
        var tileCoordinates = GetHoveredTileCoordinates(main, grid);
        var mapCoordinates = GetMapCoordinatesFromTileCoordinates(new int2(tileCoordinates.x, tileCoordinates.y), map);

        return mapCoordinates;
    }

    /// <summary>
    ///     Gets the resource at specified tile coordinates. Returns null if out of bounds
    /// </summary>
    /// <param name="tileCoordinates">The coordinates of the requested tile</param>
    /// <param name="map">The map to check against</param>
    /// <returns>Resource at tile location</returns>
    [CanBeNull]
    public static Resource GetResourceAtTileCoordinates(int2 tileCoordinates, Map map) {
        var coordinates = GetMapCoordinatesFromTileCoordinates(tileCoordinates, map);

        return coordinates != null ? map.Tiles[coordinates.Value.x, coordinates.Value.y] : null;
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
        return mapCoordinates != null ? map.Tiles[mapCoordinates.Value.x, mapCoordinates.Value.y] : null;
    }
}