using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class CommonFunc
    {
        public static void QuitApp()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
