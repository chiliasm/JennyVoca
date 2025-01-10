using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class UIManager : BaseMonoSingleton<UIManager>
    {
        #region // [Var] Data //
        const string MAIN_UI_PREFAB_PATH = "Prefab/MainUI/";
        const string SUB_UI_PREFAB_PATH = "Prefab/SubUI/";
        const string MSG_UI_PREFAB_PATH = "Prefab/MsgUI/";

        readonly Dictionary<E_MainUI, GameObject> mMainUIDic = new();
        readonly Dictionary<E_SubUI, GameObject> mSubUIDic = new();
        readonly Dictionary<E_MsgUI, GameObject> mMsgUIDic = new();
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }
        #endregion

        #region // [Func] Init //
        public override void OnCreateSingleton()
        {
            base.OnCreateSingleton();
        }
        #endregion


    }
}
