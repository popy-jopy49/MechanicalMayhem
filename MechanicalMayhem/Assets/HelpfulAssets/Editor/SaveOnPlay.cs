﻿#if UNITY_EDITOR // Could remove because in editor folder?
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class SaveOnPlay
{
    static SaveOnPlay()
    {
        EditorApplication.playModeStateChanged += Save;
    }

    private static void Save(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                if (ToggleSaveOnPlay.On)
                {
                    Debug.Log($"Auto-saving scene and any asset edits {SceneManager.GetActiveScene()}");
                    EditorSceneManager.SaveOpenScenes();
                    AssetDatabase.SaveAssets();
                }
                else Debug.Log("*** WARNING: Auto-Saving DISABLED *** " + SceneManager.GetActiveScene());
            }
        }
    }
}

public class ToggleSaveOnPlay : Editor
{
    public static bool On = true;

    [MenuItem("SWAssets/Saving On Play Options/Turn Off")]
    public static void TurnOffSaveOnPlay()
    {
        On = false;
    }

    [MenuItem("SWAssets/Saving On Play Options/Turn On")]
    public static void TurnOnSaveOnPlay()
    {
        On = true;
    }
}
#endif  // #if UNITY_EDITOR
