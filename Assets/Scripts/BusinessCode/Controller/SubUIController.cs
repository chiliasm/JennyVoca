using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class SubUIController : BaseMonoBehaviour
    {
        #region // [Var] Data //
        readonly List<E_SubUI> mOrderList = new();
        readonly Dictionary<E_SubUI, SubUI> mDataDic = new();
        #endregion


        #region // [Func] Show //
        public SubUI OpenUI(E_SubUI type)
        {
            if (mDataDic.TryGetValue(type, out var subUI) == false)
            {
                subUI = UIManager.Instance.GetOrNewUI(type);
                mDataDic[type] = subUI;
            }

            SetLastOrderUI(type);
            if (subUI != null)
            {
                subUI.transform.SetParent(Tr);
                subUI.transform.SetAsLastSibling();
                subUI.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                subUI.SetType(type);
                subUI.Show(true);
            }
            return subUI;
        }

        public void CloseUI(E_SubUI type = E_SubUI.Last)
        {
            if (type == E_SubUI.Last)
                type = GetLastOrderUI();
            if (type != E_SubUI.None)
            {
                if (mDataDic.TryGetValue(type, out var subUI))
                {
                    subUI.Hide(true);
                    mOrderList.Remove(type);
                }
            }
        }

        public void CloseAllUI()
        {
            foreach (var it in mOrderList)
            {
                if (mDataDic.TryGetValue(it, out var subUI))
                    subUI.Hide(true);
            }
            mOrderList.Clear();
        }
        #endregion

        #region // [Func] OrderUI //
        E_SubUI GetLastOrderUI()
        {
            var type = E_SubUI.None;
            if (mOrderList.Count > 0)
                type = mOrderList[^1];
            return type;
        }

        void SetLastOrderUI(E_SubUI type)
        {
            mOrderList.Remove(type);
            mOrderList.Add(type);
        }

        bool IsExistOrderUI(E_SubUI type)
        {
            return mOrderList.Contains(type);
        }
        #endregion
    }
}
