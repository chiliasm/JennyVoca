using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class ItemUIExamInfo : ItemUI
    {
        #region // [Var] Unity //
        [Header("== ExamInfo ==")]
        [SerializeField]
        TMP_Text _textQuiz;
        [SerializeField]
        TMP_Text _textEn;
        [SerializeField]
        GameObject _goResult;
        [SerializeField]
        GameObject _goPass;
        [SerializeField]
        GameObject _goFail;
        #endregion

        #region // [Var] Data //
        MainUI_Lobby_Exam.ExamScrollItemData mData;

        bool mIsShowResult = false;
        #endregion


        #region // [Func] Set //
        public void SetData(MainUI_Lobby_Exam.ExamScrollItemData data)
        {
            mData = data;
            mIsShowResult = false;

            UpdateUI();
        }
        #endregion

        #region // [Func] Get //
        public bool IsPass()
        {
            return mData.IsPass;
        }
        #endregion

        #region // [Func] Show //
        public void ShowResult(bool isShow)
        {
            mIsShowResult = isShow;

            UpdateUI();
        }
        #endregion

        #region // [Func] UpdateUI //
        void UpdateUI()
        {
            _textQuiz.text = string.Format("{0} > {1}", mData.Kr, mData.Input);
            _textEn.text = mData.En;

            _goResult.SetActive(mIsShowResult);
            _goPass.SetActive(mData.IsPass);
            _goFail.SetActive(!mData.IsPass);
            _textEn.gameObject.SetActive(mIsShowResult);
        }
        #endregion        
    }
}
