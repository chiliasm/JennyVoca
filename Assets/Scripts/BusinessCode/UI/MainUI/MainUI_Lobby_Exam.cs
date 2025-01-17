using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class MainUI_Lobby_Exam : MainUI
    {
        public class ExamScrollItemData : BaseScrollItemData
        {
            public string Kr;
            public string En;            
            public bool IsResult;
            public bool IsOK;

            public ExamScrollItemData(string kr, string en, bool isResult, bool isOK)
            {
                Kr = kr;
                En = en;
                IsResult = isResult;
                IsOK = isOK;
            }
        }

        #region // [Var] Unity //
        [Header("== ExamUI ==")]
        [SerializeField]
        TMP_Text _textKr;
        [SerializeField]
        TMP_InputField _inputEn;
        [SerializeField]
        Button _btnConfirm;

        [SerializeField]
        Button _btnClose;

        [SerializeField]
        Button _btnFinish;
        [SerializeField]
        Button _btnStart;
        [SerializeField]
        Slider _sliderTimer;
        [SerializeField]
        TMP_Text _textTimer;

        [SerializeField]
        GameObject _goReady;
        [SerializeField]
        GameObject _goExam;
        #endregion
    }
}
