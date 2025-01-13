using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class MainUI : BaseUI
    {
        #region // [Var] Data //
        protected E_MainUI mMainUIType = E_MainUI.None;
        public E_MainUI MainUIType { get { return mMainUIType; } }
        #endregion
    }
}
