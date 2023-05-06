using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace KoitanLib
{
    namespace Editor
    {
        public class SceneLaunchWindow : EditorWindow
        {
            private SceneAsset[] sceneArray;
            private Vector2 scrollPos = Vector2.zero;

            [MenuItem("KoitanLib/Scene Launcher")]
            static void Open()
            {
                GetWindow<SceneLaunchWindow>("Scene Launcher");
            }

            void OnFocus()
            {
                
            }

            void OnGUI()
            {
                EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
                this.scrollPos = EditorGUILayout.BeginScrollView(this.scrollPos);
                EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
                foreach (var scene in scenes)
                {
                    string[] strs = scene.path.Split('/');

                    string sceneName = strs[strs.Length - 1].Replace(".unity", string.Empty);
                    if (GUILayout.Button(sceneName))
                    {
                        if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                        {
                            EditorSceneManager.OpenScene(scene.path);
                        }
                    }
                }
                EditorGUILayout.EndScrollView();
                EditorGUI.EndDisabledGroup();
            }
        }
    }
}