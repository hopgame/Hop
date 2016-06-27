using UnityEditor;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(Cameracontroller))]
public class CameraEditor : Editor {
    public override void OnInspectorGUI() {
        Cameracontroller controller = (Cameracontroller) target;
        if (DrawDefaultInspector()) {
            controller.DebugScene();
        }
        if (GUILayout.Button("Generate")) {
            controller.DebugScene();
        }
    }
}
