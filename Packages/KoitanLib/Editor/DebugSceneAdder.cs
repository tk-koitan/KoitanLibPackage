using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class DebugSceneAdder
{
    [MenuItem("KoitanLib/AddScene")]
    private static void AddScene()
    {
        Debug.Log("SceneAdd");
        var scenes = EditorBuildSettings.scenes;
        ArrayUtility.Add(ref scenes, new EditorBuildSettingsScene("Packages/com.koitan.koitanlib/KoitanLib/Scenes/DebugScene.unity", true));
        EditorBuildSettings.scenes = scenes;
    }
}
