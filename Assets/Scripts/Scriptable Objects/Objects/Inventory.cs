using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Inventory")]
public class Inventory : RuntimeSet<Item> {
    public int activeIndex;

    [CanBeNull]
    public Item GetActiveItem() {
        if (items.Count == 0) {
            return null;
        }

        return activeIndex >= items.Count || activeIndex < 0 ? items[0] : items[activeIndex];
    }
}