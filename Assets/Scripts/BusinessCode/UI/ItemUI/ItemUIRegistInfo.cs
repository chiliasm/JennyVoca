using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class ItemUIRegistInfo : ItemUI
    {
        #region // [Var] Unity //
        [Header("== RegistInfo ==")]
        [SerializeField]
        Button _btnBg;
        [SerializeField]
        Image _imgBg;
        [SerializeField]
        TMP_Text _textEn;
        [SerializeField]
        TMP_Text _textKr;
        [SerializeField]
        GameObject _goBtnGroup;
        [SerializeField]
        Button _btnModify;
        [SerializeField]
        Button _btnDelete;
        #endregion

        #region // [Var] Data //
        MainUI_Lobby_Regist.RegistScrollItemData mData;
        bool mIsSelected = false;

        System.Action<int> mSelectCallback;
        System.Action<int> mModifyCallback;
        System.Action<int> mDeleteCallback;

        readonly Color COLOR_BG_DEFAULT = new(0.96f, 0.85f, 0.59f, 1f);
        readonly Color COLOR_BG_SELECT = new(0.85f, 0.85f, 0.85f, 1f);
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _btnBg.onClick.AddListener(OnClickBgButton);
            _btnModify.onClick.AddListener(OnClickModifyButton);
            _btnDelete.onClick.AddListener(OnClickDeleteButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _btnBg.onClick.RemoveListener(OnClickBgButton);
            _btnModify.onClick.RemoveListener(OnClickModifyButton);
            _btnDelete.onClick.RemoveListener(OnClickDeleteButton);
        }
        #endregion

        #region // [Func] Set //
        public void SetData(MainUI_Lobby_Regist.RegistScrollItemData data, System.Action<int> lpSelectCallback = null, System.Action<int> lpModifyCallback = null, System.Action<int> lpDeleteCallback = null)
        {
            mData = data;
            if (lpSelectCallback != null)
                mSelectCallback = lpSelectCallback;
            if (lpModifyCallback != null)
                mModifyCallback = lpModifyCallback; 
            if (lpDeleteCallback != null)
                mDeleteCallback = lpDeleteCallback;
            mIsSelected = false;

            UpdateUI();
        }

        public void SetSelect(bool isSelect)
        {
            mIsSelected = isSelect;

            UpdateUISelect();
        }
        #endregion

        #region // [Func] UpdateUI //
        void UpdateUI()
        {
            _textEn.text = mData.En;
            _textKr.text = string.Format("({0})", mData.Kr);

            UpdateUISelect();
        }

        void UpdateUISelect()
        {
            _goBtnGroup.SetActive(mIsSelected);
            _imgBg.color = mIsSelected ? COLOR_BG_SELECT : COLOR_BG_DEFAULT;
        }
        #endregion

        #region // [Func] Callback //
        void OnClickBgButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click_Item);

            mSelectCallback?.Invoke(mID);
        }

        void OnClickModifyButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click);

            mModifyCallback?.Invoke(mID);
        }

        void OnClickDeleteButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click);

            mDeleteCallback?.Invoke(mID);
        }
        #endregion
    }
}
