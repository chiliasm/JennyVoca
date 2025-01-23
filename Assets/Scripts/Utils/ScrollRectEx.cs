using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class ScrollRectEx : ScrollRect
    {
        #region // [Var] Unity //
        [Header("== Ex Info ==")]
        [SerializeField]
        GameObject _itemObject;
        #endregion

        #region // [Var] Scroll Data //
        readonly List<ItemUI> mItemList = new();
        readonly Queue<ItemUI> mPoolList = new();
        #endregion


        #region // [Func] Scroll //
        ItemUI GetOrNewItemUI()
        {
            if (_itemObject == null)
                return null;

            ItemUI info = null;
            if (mPoolList.Count > 0)
                info = mPoolList.Dequeue();
            else
            {
                var o = GameObject.Instantiate(_itemObject);
                o.SetActive(false);
                if (o.TryGetComponent<ItemUI>(out var comp))
                    info = comp;
            }
            return info;
        }

        public List<ItemUI> GetItemUIList()
        {
            return mItemList;
        }

        public ItemUI GetItemUI(int index)
        {
            if (mItemList.Count <= index)
                return null;
            return mItemList[index];
        }

        void UpdateItemUIID()
        {
            for (int i = 0; i < mItemList.Count; i++)
                mItemList[i].ID = i;
        }

        public void AddItemUI(bool isImmediate = false, System.Action<ItemUI> lpSetDataCallback = null)
        {
            var itemUI = GetOrNewItemUI();
            if (itemUI != null)
            {
                lpSetDataCallback?.Invoke(itemUI);
                itemUI.Show(true, isImmediate, () => {
                });
                mItemList.Add(itemUI);

                UpdateItemUIID();
            }
        }

        public void RemoveItemUI(int id, bool isImmediate = false)
        {
            if (mItemList.Count > id)
            {
                var itemInfo = mItemList[id];
                itemInfo.Show(false, isImmediate, () => {
                });

                if (mItemList.Remove(itemInfo))
                {
                    mPoolList.Enqueue(itemInfo);

                    UpdateItemUIID();
                }
            }
        }

        public void RemoveAllItemUI(bool isImmediate = true)
        {
            foreach (var itemInfo in mItemList)
            {
                if (itemInfo != null)
                {
                    itemInfo.Show(false, isImmediate, () => {
                    });
                    mPoolList.Enqueue(itemInfo);
                }
            }
            mItemList.Clear();
        }
        #endregion
    }
}
