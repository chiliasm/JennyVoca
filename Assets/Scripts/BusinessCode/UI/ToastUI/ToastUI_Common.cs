using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Jenny
{
    public class ToastUI_Common : ToastUI
    {
        #region // [Var] Unity //
        [Header("== Common ==")]
        [SerializeField]
        TMP_Text _textMsg;
        #endregion

        #region // [Func] Set //
        public void SetData(string msg)
        {
            _textMsg.text = msg;

            AutoClose();
        }
        #endregion
    }
}
