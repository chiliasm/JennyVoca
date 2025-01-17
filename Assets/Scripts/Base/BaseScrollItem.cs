using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class BaseScrollItem : BaseMonoBehaviour
    {
        #region // [Var] Unity //
        [Header("== BaseItemInfo ==")]
        [SerializeField]
        protected CanvasGroup _canvas;
        #endregion

        #region // [Var] Data //
        protected int mID;
        public int ID { get { return mID; } set { mID = value; } }

        const float FADE_UPDATE_TIME = 0.5f;

        Coroutine mCorFade;
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _canvas.alpha = 1f;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            ClearCoroutine(ref mCorFade);
        }
        #endregion

        #region // [Func] Show //
        public void Show(bool isShow, bool isImmediate = false, System.Action lpCompleteCallback = null)
        {
            if (isImmediate)
            {
                Go.SetActive(isShow);
                lpCompleteCallback?.Invoke();
                return;
            }

            if (isShow)
            {
                Go.SetActive(true);
                Fade(0f, 1f, () => {
                    lpCompleteCallback?.Invoke();
                });
            }
            else
            {
                Fade(1f, 0f, () => {
                    Go.SetActive(false);
                    lpCompleteCallback?.Invoke();
                });
            }
        }
        #endregion

        #region // [Func] Fade //
        void Fade(float from, float to, System.Action lpCompleteCallback = null)
        {
            if (ClearCoroutine(ref mCorFade))
            {
                mCorFade = StartCoroutine(CorFade(from, to, () => {
                    lpCompleteCallback?.Invoke();
                }));
            }   
        }

        IEnumerator CorFade(float from, float to, System.Action lpCompleteCallback = null)
        {
            var rate = 0f;
            var beginTime = Time.realtimeSinceStartup;
            while (rate < 1f)
            {
                rate = Mathf.Clamp01((Time.realtimeSinceStartup - beginTime) / FADE_UPDATE_TIME);
                _canvas.alpha = Mathf.Lerp(from, to, rate);
                yield return null;
            }
            _canvas.alpha = to;
            lpCompleteCallback?.Invoke();

            mCorFade = null;
        }
        #endregion
    }
}
