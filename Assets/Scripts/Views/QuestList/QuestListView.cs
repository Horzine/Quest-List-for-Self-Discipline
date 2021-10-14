using System.Collections;
using System.Collections.Generic;
using Cache;
using DataStructure;
using Framework;
using UnityEngine;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Views.QuestList
{
    public class QuestListView : MonoBehaviour, IQuestCacheObserver
    {
        [SerializeField] private RectTransform _content;

        private GameObject _entryPrefab;
        private QuestCache _questCache;
        private List<QuestEntryView> _entryViews = new List<QuestEntryView>();

        public void Init(QuestCache questCache, List<Quest> quests)
        {
            _questCache = questCache;
            _questCache.AddObserver(this);

            _entryPrefab = AssetsLoader.GetInstance().LoadGameObject("Assets/Resources/Views/quest_entry_view.prefab");
            foreach (var item in quests)
            {
                CreateEntryView(item);
            }

            SortEntry();
        }

        private void OnDestroy()
        {
            _questCache?.RemoveObserver(this);
        }

        private void CreateEntryView(Quest quest)
        {
            var view = Instantiate(_entryPrefab, _content).GetComponent<QuestEntryView>();
            view.Init(_questCache, quest);
            _entryViews.Add(view);
        }

        private void SortEntry()
        {
            _entryViews.Sort();

            for (int i = 0; i < _entryViews.Count; ++i)
            {
                _entryViews[i].transform.SetSiblingIndex(i);
            }
        }

        // Interface APIs
        public void OnAccomplishQuest(Quest quest) { }

        public void OnAddQuest(Quest quest)
        {
            CreateEntryView(quest);
        }

        public void OnCacheReloaded() { }

        public void OnRemoveQuest(Quest quest) { }

        public void OnRestoreQuest(Quest quest) { }
    }
}