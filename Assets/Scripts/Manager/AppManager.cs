using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class AppManager : BaseMonoSingleton<AppManager>
    {
        #region // [Var] Data //
        const int DEFAULT_APP_WIDTH = 720;
        const int DEFAULT_APP_HEIGHT = 1280;
        const float DEFAULT_APP_RATE = 0.5625f;
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
            Screen.SetResolution(DEFAULT_APP_WIDTH, DEFAULT_APP_HEIGHT, true);
        }
        #endregion

        #region // [Func] Check //
        public void CheckResolutionAdjust(CanvasScaler scaler)
        {
            if (scaler != null)
            {
                float rate = (float)Screen.width / Screen.height;
                scaler.matchWidthOrHeight = (rate < DEFAULT_APP_RATE) ? 0 : 1;
            }
        }
        #endregion
    }
}
