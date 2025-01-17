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
        MainUI_Lobby_Exam.ExamScrollItemData mData;
        #endregion


        #region // [Func] Set //
        public void SetData(MainUI_Lobby_Exam.ExamScrollItemData data)
        {
            mData = data;

            UpdateUI();
        }
        #endregion

        #region // [Func] UpdateUI //
        void UpdateUI()
        {
            _textKr.text = string.Format("({0})", mData.Kr);
            _textEn.text = mData.En;

            _goResult.SetActive(mData.IsResult);
            _goOK.SetActive(mData.IsOK);
            _goCancel.SetActive(!mData.IsOK);
        }
        #endregion
    }
}
