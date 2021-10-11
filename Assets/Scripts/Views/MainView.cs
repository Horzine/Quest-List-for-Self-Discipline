using System.Collections;
using System.Collections.Generic;
using Cache;
using DataStructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views.QuestList;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Views
{
    public interface IMainViewProtocol { }
    public class MainView : MonoBehaviour, IMainViewProtocol, IQuestCacheObserver
    {
        [SerializeField] private TextMeshProUGUI _motto_txt;
        [SerializeField] private Button _addQuest_btn;
        [SerializeField] private TextMeshProUGUI _addQuest_txt;
        [SerializeField] private Button _removeQuest_btn;
        [SerializeField] private TextMeshProUGUI _removeQuest_txt;

        private QuestCache _questCache;
        private QuestListView _questListView;

        public void Init(QuestCache questCache)
        {
            _questCache = questCache;
        }

        // Interface APIs
        public void OnAccomplishQuest(Quest quest) { }

        public void OnCacheReloaded() { }

        public void OnRestoreQuest(Quest quest) { }
    }
}