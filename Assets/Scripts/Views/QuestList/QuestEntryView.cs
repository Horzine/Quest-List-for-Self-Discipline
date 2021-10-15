using System;
using System.Collections;
using DataStructure;
using Framework;
using TMPro;
using UnityEngine;
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
        const float LongPressedActiveTime = 2;

        [Header("Art Setting"), SerializeField] private Color _incompleteColor;
        [SerializeField] private Color _accomplishColor;
        [Space(30)]
        [SerializeField] private Image _entryBackground_img;
        [SerializeField] private Text _description_txt;
        [SerializeField] private TextMeshProUGUI _rewardPoint_txt;
        [SerializeField] private Button _accomplish_btn;

        private Action<Quest> _clickAccomplishBtn;
        private Action _longPressedEntry;
        private Quest _quest;
        private QuestEntryState _state = QuestEntryState.NullState;
        private Coroutine _longPressedTimerCoroutine;

        enum QuestEntryState
        {
            NullState = 0,
            Incomplete = 1,
            Accomplish = 2,
        }

        public void Init(Quest quest, Action<Quest> clickAccomplishBtn, Action longPressedEntry)
        {
            _quest = quest;
            _clickAccomplishBtn = clickAccomplishBtn;
            _longPressedEntry = longPressedEntry;

            var eventListener = EventTriggerListener.Get(gameObject);
            eventListener._onPointerDown += (go, eventData) => OnEntryPointerDown();
            eventListener._onPointerUp += (go, eventData) => OnEntryPointerUp();

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

            switch (newState)
            {
                case QuestEntryState.Incomplete:
                    _entryBackground_img.color = _incompleteColor;
                    break;
                case QuestEntryState.Accomplish:
                    _entryBackground_img.color = _accomplishColor;
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
            _clickAccomplishBtn?.Invoke(_quest);
        }

        private void OnEntryPointerDown()
        {
            Debug.Log("On Entry Pointer Down");
            if (_longPressedTimerCoroutine == null)
            {
                _longPressedTimerCoroutine = StartCoroutine(LongPressedTimer());
            }
        }

        private void OnEntryPointerUp()
        {
            Debug.Log("On Entry Pointer Up");
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
                    _longPressedEntry?.Invoke();
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