using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraManager))]
public class CameraBoundsEditor : Editor
{

    private CameraManager cameraManager;

    void OnEnable()
    {
        cameraManager = (CameraManager)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Set Max Zoom Out"))
        {
            cameraManager.maxSize = cameraManager.GetCameraSize();
            cameraManager.minPos = cameraManager.GetCameraBottomLeft();
        }

        if (GUILayout.Button("Set Max Zoom In"))
        {
            cameraManager.minSize = cameraManager.GetCameraSize();
        }
    }
}
