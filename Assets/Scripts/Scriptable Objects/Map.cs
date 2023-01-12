using UnityEngine;

[CreateAssetMenu]
public class Map : ScriptableObject {
    public int mapSize;
    public TileType[,] Tiles;
}