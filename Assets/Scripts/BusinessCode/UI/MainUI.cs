using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class MainUI : BaseUI
    {
        #region // [Var] Data //
        protected E_MainUI mMainUIType = E_MainUI.None;
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

        #region // [Func] Init //
        protected override void InitUI()
        {
            base.InitUI();
        }

        public void SetType(E_MainUI type)
        {
            mMainUIType = type;
        }
        #endregion

        #region // [Func] Close //
        public override void CloseUI()
        {
            base.CloseUI();

            UIManager.Instance.CloseUI(mMainUIType);
        }
        #endregion

        #region // [Func] Callback //
        void OnClickTouchBgButton()
        {
            CloseUI();
        }
        #endregion
    }
}
