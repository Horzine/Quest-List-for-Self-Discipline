using System;
using System.Collections;
using System.Collections.Generic;
using DataStructure;
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

        private Quest _quest;

        public void Init(Cache.QuestCache questCache, Quest quest)
        {
            _quest = quest;

            SetupView();
        }

        public void SetupView()
        {
            _rewardPoint_txt.text = $"{_quest.RewardPoint} RMB";
        }

        public int CompareTo(QuestEntryView other)
        {
            return _quest.CompareTo(other.GetQuest());
        }

        public Quest GetQuest()
        {
            return _quest;
        }

    }
}