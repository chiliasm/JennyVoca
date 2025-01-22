using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class MainUI_Lobby_Base : MainUI
    {
        #region // [Var] Unity //
        [Header("== LobbyBase ==")]
        [SerializeField]
        Button _btnRegist;
        [SerializeField]
        Button _btnExam;
        [SerializeField]
        Button _btnQuit;
        [SerializeField]
        Button _btnSetting;
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _btnRegist.onClick.AddListener(OnClickRegistButton);
            _btnExam.onClick.AddListener(OnClickExamButton);
            _btnQuit.onClick.AddListener(OnClickQuitButton);
            _btnSetting.onClick.AddListener(OnClickSettingButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _btnRegist.onClick.RemoveListener(OnClickRegistButton);
            _btnExam.onClick.RemoveListener(OnClickExamButton);
            _btnQuit.onClick.RemoveListener(OnClickQuitButton);
            _btnSetting.onClick.RemoveListener(OnClickSettingButton);
        }
        #endregion

        #region // [Func] Callback //
        void OnClickRegistButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click);

            var registOrderUI = UIManager.Instance.OpenUI(E_SubUI.SubUI_RegistOrder) as SubUI_RegistOrder;
            if (registOrderUI != null)
                registOrderUI.SetData(CallbackConfirmItem);
        }

        void OnClickExamButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click);

            var subUI = UIManager.Instance.OpenUI(E_SubUI.SubUI_SelectOrder) as SubUI_SelectOrder;
            if (subUI != null)
            {
                subUI.SetData((orderName) => {
                    var mainUI = UIManager.Instance.OpenUI(E_MainUI.MainUI_Lobby_Exam) as MainUI_Lobby_Exam;
                    if (mainUI != null)
                        mainUI.SetData(orderName);
                });
            }
        }

        void OnClickQuitButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click);

            CommonFunc.QuitApp();
        }

        void OnClickSettingButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click);

            UIManager.Instance.OpenUI(E_SubUI.SubUI_AppSetting);
        }

        void CallbackConfirmItem(string orderName)
        {
            var registUI = UIManager.Instance.OpenUI(E_MainUI.MainUI_Lobby_Regist) as MainUI_Lobby_Regist;
            if (registUI != null)
                registUI.SetData(orderName);
        }
        #endregion
    }
}
