using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class MainUI_Lobby_Exam : MainUI
    {
        public class ExamInfo
        {
            public string Kr;
            public string En;            
            public bool IsResult;
            public bool IsOK;

            public ExamInfo(string kr, string en, bool isResult, bool isOK)
            {
                Kr = kr;
                En = en;
                IsResult = isResult;
                IsOK = isOK;
            }
        }
    }
}
