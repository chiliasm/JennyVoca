using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class ItemUIExamInfo : BaseScrollItem
    {
        #region // [Var] Unity //
        [Header("== ItemExamInfo ==")]
        [SerializeField]
        TMP_Text _textKr;
        [SerializeField]
        TMP_Text _textEn;
        [SerializeField]
        GameObject _goResult;
        [SerializeField]
        GameObject _goOK;
        [SerializeField]
        GameObject _goCancel;
        #endregion

        #region // [Var] Data //
        MainUI_Lobby_Exam.ExamInfo mInfo;
        #endregion


        #region // [Func] Set //
        public void SetData(MainUI_Lobby_Exam.ExamInfo info)
        {
            mInfo = info;

            UpdateUI();
        }
        #endregion

        #region // [Func] UpdateUI //
        void UpdateUI()
        {
            _textKr.text = string.Format("({0})", mInfo.Kr);
            _textEn.text = mInfo.En;

            _goResult.SetActive(mInfo.IsResult);
            _goOK.SetActive(mInfo.IsOK);
            _goCancel.SetActive(!mInfo.IsOK);
        }
        #endregion
    }
}
