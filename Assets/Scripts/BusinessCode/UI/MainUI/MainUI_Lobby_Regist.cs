using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class MainUI_Lobby_Regist : MainUI
    {
        public class RegistScrollItemData : BaseScrollItemData
        {
            public string En;
            public string Kr;

            public RegistScrollItemData(string en, string kr)
            {
                En = en;
                Kr = kr;
            }
        }

        #region // [Var] Unity //
        [Header("== LobbyRegist ==")]
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
        string mOrderName;
        readonly List<RegistScrollItemData> mItemList = new();
        int mModifyID = -1;        

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
        }

        protected override void Update()
        {
            base.Update();
        }
        #endregion
        
        #region // [Func] Init //
        void InitUI()
        {
            mModifyID = -1;
            _inputEn.text = string.Empty;
            _inputKr.text = string.Empty;

            var itemList = GetScrollItemList();
            foreach (var it in itemList)
            {
                var itemUI = it as ItemUIRegistInfo;
                if (itemUI != null)
                    itemUI.SetSelect(false);
            }   
        }
        #endregion

        #region // [Func] SetData //
        public void SetData(string orderName)
        {
            mOrderName = orderName;

            mItemList.Clear();
            var list = DataManager.Instance.GetVocaInfoList(orderName);
            if (list != null)
            {
                foreach (var it in list)
                    mItemList.Add(new(it.En, it.Kr));
            }

            UpdateUI();
        }
        #endregion

        #region // [Func] UpdateUI //
        void UpdateUI()
        {
            UpdateUITitle();
            UpdateUIButton();
            UpdateUIItemList();
        }

        void UpdateUITitle()
        {
            _textTitle.text = mOrderName;
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
            foreach (var it in mItemList)
            {
                AddScrollItem(false, (ui) =>
                {
                    var itemUI = ui as ItemUIRegistInfo;
                    if (itemUI != null)
                    {
                        itemUI.transform.SetParent(_scrollList.content);
                        itemUI.transform.SetAsLastSibling();
                        itemUI.SetData(it, OnSelectedCallback, OnModifyCallback, OnDeleteCallback);
                    }   
                });
                yield return new WaitForSeconds(0.1f);
            }
            mCorUpdateUIItemList = null;
        }
        #endregion        

        #region // [Func] Callback //
        void OnClickCloseButton()
        {
            CommonFunc.PlayClickSound();

            CloseUI();
        }

        void OnClickRegistButton()
        {
            CommonFunc.PlayClickSound();

            if (mItemList.Count > 0)
            {
                VocaOrder order = new(mOrderName);
                foreach (var it in mItemList)
                    order.InfoList.Add(new(it.En, it.Kr));

                DataManager.Instance.AddVocaOrder(order);
                DataManager.Instance.SaveVocaData();
            }

            CloseUI();
        }

        void OnClickAddButton()
        {
            CommonFunc.PlayClickSound();

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
                
            foreach (var it in mItemList)
            {
                if (en.Equals(it.En))
                {
                    CommonFunc.OpenMsgUI($"'{en}' 단어는 이미 등록되었습니다.");
                    return;
                }                    
            }

            RegistScrollItemData data = new(en, kr);
            mItemList.Add(data);
            AddScrollItem(false, (ui) => {
                var itemUI = ui as ItemUIRegistInfo;
                if (itemUI != null)
                {
                    itemUI.transform.SetParent(_scrollList.content);
                    itemUI.transform.SetAsLastSibling();
                    itemUI.SetData(data, OnSelectedCallback, OnModifyCallback, OnDeleteCallback);
                }   
            });

            InitUI();
        }

        void OnClickModifyButton()
        {
            CommonFunc.PlayClickSound();

            if (mItemList.Count <= mModifyID || mModifyID < 0 )
                return;

            var info = mItemList[mModifyID];
            if (info != null)
            {
                info.En = _inputEn.text;
                info.Kr = _inputKr.text;

                var itemUI = GetScrollItemUI(mModifyID) as ItemUIRegistInfo;
                if (itemUI != null)
                    itemUI.SetData(info);

                InitUI();
            }
        }

        void OnSelectedCallback(int id = -1)
        {
            var list = GetScrollItemList();
            if (list != null && list.Count > id)
            {
                foreach (var it in list)
                {
                    var itemUI = it as ItemUIRegistInfo;
                    if (itemUI != null)
                        itemUI.SetSelect(itemUI.ID.Equals(id));
                }
            }
        }

        void OnModifyCallback(int id = -1)
        {
            if (mItemList.Count <= id || id < 0)
                return;

            var info = mItemList[id];
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
            if (mItemList.Count <= id || id < 0)
                return;

            mItemList.RemoveAt(id);
            RemoveScrollItem(id);

            InitUI();
        }
        #endregion
    }
}
