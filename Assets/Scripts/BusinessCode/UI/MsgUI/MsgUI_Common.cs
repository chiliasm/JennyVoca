using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class MsgUI_Common : MsgUI
    {
        #region // [Var] Unity //
        [Header("== MsgUI Common ==")]
        [SerializeField]
        Button _btnClose;
        [SerializeField]
        Button _btnCancel;
        [SerializeField]
        Button _btnOK;
        [SerializeField]
        TMP_Text _textMsg;
        #endregion
    }
}
