using System;
using System.Collections;
using DataStructure;
using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Views.QuestList
{
    public class QuestEntryView : MonoBehaviour, IComparable<QuestEntryView>
    {
        const float LongPressedActiveTime = 1;

        [Header("Art Setting"), SerializeField] private Color _incompleteColor;
        [SerializeField] private Color _accomplishColor;
        [SerializeField] private Color _incompleteButtonColor;
        [SerializeField] private Color _accomplishButtonColor;
        [Space(30)]
        [SerializeField] private Image _entryBackground_img;
        [SerializeField] private Text _description_txt;
        [SerializeField] private TextMeshProUGUI _rewardPoint_txt;
        [SerializeField] private Button _accomplish_btn;
        [SerializeField] private TextMeshProUGUI _accomplish_txt;

        private Action<string> _clickAccomplishBtn;
        private Action<string, RectTransform> _longPressedEntry;
        private Quest _quest;
        private QuestEntryState _state = QuestEntryState.NullState;
        private Coroutine _longPressedTimerCoroutine;

        enum QuestEntryState
        {
            NullState = 0,
            Incomplete = 1,
            Accomplish = 2,
        }

        public void Init(Quest quest, Action<string> clickAccomplishBtn, Action<string, RectTransform> longPressedEntry, ScrollRect scrollView)
        {
            _quest = quest;
            _clickAccomplishBtn = clickAccomplishBtn;
            _longPressedEntry = longPressedEntry;

            var eventListener = EventTriggerListener.Get(gameObject);
            eventListener.PointerDownEvent += (go, eventData) => OnEntryPointerDown();
            eventListener.PointerUpEvent += (go, eventData) => OnEntryPointerUp();
            eventListener.BeginDragEvent += (go, eventData) => scrollView.OnBeginDrag(eventData);
            eventListener.DragEvent += (go, eventData) => scrollView.OnDrag(eventData);
            eventListener.EndDragEvent += (go, eventData) => scrollView.OnEndDrag(eventData);

            _accomplish_btn.onClick.AddListener(OnClickAccomplishBtn);

            RefreshView();
        }

        public void RefreshView()
        {
            SetupStaticView();

            SetupDynamicView();
        }

        private void SetupStaticView()
        {
            _description_txt.text = _quest.Description;
            _rewardPoint_txt.text = $"{_quest.RewardPoint} RMB";
        }

        private void SetupDynamicView()
        {
            SwitchToState(AssessmentState());
        }

        void SwitchToState(QuestEntryState newState)
        {
            if (_state.Equals(newState))
                return;
            else
                _state = newState;

            var btnImage = _accomplish_btn.GetComponent<Image>();
            switch (newState)
            {
                case QuestEntryState.Incomplete:
                    _entryBackground_img.color = _incompleteColor;
                    btnImage.color = _incompleteButtonColor;
                    _accomplish_txt.text = "Accomplish";
                    break;

                case QuestEntryState.Accomplish:
                    _entryBackground_img.color = _accomplishColor;
                    btnImage.color = _accomplishButtonColor;
                    _accomplish_txt.text = "Incomplete";
                    break;

                case QuestEntryState.NullState:
                    Debug.LogError("Shouldn't switch to null state");
                    break;
            }
        }

        QuestEntryState AssessmentState()
        {
            return _quest.Accomplish ? QuestEntryState.Accomplish : QuestEntryState.Incomplete;
        }

        public Quest GetQuest()
        {
            return _quest;
        }

        private void OnClickAccomplishBtn()
        {
            _clickAccomplishBtn?.Invoke(_quest.Id);
        }

        private void OnEntryPointerDown()
        {
            if (_longPressedTimerCoroutine == null)
            {
                _longPressedTimerCoroutine = StartCoroutine(LongPressedTimer());
            }
        }

        private void OnEntryPointerUp()
        {
            if (_longPressedTimerCoroutine != null)
            {
                StopCoroutine(_longPressedTimerCoroutine);
                _longPressedTimerCoroutine = null;
            }
        }

        IEnumerator LongPressedTimer()
        {
            float timer = 0;
            while (true)
            {
                yield return null;
                timer += Time.deltaTime;
                if (timer >= LongPressedActiveTime)
                {
                    _longPressedEntry?.Invoke(_quest.Id, GetComponent<RectTransform>());
                    yield break;
                }
            }
        }

        // Interface APIs
        public int CompareTo(QuestEntryView other)
        {
            var otherQuest = other.GetQuest();
            if (_quest.Accomplish != otherQuest.Accomplish)
            {
                return _quest.Accomplish.CompareTo(otherQuest.Accomplish);
            }
            else
            {
                return _quest.CompareTo(other.GetQuest());
            }
        }
    }
}