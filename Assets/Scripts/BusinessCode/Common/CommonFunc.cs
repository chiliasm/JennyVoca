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

        public static void OpenMsgUI(string msg)
        {
            var msgUI = UIManager.Instance.OpenUI(E_MsgUI.MsgUI_Common) as MsgUI_Common;
            if (msgUI != null)
                msgUI.SetData(msg);
        }

        public static void PlayClickSound()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click_Bubble);
        }
    }
}
