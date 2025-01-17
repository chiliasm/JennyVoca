using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class ItemUIRegistVocaInfo : ItemUI
    {
        #region // [Var] Unity //
        [Header("== RegistVocaInfo ==")]
        [SerializeField]
        TMP_Text _textInfo;
        [SerializeField]
        Button _btnDelete;
        #endregion

        #region // [Var] Data //
        SubUI_RegistVoca.RegistVocaScrollItemData mInfo;
        #endregion


        #region // [Func] Set //
        public void SetData(SubUI_RegistVoca.RegistVocaScrollItemData info)
        {
            mInfo = info;

            UpdateUI();
        }
        #endregion

        #region // [Func] UpdateUI //
        void UpdateUI()
        {

        }
        #endregion
    }
}
