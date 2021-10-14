using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private Text _description_txt;
        [SerializeField] private TextMeshProUGUI _rewardPoint_txt;
        [SerializeField] private Button _accomplish_btn;
        [SerializeField] private Button _operation_btn;

        private Action<Quest> _clickAccomplishBtn;
        private Quest _quest;

        public void Init(Cache.QuestCache questCache, Quest quest, Action<Quest> clickAccomplishBtn)
        {
            _quest = quest;
            _clickAccomplishBtn = clickAccomplishBtn;

            var eventListener = EventTriggerListener.Get(gameObject);
            eventListener._onPointerDown += (go, eventData) => Debug.Log("Point Down");

            _accomplish_btn.onClick.AddListener(OnClickAccomplishBtn);

            SetupView();
        }

        private void SetupView()
        {
            _rewardPoint_txt.text = $"{_quest.RewardPoint} RMB";
        }

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

        public Quest GetQuest()
        {
            return _quest;
        }

        private void OnClickAccomplishBtn()
        {
            _clickAccomplishBtn?.Invoke(_quest);
        }

        private void OnOperationBtnDown()
        {

        }
    }
}