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
            var ui = UIManager.Instance.OpenUI(E_MsgUI.MsgUI_Common) as MsgUI_Common;
            if (ui != null)
                ui.SetData(msg);
        }

        public static void OpenToastUI(string msg)
        {
            var ui = UIManager.Instance.OpenUI(E_ToastUI.ToastUI_Common) as ToastUI_Common;
            if (ui != null)
            {
                SoundManager.Instance.Play(E_Sound_Item.Sfx_NewQuiz);
                ui.SetData(msg);
            }   
        }

        public static void PlayClickSound()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click);
        }
    }
}
