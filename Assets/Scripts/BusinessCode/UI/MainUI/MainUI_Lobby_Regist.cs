using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class MainUI_Lobby_Regist : MainUI
    {
        public class RegistInfo
        {
            public string En;
            public string Kr;

            public RegistInfo(string en, string kr)
            {
                En = en;
                Kr = kr;
            }
        }

        #region // [Var] Unity //
        [Header("== MainUI Regist ==")]
        [SerializeField]
        TMP_Text _textTitle;
        [SerializeField]
        Button _btnClose;

        [SerializeField]
        TMP_InputField _inputEn;
        [SerializeField]
        TMP_InputField _inputKr;
        [SerializeField]
        Button _btnAdd;
        [SerializeField]
        Button _btnModify;

        [SerializeField]
        ScrollRect _scrollList;
        [SerializeField]
        GameObject _itemObject;
        [SerializeField]
        Transform _trItemPool;

        [SerializeField]
        Button _btnRegist;
        #endregion

        #region // [Var] Data //
        readonly List<RegistInfo> mDataList = new();
        int mModifyID = -1;

        readonly List<ItemRegistInfo> mItemList = new();
        readonly Queue<ItemRegistInfo> mItemPool = new();

        Vector2 mScrollPos;
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _btnAdd.onClick.AddListener(OnClickAddButton);
            _btnModify.onClick.AddListener(OnClickModifyButton);
            _btnClose.onClick.AddListener(OnClickCloseButton);
            _btnRegist.onClick.AddListener(OnClickRegistButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _btnAdd.onClick.RemoveListener(OnClickAddButton);
            _btnModify.onClick.RemoveListener(OnClickModifyButton);
            _btnClose.onClick.RemoveListener(OnClickCloseButton);
            _btnRegist.onClick.RemoveListener(OnClickRegistButton);

            Clear();
        }

        protected override void Update()
        {
            base.Update();
        }
        #endregion
        
        #region // [Func] Init //
        protected override void InitUI()
        {
            base.InitUI();

            mModifyID = -1;
            _inputEn.text = string.Empty;
            _inputKr.text = string.Empty;

            foreach (var it in mItemList)
                it.SetSelect(false);
        }

        public override void Clear()
        {
            mDataList.Clear();
            RemoveAllItem();
        }
        #endregion

        #region // [Func] UpdateUI //
        void UpdateUI()
        {
            UpdateUIButton();
            UpdateUIItemList();
        }

        void UpdateUIButton()
        {
            var isAdd = (mModifyID < 0);

            _btnAdd.gameObject.SetActive(isAdd);
            _btnModify.gameObject.SetActive(!isAdd);
        }

        void UpdateUIItemList()
        {
            RemoveAllItem();

            int id = 0;
            foreach (var it in mDataList)
            {
                var item = GetOrNewItem();
                if (item != null)
                {
                    item.SetData(id, it, OnSelectedCallback, OnModifyCallback, OnDeleteCallback);
                    item.Show(true);
                    mItemList.Add(item);

                    id++;
                }
            }
        }
        #endregion

        #region // [Func] Scroll //
        ItemRegistInfo GetOrNewItem()
        {
            ItemRegistInfo info = null;
            if (mItemPool.Count > 0)
                info = mItemPool.Dequeue();
            else
            {
                var o = GameObject.Instantiate(_itemObject);
                if (o.TryGetComponent<ItemRegistInfo>(out var comp))
                    info = comp;
            }
            if (info != null)
            {
                info.transform.SetParent(_scrollList.content);
                info.transform.SetAsLastSibling();
            }
            return info;
        }

        void RemoveItem(ItemRegistInfo info)
        {
            if (info != null)
            {
                info.Show(false, true);
                info.transform.SetParent(_trItemPool);
                mItemPool.Enqueue(info);
            }   
        }

        void RemoveAllItem()
        {
            foreach (var it in mItemList)
                RemoveItem(it);
            mItemList.Clear();
        }
        #endregion

        #region // [Func] Callback //
        void OnClickCloseButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click_Bubble);

            CloseUI();
        }

        void OnClickRegistButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click_Bubble);
        }

        void OnClickAddButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click_Bubble);

            var en = _inputEn.text;
            var kr = _inputKr.text;
            if (string.IsNullOrWhiteSpace(en) || string.IsNullOrWhiteSpace(kr))
                return;
            foreach (var it in mDataList)
            {
                if (en.Equals(it.En))
                    return;
            }

            mDataList.Add(new(en, kr));

            InitUI();
            UpdateUI();
        }

        void OnClickModifyButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click_Bubble);

            if (mDataList.Count <= mModifyID || mModifyID < 0 )
                return;

            var info = mDataList[mModifyID];
            if (info != null)
            {
                info.En = _inputEn.text;
                info.Kr = _inputKr.text;

                InitUI();
                UpdateUI();
            }
        }

        void OnSelectedCallback(int id = -1)
        {
            if (mItemList.Count <= id)
                return;
            
            foreach (var it in mItemList)
                it.SetSelect(it.ID.Equals(id));
        }

        void OnModifyCallback(int id = -1)
        {
            if (mDataList.Count <= id || id < 0)
                return;

            var info = mDataList[id];
            if (info != null)
            {
                _inputEn.text = info.En;
                _inputKr.text = info.Kr;

                mModifyID = id;
                UpdateUI();
            }
        }

        void OnDeleteCallback(int id = -1)
        {
            if (mDataList.Count <= id || id < 0)
                return;

            mDataList.RemoveAt(id);

            InitUI();
            UpdateUI();
        }
        #endregion
    }
}
