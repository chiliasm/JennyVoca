using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class ItemUIRegistOrderInfo : ItemUI
    {
        #region // [Var] Unity //
        [Header("== RegistVocaInfo ==")]
        [SerializeField]
        TMP_Text _textInfo;
        [SerializeField]
        Button _btnDelete;
        #endregion

        #region // [Var] Data //
        SubUI_RegistOrder.RegistOrderScrollItemData mInfo;

        System.Action<string> mDeleteCallback;
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _btnDelete.onClick.AddListener(OnClickDeleteButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _btnDelete.onClick.RemoveListener(OnClickDeleteButton);
        }
        #endregion

        #region // [Func] Set //
        public void SetData(SubUI_RegistOrder.RegistOrderScrollItemData info, System.Action<string> lpDeleteCallback = null)
        {
            mInfo = info;
            mDeleteCallback = lpDeleteCallback;

            UpdateUI();
        }
        #endregion

        #region // [Func] UpdateUI //
        void UpdateUI()
        {
            _textInfo.text = string.Format("{0} [{1}]", mInfo.Name, mInfo.Count);
        }
        #endregion

        #region // [Func] Callback //
        void OnClickDeleteButton()
        {
            CommonFunc.PlayClickSound();

            mDeleteCallback?.Invoke(mInfo.Name);
        }
        #endregion
    }
}
