using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (GameZone))]
public class GameZoneEditor : Editor {

    public override void OnInspectorGUI() {
        GameZone gameZone = (GameZone)target;

        if (DrawDefaultInspector()) {
            if (gameZone.autoUpdate) {
                gameZone.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate")) {
            gameZone.GenerateMap();
        }
    }
}