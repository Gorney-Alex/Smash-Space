#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace Logic.App
{
    public sealed class ApplicationFinisher
    {
        public void Finish()
        {
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            
            #else
            Application.Quit();
            
            #endif
        }
    }
}