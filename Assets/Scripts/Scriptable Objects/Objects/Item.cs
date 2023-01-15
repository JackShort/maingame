using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Game Element/Item")]
public class Item : ScriptableObject {
    public Sprite sprite;
    [CanBeNull] public Structure structure;
}