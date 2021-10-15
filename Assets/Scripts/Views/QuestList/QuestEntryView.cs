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

        [SerializeField] private Text _description_txt;
        [SerializeField] private TextMeshProUGUI _rewardPoint_txt;
        [SerializeField] private Button _accomplish_btn;

        private Action<Quest> _clickAccomplishBtn;
        private Action _longPressedEntry;
        private Quest _quest;
        private QuestEntryState _state = QuestEntryState.NullState;
        private Coroutine _longPressedCoroutine;

        enum QuestEntryState
        {
            NullState = 0,
            Incomplete = 1,
            Accomplish = 2,
        }

        public void Init(Cache.QuestCache questCache, Quest quest, Action<Quest> clickAccomplishBtn, Action longPressedEntry)
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

                    break;
                case QuestEntryState.Accomplish:

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
            if (_longPressedCoroutine == null)
            {
                _longPressedCoroutine = StartCoroutine(LongPressedTimer());
            }
        }

        private void OnEntryPointerUp()
        {
            Debug.Log("On Entry Pointer Up");
            if (_longPressedCoroutine != null)
            {
                StopCoroutine(_longPressedCoroutine);
                _longPressedCoroutine = null;
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