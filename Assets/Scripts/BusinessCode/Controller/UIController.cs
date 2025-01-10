using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class UIController : BaseMonoBehaviour
    {
        #region // [Var] Unity //
        [SerializeField]
        MainUIController _mainUIController;
        [SerializeField]
        SubUIController _subUIController;
        [SerializeField]
        MsgUIController _msgUIController;
        #endregion


        #region // [Func] MainUI //
        public bool AddMainUI(E_MainUI mainUI)
        {
            return true;
        }
        #endregion

        #region // [Func] SubUI //
        public bool AddSubUI(E_SubUI subUI)
        {
            return true;
        }
        #endregion
    }
}
