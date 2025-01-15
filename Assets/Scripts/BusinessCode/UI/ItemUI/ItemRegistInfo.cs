using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class ItemRegistInfo : BaseMonoBehaviour
    {
        #region // [Var] Unity //
        [Header("== ItemRegistInfo ==")]
        [SerializeField]
        CanvasGroup _canvas;
        [SerializeField]
        Button _btnBg;
        [SerializeField]
        Image _imgBg;
        [SerializeField]
        TMP_Text _textEn;
        [SerializeField]
        TMP_Text _textKr;
        [SerializeField]
        GameObject _goBtnGroup;
        [SerializeField]
        Button _btnModify;
        [SerializeField]
        Button _btnDelete;
        #endregion

        #region // [Var] Data //
        int mID;
        public int ID { get { return mID; } }
        MainUI_Lobby_Regist.RegistInfo mInfo;
        bool mIsSelected = false;

        System.Action<int> mSelectCallback;
        System.Action<int> mModifyCallback;
        System.Action<int> mDeleteCallback;

        readonly Color COLOR_BG_DEFAULT = new(0.96f, 0.85f, 0.59f, 1f);
        readonly Color COLOR_BG_SELECT = new(0.85f, 0.85f, 0.85f, 1f);
        const float FADE_UPDATE_TIME = 0.5f;

        Coroutine mCorFade;
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _btnBg.onClick.AddListener(OnClickBgButton);
            _btnModify.onClick.AddListener(OnClickModifyButton);
            _btnDelete.onClick.AddListener(OnClickDeleteButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _btnBg.onClick.RemoveListener(OnClickBgButton);
            _btnModify.onClick.RemoveListener(OnClickModifyButton);
            _btnDelete.onClick.RemoveListener(OnClickDeleteButton);

            ClearCoroutine(ref mCorFade);
        }
        #endregion

        #region // [Func] Set //
        public void SetData(int id, MainUI_Lobby_Regist.RegistInfo info, System.Action<int> lpSelectCallback = null, System.Action<int> lpModifyCallback = null, System.Action<int> lpDeleteCallback = null)
        {
            mID = id;
            mInfo = info;
            mSelectCallback = lpSelectCallback;
            mModifyCallback = lpModifyCallback; 
            mDeleteCallback = lpDeleteCallback;
            mIsSelected = false;

            UpdateUI();
        }

        public void SetSelect(bool isSelect)
        {
            mIsSelected = isSelect;

            UpdateUISelect();
        }
        #endregion

        #region // [Func] Show, Hide //
        public void Show(bool isShow, bool isImmediate = true)
        {
            if (isImmediate)
                Go.SetActive(isShow);
            else
            {
                if (isShow)
                {
                    Go.SetActive(true);
                    if (ClearCoroutine(ref mCorFade))
                        mCorFade = StartCoroutine(CorFade(0f, 1f, () => {
                        }));
                }
                else
                {
                    if (ClearCoroutine(ref mCorFade))
                        mCorFade = StartCoroutine(CorFade(1f, 0f, () => {
                            Go.SetActive(false);
                        }));
                }
            }
        }

        IEnumerator CorFade(float from, float to, System.Action lpCompleteCallback = null)
        {
            _canvas.alpha = from;

            var rate = 0f;
            var beginTime = Time.realtimeSinceStartup;
            while(rate <= 1f)
            {
                yield return null;

                rate = Mathf.Clamp01((Time.realtimeSinceStartup - beginTime) / FADE_UPDATE_TIME);
                _canvas.alpha = rate;
            }

            lpCompleteCallback?.Invoke();
            mCorFade = null;
        }
        #endregion

        #region // [Func] UpdateUI //
        void UpdateUI()
        {
            _textEn.text = mInfo.En;
            _textKr.text = string.Format("({0})", mInfo.Kr);

            UpdateUISelect();
        }

        void UpdateUISelect()
        {
            _goBtnGroup.SetActive(mIsSelected);
            _imgBg.color = mIsSelected ? COLOR_BG_SELECT : COLOR_BG_DEFAULT;
        }
        #endregion

        #region // [Func] Callback //
        void OnClickBgButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click_Bubble);

            mSelectCallback?.Invoke(mID);
        }

        void OnClickModifyButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click_Bubble);

            mModifyCallback?.Invoke(mID);
        }

        void OnClickDeleteButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click_Bubble);

            mDeleteCallback?.Invoke(mID);
        }
        #endregion
    }
}
