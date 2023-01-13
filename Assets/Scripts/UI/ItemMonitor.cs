using TMPro;
using UnityEngine;

public class ItemMonitor : MonoBehaviour {
    public Inventory inventory;
    public GameObject textGui;
    private int _lastCount;

    private TextMeshProUGUI _text;

    private void Start() {
        _text = textGui.GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        var count = inventory.items.Count;

        if (count == _lastCount) {
            return;
        }

        UpdateText();
        _lastCount = count;
    }

    private void OnValidate() {
        _text ??= textGui.GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    private void UpdateText() {
        _text.text = inventory.items.Count.ToString();
    }
}