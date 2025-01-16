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

        Coroutine mCorUpdateUIItemList;
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
            RemoveAllScrollItem();
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
            RemoveAllScrollItem();

            if (ClearCoroutine(ref mCorUpdateUIItemList))
                mCorUpdateUIItemList = StartCoroutine(CorUpdateUIItemList());
        }

        IEnumerator CorUpdateUIItemList()
        {
            foreach (var it in mDataList)
            {
                AddScrollItem(it);
                yield return new WaitForSeconds(0.1f);
            }
            mCorUpdateUIItemList = null;
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
                o.SetActive(false);
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

        void RefreshItemID()
        {
            for (int i = 0; i < mItemList.Count; i++)
                mItemList[i].ID = i;
        }

        void AddScrollItem(RegistInfo info)
        {
            if (info == null)
                return;
            
            var itemInfo = GetOrNewItem();
            if (itemInfo != null)
            {
                itemInfo.SetData(info, OnSelectedCallback, OnModifyCallback, OnDeleteCallback);
                itemInfo.Show(true, false, () => {
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
                itemInfo.Show(false, false, () => {
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
            if (string.IsNullOrWhiteSpace(en))
            {
                CommonFunc.OpenMsgUI("'영어' 란을 입력해주세요.");
                return;
            }
            if (string.IsNullOrWhiteSpace(kr))
            {
                CommonFunc.OpenMsgUI("'해석' 란을 입력해주세요.");
                return;
            }
                
            foreach (var it in mDataList)
            {
                if (en.Equals(it.En))
                {
                    CommonFunc.OpenMsgUI($"'{en}' 단어는 이미 등록되었습니다.");
                    return;
                }                    
            }

            RegistInfo info = new(en, kr);
            mDataList.Add(info);
            AddScrollItem(info);

            InitUI();
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

                if (mItemList.Count > mModifyID)
                    mItemList[mModifyID].SetData(info);

                InitUI();
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
                UpdateUIButton();
            }
        }

        void OnDeleteCallback(int id = -1)
        {
            if (mDataList.Count <= id || id < 0)
                return;

            mDataList.RemoveAt(id);
            RemoveScrollItem(id);

            InitUI();
        }
        #endregion
    }
}
