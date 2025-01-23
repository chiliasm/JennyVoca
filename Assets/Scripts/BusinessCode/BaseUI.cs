using System.Collections;
using System.Collections.Generic;
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
        protected Button _btnTouchBg;
        #endregion


        #region // [Func] Unity //
        protected override void Start()
        {
            base.Start();

            Rt.anchorMin = Vector2.zero;
            Rt.anchorMax = Vector2.one;
            Rt.sizeDelta = Vector2.zero;
            Rt.localScale = Vector3.one;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
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

        virtual public void CloseUI()
        {
        }
        #endregion
    }
}
