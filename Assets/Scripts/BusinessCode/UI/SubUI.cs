using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class SubUI : BaseUI
    {
        #region // [Var] Data //
        protected E_SubUI mSubUIType = E_SubUI.None;
        public E_SubUI SubUIType { get { return mSubUIType; } }
        #endregion
    }
}
