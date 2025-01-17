using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class SubUI_RegistVoca : SubUI
    {
        public class RegistVocaScrollItemData : BaseScrollItemData
        {
            public string Name;
            public int Count;

            public RegistVocaScrollItemData(string name, int count)
            {
                Name = name;
                Count = count;
            }
        }

        #region // [Var] Unity //
        [Header("== RegistVoca ==")]
        [SerializeField]
        Button _btnClose;
        [SerializeField]
        Button _btnConfirm;

        [SerializeField]
        TMP_InputField _inputName;

        [SerializeField]
        ScrollRect _scrollList;
        #endregion

        #region // [Var] Data //
        string mRegistName;
        readonly List<RegistVocaScrollItemData> mDataList = new();

        readonly List<ItemUIRegistVocaInfo> mItemList = new();
        readonly Queue<ItemUIRegistVocaInfo> mItemPool = new();
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _btnClose.onClick.AddListener(OnClickCloseButton);
            _btnConfirm.onClick.AddListener(OnClickConfirmButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _btnClose.onClick.RemoveListener(OnClickCloseButton);
            _btnConfirm.onClick.RemoveListener(OnClickConfirmButton);

            Clear();
        }
        #endregion

        #region // [Func] Init //
        public override void Clear()
        {
            base.Clear();

            RemoveAllScrollItem();
        }

        protected override void InitUI()
        {
            base.InitUI();

            _inputName.text = string.Empty;
        }

        public void SetData()
        {
            InitUI();

            mDataList.Clear();
            var container = DataManager.Instance.GetVocaContainer();
            if (container != null)
            {
                foreach (var it in container.DataList)
                    mDataList.Add(new(it.OrderName, it.InfoList.Count));
            }

            UpdateUI();
        }
        #endregion

        #region // [Func] UpdateUI //
        void UpdateUI()
        {
            UpdateUIScroll();
        }

        void UpdateUIScroll()
        {
            RemoveAllScrollItem();

            foreach (var it in mDataList)
            {
                AddScrollItem(true, (ui) => {
                    var itemUI = ui as ItemUIRegistVocaInfo;
                    if (itemUI != null)
                    {
                        itemUI.transform.SetParent(_scrollList.content);
                        itemUI.transform.SetAsFirstSibling();
                        itemUI.SetData(it);
                    }
                });
            }
        }
        #endregion        

        #region // [Func] Callback //
        void OnClickCloseButton()
        {

        }

        void OnClickConfirmButton()
        {

        }
        #endregion
    }
}
