using UnityEngine;

namespace Jenny
{
    public class BaseScene : BaseMonoBehaviour
    {
        enum E_SCENE_TYPE
        {
            Intro,
            Lobby,
            Voca,
        }

        #region // [Var] Unity //
        [SerializeField]
        E_SCENE_TYPE mSceneType = E_SCENE_TYPE.Intro;

        [Header("== BaseScene ==")]
        [SerializeField]
        protected Camera _camera;
        [SerializeField]
        protected UIController _uiController;
        #endregion


        #region // [Func] Unity //
        protected override void Start()
        {
            base.Start();

            InitCamera();

            InitMainUI();
        }
        #endregion

        #region // [Func] Camera //
        void InitCamera()
        {
            _camera.backgroundColor = Color.white;
        }
        #endregion

        #region // [Func] MainUI //
        void InitMainUI()
        {
            UIManager.Instance.InitUIController(_uiController);

            switch (mSceneType)
            {
                case E_SCENE_TYPE.Intro:
                    UIManager.Instance.OpenUI(E_MainUI.MainUI_Intro_Base);
                    break;
                case E_SCENE_TYPE.Lobby:
                    UIManager.Instance.OpenUI(E_MainUI.MainUI_Lobby_Base);
                    break;
                case E_SCENE_TYPE.Voca:
                    break;
                default:
                    return;
            }
        }
        #endregion
    }
}
