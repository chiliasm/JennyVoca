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
                    mFinishCallback?.Invoke();
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
        const float TEXT_CAUTION_RATIO = 0.75f;

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
            UpdateUIScroll();
            UpdateUITimer();
        }

        void UpdateUITitle()
        {
            _textOrder.text = mOrderName;
        }

        void UpdateUIScroll()
        {
            RemoveAllScrollItem();

            foreach (var it in mItemList)
            {
                AddScrollItem(true, (ui) =>
                {
                    var itemUI = ui as ItemUIExamInfo;
                    if (itemUI != null)
                    {
                        itemUI.transform.SetParent(_scrollList.content);
                        itemUI.transform.SetAsLastSibling();
                        itemUI.SetData(it);
                    }
                });
            }
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
            _textTimer.color = (ratio >= TEXT_CAUTION_RATIO) ? Color.red : Color.black;
        }
        #endregion

        #region // [Func] Marking //
        void RunMarking()
        {
            if (ClearCoroutine(ref mCorRunMarking))
                mCorRunMarking = StartCoroutine(CorRunMarking());
        }

        IEnumerator CorRunMarking()
        {
            var list = GetScrollItemList();
            foreach (var it in list)
            {
                var itemUI = it as ItemUIExamInfo;
                if (itemUI != null)
                    itemUI.ShowResult(true);

                yield return new WaitForSeconds(0.3f);
            }
            mCorRunMarking = null;
        }
        #endregion

        #region // [Func] Callback //
        void OnClickCloseButton()
        {
            CommonFunc.PlayClickSound();
        }

        void OnClickSubmitButton()
        {
            CommonFunc.PlayClickSound();
        }

        void OnClickStartButton()
        {
            CommonFunc.PlayClickSound();

            SetExamState(E_ExamState.Testing);
            mExamInfo.Next();
        }

        void OnClickMarkingButton()
        {
            CommonFunc.PlayClickSound();
        }

        void OnClickRetryButton()
        {
            CommonFunc.PlayClickSound();
        }

        void CallbackQuitNextFinish()
        {
            SetExamState(E_ExamState.Marking);
        }
        #endregion
    }
}
