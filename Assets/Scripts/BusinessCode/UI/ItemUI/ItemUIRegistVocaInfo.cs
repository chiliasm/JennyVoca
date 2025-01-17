using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class ItemUIRegistVocaInfo : ItemUI
    {
        #region // [Var] Data //
        SubUI_RegistVoca.RegistVocaScrollItemData mInfo;
        #endregion


        #region // [Func] Set //
        public void SetData(SubUI_RegistVoca.RegistVocaScrollItemData info)
        {
            mInfo = info;
        }
        #endregion
    }
}
