using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(IntegerReference))]
public class IntegerReferencePropertyDrawer : PropertyDrawer {
    /// <summary>
    ///     Options to display in the popup to select constant or variable.
    /// </summary>
    private readonly string[] _popupOptions;

    /// <summary> Cached style to use to draw the popup button. </summary>
    private GUIStyle _popupStyle;

    public IntegerReferencePropertyDrawer() {
        _popupOptions = new[] { "Use Constant", "Use Variable" };
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        _popupStyle ??= new GUIStyle(GUI.skin.GetStyle("PaneOptions")) {
            imagePosition = ImagePosition.ImageOnly
        };

        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        EditorGUI.BeginChangeCheck();

        // Get properties
        var useConstant = property.FindPropertyRelative("useConstant");
        var constantValue = property.FindPropertyRelative("constantValue");
        var variable = property.FindPropertyRelative("variable");

        // Calculate rect for configuration button
        var buttonRect = new Rect(position);
        buttonRect.yMin += _popupStyle.margin.top;
        buttonRect.width = _popupStyle.fixedWidth + _popupStyle.margin.right;
        position.xMin = buttonRect.xMax;

        // Store old indent level and set it to 0, the PrefixLabel takes care of it
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, _popupOptions, _popupStyle);

        useConstant.boolValue = result == 0;

        EditorGUI.PropertyField(position,
            useConstant.boolValue ? constantValue : variable,
            GUIContent.none);

        if (EditorGUI.EndChangeCheck()) {
            property.serializedObject.ApplyModifiedProperties();
        }

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}