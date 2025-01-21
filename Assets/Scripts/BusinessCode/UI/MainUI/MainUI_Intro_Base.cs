using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class MainUI_Intro_Base : MainUI
    {
        #region // [Var] Unity //
        [Header("== MainUI ==")]
        [SerializeField]
        Button _btnQuit;
        [SerializeField]
        Button _btnStart;
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _btnQuit.onClick.AddListener(OnClickQuitButton);
            _btnStart.onClick.AddListener(OnClickStartButton);
        }
        protected override void OnDisable()
        {
            base.OnDisable();

            _btnQuit.onClick.RemoveListener(OnClickQuitButton);
            _btnStart.onClick.RemoveListener(OnClickStartButton);
        }
        #endregion

        #region // [Func] Init //
        protected override void InitUI()
        {
            base.InitUI();
        }
        #endregion

        #region // [Func] Callback //
        void OnClickQuitButton()
        {
            CommonFunc.PlayClickSound();

            CommonFunc.QuitApp();
        }

        void OnClickStartButton()
        {
            CommonFunc.PlayClickSound();

            SceneManager.Instance.ChangeScene(SceneManager.E_SCENE_TYPE.Lobby);
        }
        #endregion
    }
}
