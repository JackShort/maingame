using UnityEngine;

public abstract class Map<T> : ScriptableObject {
    public T[,] Elements;

    public bool IsInBounds(ArrayIndex2 index) {
        return index.r >= 0 && index.r < Elements.GetLength(0) && index.c >= 0 && index.c < Elements.GetLength(1);
    }

    public bool ContainsElementAtIndex(ArrayIndex2 index) {
        return IsInBounds(index) && Elements[index.r, index.c] != null;
    }
}