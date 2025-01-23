using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class SubUI_AppSetting : SubUI
    {
        #region // [Var] Unity //
        [Header("== AppSetting ==")]
        [SerializeField]
        Button _btnClose;
        [SerializeField]
        Button _btnOK;
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _btnClose.onClick.AddListener(OnClickCloseButton);
            _btnOK.onClick.AddListener(OnClickOKButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _btnClose.onClick.RemoveListener(OnClickCloseButton);
            _btnOK.onClick.RemoveListener(OnClickOKButton);
        }
        #endregion

        #region // [Func] Callback //
        void OnClickCloseButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click);

            CloseUI();
        }

        void OnClickOKButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click);

            CloseUI();
        }
        #endregion
    }
}
