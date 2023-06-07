#if UNITY_EDITOR
using UnityEditor;

namespace SWAssets
{
    public class EditorUtilities : Editor
    {
        //Public Methods:
        /// <summary>
        /// Global error for the Editor.
        /// </summary>
        /// <param name="errorMessage">Error message.</param>
        public static void Error (string errorMessage)
        {
            EditorUtility.DisplayDialog ("Framework Error", errorMessage, "OK");
        }
    }
}
#endif