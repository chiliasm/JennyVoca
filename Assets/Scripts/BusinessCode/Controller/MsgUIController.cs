using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class MsgUIController : BaseMonoBehaviour
    {
        #region // [Var] Data //
        readonly List<E_MsgUI> mOrderList = new();
        readonly Dictionary<E_MsgUI, MsgUI> mDataDic = new();
        #endregion


        #region // [Func] Show //
        public MsgUI OpenUI(E_MsgUI type)
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

        public void CloseUI(E_MsgUI type = E_MsgUI.Last)
        {
            if (type == E_MsgUI.Last)
                type = GetLastOrderUI();
            if (type != E_MsgUI.None)
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
        E_MsgUI GetLastOrderUI()
        {
            var type = E_MsgUI.None;
            if (mOrderList.Count > 0)
                type = mOrderList[^1];
            return type;
        }

        void SetLastOrderUI(E_MsgUI type)
        {
            mOrderList.Remove(type);
            mOrderList.Add(type);
        }

        bool IsExistOrderUI(E_MsgUI type)
        {
            return mOrderList.Contains(type);
        }
        #endregion
    }
}
