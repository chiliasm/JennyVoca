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
        [SerializeField]
        ToastUIController _toastUIController;
        #endregion


        #region // [Func] OpenUI //
        public MainUI OpenUI(E_MainUI type)
        {
            //  MainUI Ÿ���� �ٸ��� SubUI�� ��� �ݴ´�.
            var lastType = _mainUIController.GetLastOrderUI();
            if (lastType != E_MainUI.None && lastType != type)
                _subUIController.CloseAllUI();  

            return _mainUIController.OpenUI(type);
        }

        public SubUI OpenUI(E_SubUI type)
        {
            return _subUIController.OpenUI(type);
        }

        public MsgUI OpenUI(E_MsgUI type)
        {
            return _msgUIController.OpenUI(type);
        }

        public ToastUI OpenUI(E_ToastUI type)
        {
            return _toastUIController.OpenUI(type);
        }
        #endregion

        #region // [Func] CloseUI //
        public void CloseUI(E_MainUI type)
        {
            _mainUIController.CloseUI(type);
        }
        
        public void CloseUI(E_SubUI type)
        {
            _subUIController.CloseUI(type);
        }
        
        public void CloseUI(E_MsgUI type)
        {
            _msgUIController.CloseUI(type);
        }

        public void CloseUI(E_ToastUI type)
        {
            _toastUIController.CloseUI(type);
        }
        #endregion
    }
}
