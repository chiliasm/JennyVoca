using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class SubUI_RegistVoca : SubUI
    {
        #region // [Var] Unity //
        [SerializeField]
        Button _btnClose;
        [SerializeField]
        Button _btnConfirm;

        [SerializeField]
        TMP_InputField _inputName;

        [SerializeField]
        ScrollRect _scrollList;
        [SerializeField]
        GameObject _itemObject;
        [SerializeField]
        Transform _trItemPool;
        #endregion

        #region // [Var] Data //
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
        #endregion

        #region // [Func] UpdateUI //
        void UpdateUI()
        {

        }

        void UpdateUIScroll()
        {

        }
        #endregion

        #region // [Func] Scroll //
        ItemUIRegistVocaInfo GetOrNewItem()
        {
            ItemUIRegistVocaInfo info = null;
            if (mItemPool.Count > 0)
                info = mItemPool.Dequeue();
            else
            {
                var o = GameObject.Instantiate(_itemObject);
                o.SetActive(false);
                if (o.TryGetComponent<ItemUIRegistVocaInfo>(out var comp))
                    info = comp;
            }
            if (info != null)
            {
                info.transform.SetParent(_scrollList.content);
                info.transform.SetAsFirstSibling();
            }
            return info;
        }

        void RefreshItemID()
        {
            for (int i = 0; i < mItemList.Count; i++)
                mItemList[i].ID = i;
        }

        void AddScrollItem(string info)
        {
            if (string.IsNullOrWhiteSpace(info))
                return;

            var itemInfo = GetOrNewItem();
            if (itemInfo != null)
            {
                itemInfo.SetData(info);
                itemInfo.Show(true, true, () => {
                });
                mItemList.Add(itemInfo);

                RefreshItemID();
            }
        }

        void RemoveScrollItem(int id)
        {
            if (mItemList.Count > id)
            {
                var itemInfo = mItemList[id];
                itemInfo.Show(false, true, () => {
                    itemInfo.transform.SetParent(_trItemPool);
                });

                if (mItemList.Remove(itemInfo))
                {
                    mItemPool.Enqueue(itemInfo);
                    RefreshItemID();
                }
            }
        }

        void RemoveAllScrollItem()
        {
            foreach (var itemInfo in mItemList)
            {
                if (itemInfo != null)
                {
                    itemInfo.Show(false, true, () => {
                        itemInfo.transform.SetParent(_trItemPool);
                    });
                    mItemPool.Enqueue(itemInfo);
                }
            }
            mItemList.Clear();
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
