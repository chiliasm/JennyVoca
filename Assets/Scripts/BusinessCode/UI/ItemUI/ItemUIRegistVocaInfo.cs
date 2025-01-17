using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class ItemUIRegistVocaInfo : BaseScrollItem
    {
        #region // [Var] Data //
        string mName;
        #endregion


        #region // [Func] Set //
        public void SetData(string name)
        {
            mName = name;
        }
        #endregion
    }
}
