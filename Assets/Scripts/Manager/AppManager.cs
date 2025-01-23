using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class AppManager : BaseMonoSingleton<AppManager>
    {
        #region // [Var] Data //
#if (UNITY_STANDALONE && !UNITY_EDITOR)
        const int DEFAULT_APP_WIDTH = 480;
        const int DEFAULT_APP_HEIGHT = 854;
#else
        const int DEFAULT_APP_WIDTH = 720;
        const int DEFAULT_APP_HEIGHT = 1280;
#endif
        #endregion

        #region // [Func] Singleton //
        public override void OnCreateSingleton()
        {
            base.OnCreateSingleton();

            InitSetting();
        }
        #endregion

        #region // [Func] Init //
        void InitSetting()
        {
            Application.runInBackground = true;
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            var isFullWindow = true;
#if (UNITY_STANDALONE && !UNITY_EDITOR)
            isFullWindow = false;
#endif
            Screen.SetResolution(DEFAULT_APP_WIDTH, DEFAULT_APP_HEIGHT, isFullWindow);
        }
        #endregion

        #region // [Func] Check //
        public void CheckResolutionAdjust(CanvasScaler scaler)
        {
            if (scaler != null)
            {
                float fixRate = (float)DEFAULT_APP_WIDTH / (float)DEFAULT_APP_HEIGHT;

                float rate = (float)Screen.width / Screen.height;
                scaler.matchWidthOrHeight = (rate < fixRate) ? 0 : 1;
            }
        }
        #endregion
    }
}
