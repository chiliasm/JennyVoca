using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jenny
{
    public class MainUI_Lobby_Exam : MainUI
    {
        enum E_ExamState
        {
            None = -1,

            Ready,
            Testing,
            Marking,
            Retry,

            Max,
        }

        class ExamInfo
        {
            bool mIsStart;
            public bool IsStart => mIsStart;
            int mMax;
            public int Max => mMax;
            int mIndex;
            public int Index => mIndex;
            float mStartTime;
            public float StartTime => mStartTime;
            System.Action mFinishCallback;

            public void Init(int max, System.Action lpFinishCallback)
            {
                mIsStart = false;
                mIndex = -1;
                mMax = max;
                mStartTime = 0f;
                mFinishCallback = lpFinishCallback;
            }

            public void Next()
            {
                mIndex++;
                mIsStart = true;                
                mStartTime = Time.realtimeSinceStartup;

                if (mIndex >= mMax)
                {
                    mIsStart = false;
                    mFinishCallback?.Invoke();
                }
            }
        }

        class QuizInfo
        {
            string mEn;
            public string En => mEn;
            string mKr;
            public string Kr => mKr;
            bool mIsPass;
            public bool IsPass => mIsPass;

            public QuizInfo(string en, string kr)
            {
                mEn = en;
                mKr = kr;
                mIsPass = false;
            }

            public void SetPass(bool isPass)
            {
                mIsPass = isPass;
            }
        }

        public class ExamScrollItemData : BaseScrollItemData
        {
            public string Kr;
            public string En;
            public string Input;
            public bool IsPass;

            public ExamScrollItemData(string en, string kr, string input)
            {
                En = en;
                Kr = kr;
                Input = input;
                IsPass = (En == Input);
            }
        }

        #region // [Var] Unity //
        [Header("== LobbyExam ==")]
        [SerializeField]
        Button _btnClose;

        [SerializeField]
        TMP_Text _textOrder;
        [SerializeField]
        TMP_Text _textKr;
        [SerializeField]
        TMP_InputField _inputEn;
        [SerializeField]
        Button _btnSubmit;

        [SerializeField]
        ScrollRect _scrollList;

        [SerializeField]
        Button _btnStart;
        [SerializeField]
        Button _btnMarking;
        [SerializeField]
        Button _btnRetry;
        [SerializeField]
        Slider _sliderTimer;
        [SerializeField]
        TMP_Text _textTimer;

        [SerializeField]
        GameObject[] _goExamReady;
        [SerializeField]
        GameObject[] _goExamTesting;
        [SerializeField]
        GameObject[] _goExamMarking;
        [SerializeField]
        GameObject[] _goExamRetry;
        #endregion

        #region // [Var] Data //
        const float MAX_TESTING_TIME = 20f;
        const float TEXT_CAUTION_RATIO = 0.8f;

        string mOrderName;
        readonly List<(string, string)> mDataList = new();

        E_ExamState mExamState = E_ExamState.None;
        readonly ExamInfo mExamInfo = new();
        readonly List<QuizInfo> mQuizList = new();
        readonly List<ExamScrollItemData> mItemList = new();

        Coroutine mCorRunMarking;
        #endregion


        #region // [Func] Unity //
        protected override void OnEnable()
        {
            base.OnEnable();

            _btnClose.onClick.AddListener(OnClickCloseButton);
            _btnSubmit.onClick.AddListener(OnClickSubmitButton);
            _btnStart.onClick.AddListener(OnClickStartButton);
            _btnMarking.onClick.AddListener(OnClickMarkingButton);
            _btnRetry.onClick.AddListener(OnClickRetryButton);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            ClearCoroutine(ref mCorRunMarking);

            _btnClose.onClick.RemoveListener(OnClickCloseButton);
            _btnSubmit.onClick.RemoveListener(OnClickSubmitButton);
            _btnStart.onClick.RemoveListener(OnClickStartButton);
            _btnMarking.onClick.RemoveListener(OnClickMarkingButton);
            _btnRetry.onClick.RemoveListener(OnClickRetryButton);
        }

        protected override void Update()
        {
            base.Update();

            UpdateUITimer();
        }
        #endregion

        #region // [Func] Init //
        void InitValue()
        {
            mExamState = E_ExamState.None;
            mOrderName = string.Empty;
            mQuizList.Clear();
        }
        #endregion

        #region // [Func] Set //
        public void SetData(string orderName)
        {
            InitValue();

            mDataList.Clear();
            var order = DataManager.Instance.GetVocaOrder(orderName);
            if (order != null)
            {
                mOrderName = order.OrderName;
                foreach (var it in order.InfoList)
                    mDataList.Add((it.En, it.Kr));
            }
            
            SetExamState(E_ExamState.Ready);
            UpdateUI();
        }

        void InitShuffleQuiz()
        {
            mQuizList.Clear();
            List<(string, string)> list = new(mDataList);
            while (list.Count > 0)
            {
                var randomIdx = 0;
                if (list.Count > 1)
                    randomIdx = Random.Range(0, list.Count);
                var data = list[randomIdx];

                mQuizList.Add(new(data.Item1, data.Item2));
                list.RemoveAt(randomIdx);
            }

            mExamInfo.Init(mQuizList.Count, CallbackQuitNextFinish);
        }

        void SetExamState(E_ExamState state)
        {
            if (mExamState == state)
                return;

            mExamState = state;
            foreach (var it in _goExamReady)
                it.SetActive(false);
            foreach (var it in _goExamTesting)
                it.SetActive(false);
            foreach (var it in _goExamMarking)
                it.SetActive(false);
            foreach (var it in _goExamRetry)
                it.SetActive(false);

            switch (state)
            {
                case E_ExamState.Ready:
                    {
                        foreach (var it in _goExamReady)
                            it.SetActive(true);

                        InitShuffleQuiz();
                    }
                    break;
                case E_ExamState.Testing:
                    {
                        foreach (var it in _goExamTesting)
                            it.SetActive(true);
                    }
                    break;
                case E_ExamState.Marking:
                    {
                        foreach (var it in _goExamMarking)
                            it.SetActive(true);
                    }
                    break;
                case E_ExamState.Retry:
                    {
                        foreach (var it in _goExamRetry)
                            it.SetActive(true);
                    }
                    break;
            }
        }
        #endregion

        #region // [Func] UpdateUI //
        void UpdateUI()
        {
            UpdateUITitle();
            UpdateUIInput();
            UpdateUIScroll();
            UpdateUITimer();
        }

        void UpdateUITitle()
        {
            _textOrder.text = mOrderName;
        }

        void UpdateUIInput()
        {
            var kr = string.Empty;
            if (mExamInfo.IsStart)
                kr = mQuizList[mExamInfo.Index].Kr;
            
            _textKr.text = kr;
            _inputEn.text = string.Empty;
        }

        void UpdateUIScroll()
        {
            mItemList.Clear();
            RemoveAllScrollItem();
        }

        void UpdateUITimer()
        {
            if (_sliderTimer.isActiveAndEnabled == false || _textTimer.isActiveAndEnabled == false)
                return;

            float elapsedTime = 0f;
            float ratio = 0f;
            switch (mExamState)
            {
                case E_ExamState.Testing:
                    {
                        if (mExamInfo.IsStart)
                        {
                            elapsedTime = Time.realtimeSinceStartup - mExamInfo.StartTime;
                            ratio = Mathf.Clamp01(elapsedTime / MAX_TESTING_TIME);
                        }
                    }
                    break;
                default:
                    break;
            }

            _sliderTimer.value = ratio;
            _textTimer.text = ((int)elapsedTime > 0) ? string.Format("{0} s", (int)elapsedTime) : string.Empty;
            _textTimer.color = (ratio >= TEXT_CAUTION_RATIO) ? Color.yellow : Color.black;
        }
        #endregion

        #region // [Func] Marking //
        void RunMarking(System.Action lpCompleteCallback = null)
        {
            if (ClearCoroutine(ref mCorRunMarking))
                mCorRunMarking = StartCoroutine(CorRunMarking(lpCompleteCallback));
        }

        IEnumerator CorRunMarking(System.Action lpCompleteCallback = null)
        {
            int passCount = 0;
            var list = GetScrollItemList();
            foreach (var it in list)
            {
                var itemUI = it as ItemUIExamInfo;
                if (itemUI != null)
                {
                    itemUI.ShowResult(true);

                    if (itemUI.IsPass())
                        passCount++;
                }   

                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(0.5f);

            var msg = string.Format("Pass count : {0} / {1}", passCount, list.Count);
            CommonFunc.OpenToastUI(msg);
            if (passCount.Equals(list.Count))
                SoundManager.Instance.Play(E_Sound_Item.Sfx_PerfectScore);
            yield return new WaitForSeconds(2f);
            
            lpCompleteCallback?.Invoke();
            mCorRunMarking = null;
        }
        #endregion

        #region // [Func] Callback //
        void OnClickCloseButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click);

            CloseUI();
        }

        void OnClickSubmitButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click_Submit);

            if (mExamInfo.IsStart)
            {
                var info = mQuizList[mExamInfo.Index];

                ExamScrollItemData data = new(info.En, info.Kr, _inputEn.text);
                mItemList.Add(data);
                AddScrollItem(false, (ui) => {
                    var itemUI = ui as ItemUIExamInfo;
                    if (itemUI != null)
                    {
                        itemUI.transform.SetParent(_scrollList.content);
                        itemUI.transform.localScale = Vector3.one;
                        itemUI.transform.SetAsLastSibling();
                        itemUI.SetData(data);
                    }
                });

                mExamInfo.Next();
                UpdateUIInput();
            }
        }

        void OnClickStartButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click);

            SetExamState(E_ExamState.Testing);

            mExamInfo.Next();
            UpdateUIInput();
        }

        void OnClickMarkingButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click);

            RunMarking(() => {
                SetExamState(E_ExamState.Retry);
            });
        }

        void OnClickRetryButton()
        {
            SoundManager.Instance.Play(E_Sound_Item.Sfx_Click);

            SetExamState(E_ExamState.Ready);
            UpdateUI();
        }

        void CallbackQuitNextFinish()
        {
            SetExamState(E_ExamState.Marking);
        }
        #endregion
    }
}
