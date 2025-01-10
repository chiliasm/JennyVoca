using System.Collections;
using System.Collections.Generic;
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

            InitMainUI();
        }
        #endregion

        #region // [Func] MainUI //
        void InitMainUI()
        {
            switch (mSceneType)
            {
                case E_SCENE_TYPE.Intro:
                    break;
                case E_SCENE_TYPE.Lobby:
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
