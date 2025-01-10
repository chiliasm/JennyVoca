using UnityEngine.SceneManagement;

namespace Jenny
{
    public class SceneManager : BaseMonoSingleton<SceneManager>
    {
        public enum E_SCENE_TYPE
        {
            None = -1,
            Intro,
            Lobby,
            Voca,
            Max,
        }

        struct SceneInfo
        {
            bool mIsSet;
            E_SCENE_TYPE mType;
            System.Action mCompleteCallback;

            public SceneInfo(E_SCENE_TYPE type, System.Action lpCompleteCallback = null)
            {
                mType = type;
                mCompleteCallback = lpCompleteCallback;
                mIsSet = true;
            }

            public void Clear()
            {
                mType = E_SCENE_TYPE.None;
                mCompleteCallback = null;
                mIsSet = false;
            }

            public bool IsSet => mIsSet;
            public E_SCENE_TYPE Type => mType;
            public void CompleteCallback()
            {
                mCompleteCallback?.Invoke();
            }
        }

        #region // [Var] Data //
        const string EMPTY_SCENE_NAME = "EmptyScene";
        const string INTRO_SCENE_NAME = "IntroScene";
        const string LOBBY_SCENE_NAME = "LobbyScene";
        const string VOCA_SCENE_NAME = "VocaScene";

        E_SCENE_TYPE mCurrentSceneType = E_SCENE_TYPE.None;
        SceneInfo mNewSceneInfo;

        public System.Action LoadedCompleteEmptySceneDelegate = null;
        public System.Action LoadedCompleteNewSceneDelegate = null;
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoadedCallback;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoadedCallback;
        }
        #endregion

        #region // [Func] Init //
        public override void OnCreateSingleton()
        {
            base.OnCreateSingleton();
        }
        #endregion

        #region // [Func] Scene //
        public void ChangeScene(E_SCENE_TYPE newScene, System.Action lpCompleteCallback = null)
        {
            if (mCurrentSceneType == newScene || mNewSceneInfo.IsSet)
                return;

            mNewSceneInfo = new(newScene, lpCompleteCallback);
            LoadScene(EMPTY_SCENE_NAME);
        }

        void LoadScene(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName) == false)
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        }

        string GetSceneName(E_SCENE_TYPE type)
        {
            return type switch
            {
                E_SCENE_TYPE.Intro => INTRO_SCENE_NAME,
                E_SCENE_TYPE.Lobby => LOBBY_SCENE_NAME,
                E_SCENE_TYPE.Voca => VOCA_SCENE_NAME,
                _ => string.Empty,
            };
        }
        #endregion
        
        #region // [Func] Callback //
        void OnSceneLoadedCallback(Scene scene, LoadSceneMode mode)
        {
            if (mNewSceneInfo.IsSet == false)
                return;

            var newSceneName = GetSceneName(mNewSceneInfo.Type);
            if (scene.name.Equals(EMPTY_SCENE_NAME))
            {
                LoadedCompleteEmptySceneDelegate?.Invoke();

                LoadScene(newSceneName);
            }
            else
            {
                if (scene.name.Equals(newSceneName))
                {
                    mCurrentSceneType = mNewSceneInfo.Type;

                    mNewSceneInfo.CompleteCallback();
                    mNewSceneInfo.Clear();

                    LoadedCompleteNewSceneDelegate?.Invoke();
                }                
            }
        }
        #endregion
    }
}
