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

            if (mDataDic.TryGetValue(type, out var mainUI) == false)
            {
                mainUI = UIManager.Instance.GetOrNewUI(type);
                mDataDic[type] = mainUI;
            }

            SetLastOrderUI(type);
            if (mainUI != null)
            {
                mainUI.transform.SetParent(Tr);
                mainUI.transform.SetAsLastSibling();
                mainUI.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                mainUI.SetType(type);
                mainUI.Show(true);
            }
            return mainUI;
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

            var lastMainUI = GetLastMainUI();
            if (lastMainUI != null)
                lastMainUI.Show(true);
        }

        public void CloseAllUI()
        {
            foreach (var it in mOrderList)
            {
                var mainUI = GetMainUI(it);
                if (mainUI != null)
                    mainUI.Hide(true);
            }
            mOrderList.Clear();
        }
        #endregion

        #region // [Func] MainUI //
        MainUI GetMainUI(E_MainUI type)
        {
            if (mDataDic.TryGetValue(type, out var mainUI) == false)
                return null;
            return mainUI;
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
