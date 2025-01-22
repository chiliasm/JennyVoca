using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class BaseUI : BaseMonoBehaviour
    {
        #region // [Var] Unity //
        [Header("== BaseUI ==")]
        [SerializeField]
        CanvasGroup _cg;
        [SerializeField]
        protected Button _btnTouchBg;
        [SerializeField]
        GameObject _scrollItemObject;
        #endregion

        #region // [Var] Scroll Data //
        readonly List<ItemUI> mScrollItemList = new();
        readonly Queue<ItemUI> mScrollItemPool = new();
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            RemoveAllScrollItem();
        }
        #endregion

        #region // [Func] Show //
        virtual public void Show(bool isImmediate = false, System.Action lpCompleteCallback = null)
        {
            Go.SetActive(true);

            lpCompleteCallback?.Invoke();
        }

        virtual public void Hide(bool isImmediate = false, System.Action lpCompleteCallback = null)
        {
            Go.SetActive(false);

            lpCompleteCallback?.Invoke();
        }

        virtual public void CloseUI()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Close_UI);
        }
        #endregion

        #region // [Func] Scroll //
        protected ItemUI GetOrNewItem()
        {
            if (_scrollItemObject == null)
                return null;

            ItemUI info = null;
            if (mScrollItemPool.Count > 0)
                info = mScrollItemPool.Dequeue();
            else
            {
                var o = GameObject.Instantiate(_scrollItemObject);
                o.SetActive(false);
                if (o.TryGetComponent<ItemUI>(out var comp))
                    info = comp;
            }
            return info;
        }

        protected List<ItemUI> GetScrollItemList()
        {
            return mScrollItemList;
        }

        protected ItemUI GetScrollItemUI(int index)
        {
            if (mScrollItemList.Count <= index)
                return null;
            return mScrollItemList[index];
        }

        void UpdateScrollItemID()
        {
            for (int i = 0; i < mScrollItemList.Count; i++)
                mScrollItemList[i].ID = i;
        }

        protected void AddScrollItem(bool isImmediate = false, System.Action<ItemUI> lpSetDataCallback = null)
        {
            var itemUI = GetOrNewItem();
            if (itemUI != null)
            {
                lpSetDataCallback?.Invoke(itemUI);
                itemUI.Show(true, isImmediate, () => {
                });
                mScrollItemList.Add(itemUI);

                UpdateScrollItemID();
            }
        }

        protected void RemoveScrollItem(int id, bool isImmediate = false)
        {
            if (mScrollItemList.Count > id)
            {
                var itemInfo = mScrollItemList[id];
                itemInfo.Show(false, isImmediate, () => {
                });

                if (mScrollItemList.Remove(itemInfo))
                {
                    mScrollItemPool.Enqueue(itemInfo);

                    UpdateScrollItemID();
                }
            }
        }

        protected void RemoveAllScrollItem(bool isImmediate = true)
        {
            foreach (var itemInfo in mScrollItemList)
            {
                if (itemInfo != null)
                {
                    itemInfo.Show(false, isImmediate, () => {
                    });
                    mScrollItemPool.Enqueue(itemInfo);
                }
            }
            mScrollItemList.Clear();
        }
        #endregion
    }
}
