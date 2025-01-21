using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class SubUI_SelectOrder : SubUI
    {
        public class SelectOrderScrollItemData : BaseScrollItemData
        {
            public string Name;
            public int Count;

            public SelectOrderScrollItemData(string name, int count)
            {
                Name = name;
                Count = count;
            }
        }

        #region // [Var] Unity //
        [Header("== SelectOrder ==")]
        [SerializeField]
        Button _btnClose;
        [SerializeField]
        Button _btnSelect;

        [SerializeField]
        ScrollRect _scrollList;
        #endregion

        #region // [Var] Data //
        readonly List<SelectOrderScrollItemData> mDataList = new();
        int mSelectID = -1;

        System.Action<string> mSelectCallback;
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _btnClose.onClick.AddListener(OnClickCloseButton);
            _btnSelect.onClick.AddListener(OnClickSelectButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _btnClose.onClick.RemoveListener(OnClickCloseButton);
            _btnSelect.onClick.RemoveListener(OnClickSelectButton);
        }
        #endregion

        #region // [Func] Set //
        public void SetData(System.Action<string> lpSelectCallback = null)
        {
            mSelectID = -1;
            mDataList.Clear();

            var container = DataManager.Instance.GetVocaContainer();
            if (container != null)
            {
                foreach (var it in container.DataList)
                    mDataList.Add(new(it.OrderName, it.InfoList.Count));
            }
            if (lpSelectCallback != null)
                mSelectCallback = lpSelectCallback;

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
                    var itemUI = ui as ItemUISelectOrderInfo;
                    if (itemUI != null)
                    {
                        itemUI.transform.SetParent(_scrollList.content);
                        itemUI.transform.SetAsFirstSibling();
                        itemUI.SetData(it, CallbackSelectItem);
                    }
                });
            }
        }
        #endregion

        #region // [Func] Callback //
        void OnClickCloseButton()
        {
            CommonFunc.PlayClickSound();

            CloseUI();
        }

        void OnClickSelectButton()
        {
            CommonFunc.PlayClickSound();

            if (mSelectID >= 0 && mDataList.Count > mSelectID)
            {
                var mainUI = UIManager.Instance.OpenUI(E_MainUI.MainUI_Lobby_Exam) as MainUI_Lobby_Exam;
                if (mainUI != null)
                    mainUI.SetData(mDataList[mSelectID].Name);
            }

            CloseUI();
        }

        void CallbackSelectItem(int id)
        {
            mSelectID = id;

            var list = GetScrollItemList();
            if (list != null && list.Count > id)
            {
                foreach (var it in list)
                {
                    var itemUI = it as ItemUISelectOrderInfo;
                    if (itemUI != null)
                        itemUI.SetSelect(itemUI.ID.Equals(id));
                }
            }
        }
        #endregion
    }
}
