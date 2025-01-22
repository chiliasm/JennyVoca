using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class LocalDataManager : BaseMonoSingleton<LocalDataManager>
    {
        enum E_LOCAL_KEY
        {
            KEY_SOUND_ON,
            KEY_TIME_OF_QUIZ,
        }

        class LocalData
        {
            public bool SoundOn;
            public int TimeOfQuiz;

            public LocalData()
            {
                SoundOn = true;
                TimeOfQuiz = 20;
            }
        }

        #region // [Var] Data //
        readonly LocalData mLocalData = new();

        System.Action OnChangeLocalData_SoundOn;
        System.Action OnChangeLocalData_TimeOfQuiz;
        #endregion


        #region // [Func] Create //
        public override void OnCreateSingleton()
        {
            base.OnCreateSingleton();

            Load();
        }
        #endregion

        #region // [Func] Load //
        void Load()
        {
            string key;

            key = E_LOCAL_KEY.KEY_SOUND_ON.ToString();
            if (PlayerPrefs.HasKey(key))
                mLocalData.SoundOn = (PlayerPrefs.GetInt(key) == 1);
            else
                PlayerPrefs.SetInt(key, mLocalData.SoundOn ? 1 : 0);
                

            key = E_LOCAL_KEY.KEY_TIME_OF_QUIZ.ToString();
            if (PlayerPrefs.HasKey(key) == false)
                mLocalData.TimeOfQuiz = PlayerPrefs.GetInt(key);
            else
                PlayerPrefs.SetInt(key, mLocalData.TimeOfQuiz);
        }
        #endregion

        #region // [Func] Set //
        public bool IsSoundOn()
        {
            return mLocalData.SoundOn;
        }

        public void SetSoundOn(bool isOn)
        {
            if (mLocalData.SoundOn == isOn)
                return;

            mLocalData.SoundOn = isOn;
            PlayerPrefs.SetInt(E_LOCAL_KEY.KEY_SOUND_ON.ToString(), isOn ? 1 : 0);

            OnChangeLocalData_SoundOn?.Invoke();
        }

        public int GetTimeOfQuiz()
        {
            return mLocalData.TimeOfQuiz;
        }

        public void SetTimeOfQuiz(int time)
        {
            if (mLocalData.TimeOfQuiz == time)
                return;

            mLocalData.TimeOfQuiz = time;
            PlayerPrefs.SetInt(E_LOCAL_KEY.KEY_TIME_OF_QUIZ.ToString(), time);

            OnChangeLocalData_TimeOfQuiz?.Invoke();
        }
        #endregion
    }
}
