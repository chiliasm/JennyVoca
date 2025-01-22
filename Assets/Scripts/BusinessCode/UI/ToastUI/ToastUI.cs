using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class ToastUI : BaseUI
    {
        #region // [Var] Unity //
        [Header("== ToastUI ==")]
        [SerializeField]
        float _durationTime = 3f;
        #endregion

        #region // [Var] Data //
        protected E_ToastUI mToastUIType = E_ToastUI.None;
        public E_ToastUI ToastUIType => mToastUIType;

        Coroutine mCorAutoClose;
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

            ClearCoroutine(ref mCorAutoClose);

            _btnTouchBg.onClick.RemoveListener(OnClickTouchBgButton);
        }
        #endregion

        #region // [Func] Set //
        public void SetType(E_ToastUI type)
        {
            mToastUIType = type;
        }
        #endregion

        #region // [Func] Close //
        public override void CloseUI()
        {
            base.CloseUI();

            UIManager.Instance.CloseUI(mToastUIType);
        }
        #endregion

        #region // [Func] AutoClosing //
        protected void AutoClose()
        {
            if (ClearCoroutine(ref mCorAutoClose))
                mCorAutoClose = StartCoroutine(CorAutoClose());
        }

        IEnumerator CorAutoClose()
        {
            if (_durationTime > 0f)
                yield return new WaitForSeconds(_durationTime);
            
            CloseUI();
            mCorAutoClose = null;
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
