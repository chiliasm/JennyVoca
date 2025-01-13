using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class MsgUI : BaseUI
    {
        #region // [Var] Data //
        protected E_MsgUI mMsgUIType = E_MsgUI.None;
        public E_MsgUI MsgUIType { get { return mMsgUIType; } }
        #endregion
    }
}
