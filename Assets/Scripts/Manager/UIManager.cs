using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class UIManager : BaseMonoSingleton<UIManager>
    {
        #region // [Var] Data //
        const string MAIN_UI_PREFAB_PATH = "Prefab/MainUI";
        const string SUB_UI_PREFAB_PATH = "Prefab/SubUI";
        const string MSG_UI_PREFAB_PATH = "Prefab/MsgUI";

        readonly Dictionary<E_MainUI, MainUI> mMainUIDic = new();
        readonly Dictionary<E_SubUI, SubUI> mSubUIDic = new();
        readonly Dictionary<E_MsgUI, MsgUI> mMsgUIDic = new();

        UIController mUIController;
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }
        #endregion

        #region // [Func] Init //
        public override void OnCreateSingleton()
        {
            base.OnCreateSingleton();
        }

        public void InitUIController(UIController controller)
        {
            mUIController = controller;
        }

        public override void Clear()
        {
            base.Clear();

            foreach (var it in mMainUIDic)
                Destroy(it.Value.gameObject);
            mMainUIDic.Clear();

            foreach (var it in mSubUIDic)
                Destroy(it.Value.gameObject);
            mSubUIDic.Clear();

            foreach (var it in mMsgUIDic)
                Destroy(it.Value.gameObject);
            mMsgUIDic.Clear();
        }
        #endregion

        #region // [Func] Load //
        public MainUI GetOrNewUI(E_MainUI type)
        {
            if (mMainUIDic.TryGetValue(type, out var mainUI) == false)
            {
                var path = string.Format("{0}/{1}", MAIN_UI_PREFAB_PATH, type.ToString());
                var o = ResourceManager.Instance.Load(path);
                if (o != null)
                {
                    var go = GameObject.Instantiate(o) as GameObject;
                    if (go != null && go.TryGetComponent<MainUI>(out var comp))
                    {
                        mainUI = comp;
                        mMainUIDic[type] = mainUI;
                    }                    
                }
            }
            return mainUI;
        }

        public SubUI GetOrNewUI(E_SubUI type)
        {
            if (mSubUIDic.TryGetValue(type, out var subUI) == false)
            {
                var path = string.Format("{0}/{1}", SUB_UI_PREFAB_PATH, type.ToString());
                var o = ResourceManager.Instance.Load(path);
                if (o != null)
                {
                    var go = GameObject.Instantiate(o) as GameObject;
                    if (go != null && go.TryGetComponent<SubUI>(out var comp))
                    {
                        subUI = comp;
                        mSubUIDic[type] = subUI;
                    }   
                }
            }
            return subUI;
        }

        public MsgUI GetOrNewUI(E_MsgUI type)
        {
            if (mMsgUIDic.TryGetValue(type, out var msgUI) == false)
            {
                var path = string.Format("{0}/{1}", MSG_UI_PREFAB_PATH, type.ToString());
                var o = ResourceManager.Instance.Load(path);
                if (o != null)
                {
                    var go = GameObject.Instantiate(o) as GameObject;
                    if (go != null && go.TryGetComponent<MsgUI>(out var comp))
                    {
                        msgUI = comp;
                        mMsgUIDic[type] = msgUI;
                    }
                }
            }
            return msgUI;
        }
        #endregion

        #region // [Func] OpenUI //
        public void OpenUI(E_MainUI type)
        {
            if (mUIController != null)
                mUIController.OpenUI(type);
        }

        public void OpenUI(E_SubUI type)
        {
            if (mUIController != null)
                mUIController.OpenUI(type);
        }

        public void OpenUI(E_MsgUI type)
        {
            if (mUIController != null)
                mUIController.OpenUI(type);
        }
        #endregion

        #region // [Func] CloseUI //
        public void CloseUI(E_MainUI type)
        {
            if (mUIController != null)
                mUIController.CloseUI(type);
        }

        public void CloseUI(E_SubUI type)
        {
            if (mUIController != null)
                mUIController.CloseUI(type);
        }

        public void CloseUI(E_MsgUI type)
        {
            if (mUIController != null)
                mUIController.CloseUI(type);
        }
        #endregion
    }
}
