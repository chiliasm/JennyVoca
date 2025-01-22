using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class MsgUI : BaseUI
    {
        #region // [Var] Data //
        protected E_MsgUI mMsgUIType = E_MsgUI.None;
        public E_MsgUI MsgUIType { get { return mMsgUIType; } }
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _btnTouchBg.onClick.AddListener(OnClickTouchBgButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _btnTouchBg.onClick.RemoveListener(OnClickTouchBgButton);
        }
        #endregion

        #region // [Func] Set //
        public void SetType(E_MsgUI type)
        {
            mMsgUIType = type;
        }
        #endregion

        #region // [Func] Close //
        public override void CloseUI()
        {
            base.CloseUI();

            UIManager.Instance.CloseUI(mMsgUIType);
        }
        #endregion

        #region // [Func] Callback //
        void OnClickTouchBgButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Close_UI);

            CloseUI();
        }
        #endregion
    }
}
