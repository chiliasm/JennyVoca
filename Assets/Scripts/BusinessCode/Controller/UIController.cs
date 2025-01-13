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
        public void OpenMainUI(E_MainUI type)
        {
            _mainUIController.OpenUI(type);
        }

        public void CloseMainUI()
        {
            _mainUIController.CloseUI();
        }
        #endregion

        #region // [Func] SubUI //
        public void OpenSubUI(E_SubUI type)
        {
            _subUIController.OpenUI(type);
        }

        public void CloseSubUI()
        {
            _subUIController.CloseUI();
        }
        #endregion

        #region // [Func] MsgUI //
        public void OpenMsgUI(E_MsgUI type)
        {
            _msgUIController.OpenUI(type);
        }

        public void CloseMsgUI()
        {
            _msgUIController.CloseUI();
        }
        #endregion
    }
}
