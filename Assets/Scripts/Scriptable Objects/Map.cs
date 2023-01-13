using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Map")]
public class Map : ScriptableObject {
    public Resource[,] Tiles;
}