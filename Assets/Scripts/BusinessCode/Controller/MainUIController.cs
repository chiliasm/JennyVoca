using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class MainUIController : BaseMonoBehaviour
    {
        #region // [Var] Data //
        readonly List<E_MainUI> mOrderList = new();
        readonly Dictionary<E_MainUI, MainUI> mDataDic = new();
        #endregion


        #region // [Func] Show //
        public MainUI OpenUI(E_MainUI type)
        {
            var lastMainUI = GetLastMainUI();
            if (lastMainUI != null)
                lastMainUI.Hide(true);

            if (mDataDic.TryGetValue(type, out var comp) == false)
            {
                comp = UIManager.Instance.GetOrNewUI(type);
                mDataDic[type] = comp;
            }

            SetLastOrderUI(type);
            if (comp != null)
            {
                comp.transform.SetParent(Tr);
                comp.transform.SetAsLastSibling();
                comp.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                comp.SetType(type);
                comp.Show(true);
            }
            return comp;
        }

        public void CloseUI(E_MainUI type = E_MainUI.Last)
        {
            if (type == E_MainUI.Last)
                type = GetLastOrderUI();
            if (type != E_MainUI.None)
            {
                var mainUI = GetMainUI(type);
                if (mainUI != null)
                {
                    mainUI.Hide(true);
                    mOrderList.Remove(type);
                }   
            }

            var lastComp = GetLastMainUI();
            if (lastComp != null)
                lastComp.Show(true);
        }

        public void CloseAllUI()
        {
            foreach (var it in mOrderList)
            {
                var comp = GetMainUI(it);
                if (comp != null)
                    comp.Hide(true);
            }
            mOrderList.Clear();
        }
        #endregion

        #region // [Func] MainUI //
        MainUI GetMainUI(E_MainUI type)
        {
            if (mDataDic.TryGetValue(type, out var comp))
                return comp;
            return null;
        }

        MainUI GetLastMainUI()
        {
            return GetMainUI(GetLastOrderUI());
        }
        #endregion

        #region // [Func] OrderUI //
        public E_MainUI GetLastOrderUI()
        {
            var type = E_MainUI.None;
            if (mOrderList.Count > 0)
                type = mOrderList[^1];
            return type;
        }

        void SetLastOrderUI(E_MainUI type)
        {
            mOrderList.Remove(type);
            mOrderList.Add(type);
        }

        bool IsExistOrderUI(E_MainUI type)
        {
            return mOrderList.Contains(type);
        }
        #endregion
    }
}
