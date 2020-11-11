using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TextTexture))]
[CanEditMultipleObjects]
public class TextTextureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Render"))
        {
            if (target) (target as TextTexture).Render();
        }
    }
}
