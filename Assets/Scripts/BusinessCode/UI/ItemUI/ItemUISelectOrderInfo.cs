using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class ItemUISelectOrderInfo : ItemUI
    {
        #region // [Var] Unity //
        [Header("== SelectOrderInfo ==")]
        [SerializeField]
        TMP_Text _textName;
        [SerializeField]
        Button _btnBg;
        [SerializeField]
        Image _imgBg;
        #endregion

        #region // [Var] Data //
        SubUI_SelectOrder.SelectOrderScrollItemData mInfo;
        bool mIsSelect = false;

        System.Action<int> mSelectCallback;

        readonly Color COLOR_BG_DEFAULT = new(0.96f, 0.85f, 0.59f, 1f);
        readonly Color COLOR_BG_SELECT = new(0.85f, 0.85f, 0.85f, 1f);
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _btnBg.onClick.AddListener(OnClickBgButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _btnBg.onClick.RemoveListener(OnClickBgButton);
        }
        #endregion

        #region // [Func] Set //
        public void SetData(SubUI_SelectOrder.SelectOrderScrollItemData info, System.Action<int> lpSelectCallback = null)
        {
            mInfo = info;
            if (lpSelectCallback != null)
                mSelectCallback = lpSelectCallback;

            UpdateUI();
        }

        public void SetSelect(bool isSelect)
        {
            mIsSelect = isSelect;

            UpdateUISelect();
        }
        #endregion

        #region // [Func] UpdateUI //
        void UpdateUI()
        {
            _textName.text = string.Format("{0} [{1}]", mInfo.Name, mInfo.Count);

            UpdateUISelect();
        }

        void UpdateUISelect()
        {
            _imgBg.color = mIsSelect ? COLOR_BG_SELECT : COLOR_BG_DEFAULT;
        }
        #endregion

        #region // [Func] Callback //
        void OnClickBgButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click_Item);

            mSelectCallback?.Invoke(ID);
        }
        #endregion
    }
}
