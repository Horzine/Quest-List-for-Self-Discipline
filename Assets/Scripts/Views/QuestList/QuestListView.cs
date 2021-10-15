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
            view.Init(quest, OnClickAccomplish, OnLongPressedEntry);
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

        private void OnClickAccomplish(Quest quest)
        {
            if (!quest.Accomplish)
            {
                _questCache.AccomplishQuest(quest.Id);
            }
            else
            {
                _questCache.RestoreQuest(quest.Id);
            }
        }

        private void OnLongPressedEntry()
        {
            Debug.Log("Long Pressed");
        }

        private void RefreshEntryByQuestId(string questId)
        {
            QuestEntryView view = null;
            foreach (var item in _entryViews)
            {
                if (item.GetQuest().Id == questId)
                {
                    view = item;
                    break;
                }
            }

            if (view != null)
                view.RefreshView();
        }

        // Interface APIs
        public void OnAccomplishQuest(Quest quest)
        {
            RefreshEntryByQuestId(quest.Id);

            SortEntry();
        }

        public void OnAddQuest(Quest quest)
        {
            CreateEntryView(quest);

            SortEntry();
        }

        public void OnCacheReloaded() { }

        public void OnRemoveQuest(Quest quest) { }

        public void OnRestoreQuest(Quest quest)
        {
            RefreshEntryByQuestId(quest.Id);

            SortEntry();
        }
    }
}