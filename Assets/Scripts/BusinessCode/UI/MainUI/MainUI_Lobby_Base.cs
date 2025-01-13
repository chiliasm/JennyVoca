using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class MainUI_Lobby_Base : MainUI
    {
        #region // [Var] Unity //
        [SerializeField]
        Button _btnRegist;
        [SerializeField]
        Button _btnExam;
        [SerializeField]
        Button _btnQuit;
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _btnRegist.onClick.AddListener(OnClickRegistButton);
            _btnExam.onClick.AddListener(OnClickExamButton);
            _btnQuit.onClick.AddListener(OnClickQuitButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _btnRegist.onClick.RemoveListener(OnClickRegistButton);
            _btnExam.onClick.RemoveListener(OnClickExamButton);
            _btnQuit.onClick.RemoveListener(OnClickQuitButton);
        }
        #endregion

        #region // [Func] Callback //
        void OnClickRegistButton()
        {

        }

        void OnClickExamButton()
        {

        }

        void OnClickQuitButton()
        {
            CommonFunc.QuitApp();
        }
        #endregion
    }
}
