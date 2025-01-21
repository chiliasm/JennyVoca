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


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _btnClose.onClick.AddListener(OnClickCloseButton);
            _btnCancel.onClick.AddListener(OnClickCancelButton);
            _btnOK.onClick.AddListener(OnClickOKButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _btnClose.onClick.RemoveListener(OnClickCloseButton);
            _btnCancel.onClick.RemoveListener(OnClickCancelButton);
            _btnOK.onClick.RemoveListener(OnClickOKButton);
        }
        #endregion

        #region // [Func] Init //
        protected override void InitUI()
        {
            base.InitUI();
        }

        public void SetData(string msg)
        {
            _textMsg.text = msg;
        }
        #endregion

        #region // [Func] Callback //
        void OnClickCloseButton()
        {
            CommonFunc.PlayClickSound();

            CloseUI();
        }

        void OnClickCancelButton()
        {
            CommonFunc.PlayClickSound();

            CloseUI();
        }

        void OnClickOKButton()
        {
            CommonFunc.PlayClickSound();

            CloseUI();
        }
        #endregion
    }
}
