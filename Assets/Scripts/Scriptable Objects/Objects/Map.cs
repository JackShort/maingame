using UnityEngine;

public abstract class Map<T> : ScriptableObject {
    public T[,] Elements;
}