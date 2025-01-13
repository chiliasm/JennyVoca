using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class BaseUI : BaseMonoBehaviour
    {
        #region // [Var] Unity //
        [Header("== BaseUI ==")]
        [SerializeField]
        CanvasGroup _cg;
        [SerializeField]
        Button _btnTouchBg;
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _btnTouchBg.onClick.AddListener(OnClickTouchBgButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _btnTouchBg.onClick.RemoveListener(OnClickTouchBgButton);
        }
        #endregion

        #region // [Func] Show //
        virtual public void Show(bool isImmediate = false, System.Action lpCompleteCallback = null)
        {
            Go.SetActive(true);

            lpCompleteCallback?.Invoke();
        }

        virtual public void Hide(bool isImmediate = false, System.Action lpCompleteCallback = null)
        {
            Go.SetActive(false);

            lpCompleteCallback?.Invoke();
        }
        #endregion

        #region // [Func] Callback //
        void OnClickTouchBgButton()
        {
            Hide();
        }
        #endregion
    }
}
