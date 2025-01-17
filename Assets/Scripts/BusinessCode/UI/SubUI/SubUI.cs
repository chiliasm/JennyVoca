using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class SubUI : BaseUI
    {
        #region // [Var] Data //
        protected E_SubUI mSubUIType = E_SubUI.None;
        public E_SubUI SubUIType { get { return mSubUIType; } }
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

        public void SetType(E_SubUI type)
        {
            mSubUIType = type;
        }
        #endregion

        #region // [Func] Close //
        public override void CloseUI()
        {
            base.CloseUI();

            UIManager.Instance.CloseUI(mSubUIType);
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
