using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class ToastUIController : BaseMonoBehaviour
    {
        #region // [Var] Data //
        readonly List<E_ToastUI> mOrderList = new();
        readonly Dictionary<E_ToastUI, ToastUI> mDataDic = new();
        #endregion


        #region // [Func] Show //
        public ToastUI OpenUI(E_ToastUI type)
        {
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

        public void CloseUI(E_ToastUI type = E_ToastUI.Last)
        {
            if (type == E_ToastUI.Last)
                type = GetLastOrderUI();
            if (type != E_ToastUI.None)
            {
                if (mDataDic.TryGetValue(type, out var comp))
                {
                    comp.Hide(true);
                    mOrderList.Remove(type);
                }
            }
        }

        public void CloseAllUI()
        {
            foreach (var it in mOrderList)
            {
                if (mDataDic.TryGetValue(it, out var comp))
                    comp.Hide(true);
            }
            mOrderList.Clear();
        }
        #endregion

        #region // [Func] OrderUI //
        E_ToastUI GetLastOrderUI()
        {
            var type = E_ToastUI.None;
            if (mOrderList.Count > 0)
                type = mOrderList[^1];
            return type;
        }

        void SetLastOrderUI(E_ToastUI type)
        {
            mOrderList.Remove(type);
            mOrderList.Add(type);
        }

        bool IsExistOrderUI(E_ToastUI type)
        {
            return mOrderList.Contains(type);
        }
        #endregion
    }
}
