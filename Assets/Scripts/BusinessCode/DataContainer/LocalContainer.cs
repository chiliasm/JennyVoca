using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class LocalContainer
    {
        #region // [Var] Data //
        bool mIsSoundOff = false;
        public bool IsSoundOff => mIsSoundOff;
        int mMaxTimeForQuiz = 20;
        public int MaxTimeForQuiz => mMaxTimeForQuiz;

        public System.Action ON_CHANGE_SOUNDOFF;
        public System.Action ON_CHANGE_MAXTIME_FOR_QUIZ;
        #endregion


        #region // [Func] Set //
        public void SetSoundOff(bool isOff)
        {
            if (mIsSoundOff == isOff)
                return;

            mIsSoundOff = isOff;
            ON_CHANGE_SOUNDOFF?.Invoke();
        }

        public void SetMaxTimeForQuiz(int maxTime)
        {
            if (mMaxTimeForQuiz == maxTime)
                return;

            mMaxTimeForQuiz = maxTime;
            ON_CHANGE_MAXTIME_FOR_QUIZ?.Invoke();
        }
        #endregion

        #region // [Func] Load, Save //

        #endregion
    }
}
