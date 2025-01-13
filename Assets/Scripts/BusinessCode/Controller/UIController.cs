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
        public void OpenUI(E_MainUI type)
        {
            //  MainUI 타입이 다르면 SubUI는 모두 닫는다.
            var lastType = _mainUIController.GetLastOrderUI();
            if (lastType != E_MainUI.None && lastType != type)
                _subUIController.CloseAllUI();  

            _mainUIController.OpenUI(type);
        }

        public void CloseUI(E_MainUI type)
        {
            _mainUIController.CloseUI(type);
        }
        #endregion

        #region // [Func] SubUI //
        public void OpenUI(E_SubUI type)
        {
            _subUIController.OpenUI(type);
        }

        public void CloseUI(E_SubUI type)
        {
            _subUIController.CloseUI(type);
        }
        #endregion

        #region // [Func] MsgUI //
        public void OpenUI(E_MsgUI type)
        {
            _msgUIController.OpenUI(type);
        }

        public void CloseUI(E_MsgUI type)
        {
            _msgUIController.CloseUI(type);
        }
        #endregion
    }
}
