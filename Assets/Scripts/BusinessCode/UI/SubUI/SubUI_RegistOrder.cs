using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class SubUI_RegistOrder : SubUI
    {
        public class RegistOrderScrollItemData : BaseScrollItemData
        {
            public string Name;
            public int Count;

            public RegistOrderScrollItemData(string name, int count)
            {
                Name = name;
                Count = count;
            }
        }

        #region // [Var] Unity //
        [Header("== RegistOrder ==")]
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
        readonly List<RegistOrderScrollItemData> mItemList = new();

        System.Action<string> mConfirmCallback;
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
        }
        #endregion

        #region // [Func] Init //
        void InitUI()
        {
            _inputName.text = string.Empty;
        }

        public void SetData(System.Action<string> lpConfirmCallback = null)
        {
            InitUI();

            mItemList.Clear();

            var container = DataManager.Instance.GetVocaContainer();
            if (container != null)
            {
                foreach (var it in container.DataList)
                    mItemList.Add(new(it.OrderName, it.InfoList.Count));
            }
            if (lpConfirmCallback != null)
                mConfirmCallback = lpConfirmCallback;

            UpdateUI();
        }
        #endregion

        #region // [Func] UpdateUI //
        void UpdateUI()
        {
            UpdateUIName();
            UpdateUIScroll();
        }

        void UpdateUIName()
        {
            var orderName = _inputName.text;
            if (string.IsNullOrWhiteSpace(orderName))
                orderName = MakeOrderName();

            _inputName.text = orderName;
        }

        void UpdateUIScroll()
        {
            RemoveAllScrollItem();

            foreach (var it in mItemList)
            {
                AddScrollItem(true, (ui) => {
                    var itemUI = ui as ItemUIRegistOrderInfo;
                    if (itemUI != null)
                    {
                        itemUI.transform.SetParent(_scrollList.content);
                        itemUI.transform.SetAsFirstSibling();
                        itemUI.SetData(it, CallbackDeleteItem);
                    }
                });
            }
        }
        #endregion

        #region // [Func] Util //
        string MakeOrderName()
        {
            var dateName = string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var orderName = dateName;
            int retryCount = 0;
            while (true)
            {
                if (IsValidOrderName(orderName))
                    break;
                else
                    orderName = string.Format("{0} ({1})", dateName, retryCount);
                
                retryCount++;
                if (retryCount > 10)
                {
                    orderName = MakeOrderNameRandom(dateName);
                    break;
                }
            }
            return orderName;
        }

        string MakeOrderNameRandom(string baseName)
        {
            return string.Format("{0} ({1})", baseName, DateTime.Now.Ticks);
        }

        bool IsValidOrderName(string orderName)
        {
            if (string.IsNullOrWhiteSpace(orderName))
                return false;

            var isValid = true;
            var container = DataManager.Instance.GetVocaContainer();
            if (container != null)
            {
                foreach (var it in container.DataList)
                {
                    if (orderName == it.OrderName)
                    {
                        isValid = false;
                        break;
                    }   
                }
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }
        #endregion

        #region // [Func] Callback //
        void OnClickCloseButton()
        {
            CommonFunc.PlayClickSound();

            CloseUI();
        }

        void OnClickConfirmButton()
        {
            CommonFunc.PlayClickSound();

            var orderName = _inputName.text;
            if (IsValidOrderName(orderName) == false)
                orderName = MakeOrderNameRandom(orderName);

            mConfirmCallback?.Invoke(orderName);

            CloseUI();
        }

        void CallbackDeleteItem(string orderName)
        {
            if (DataManager.Instance.RemoveVocaOrder(orderName))
                SetData();
        }
        #endregion
    }
}
