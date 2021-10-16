using System;
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
        [SerializeField] private RectTransform _viewPort;
        [SerializeField] private RectTransform _content;

        private GameObject _entryPrefab;
        private QuestCache _questCache;
        private List<QuestEntryView> _entryViews = new List<QuestEntryView>();
        private IMainViewProtocol _mainView;

        public void Init(QuestCache questCache, IEnumerable<Quest> quests, IMainViewProtocol mainView)
        {
            _questCache = questCache;
            _questCache.AddObserver(this);
            _mainView = mainView;

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

        private void OnClickAccomplish(string questId)
        {
            var quest = _questCache.GetQuest(questId);
            if (!quest.Accomplish)
            {
                _questCache.AccomplishQuest(quest.Id);
            }
            else
            {
                _questCache.RestoreQuest(quest.Id);
            }
        }

        private void OnLongPressedEntry(string questId, RectTransform entryRtf)
        {
            _mainView.ShowEntryOperationView(questId, entryRtf);
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

        public Rect GetViewPortRect()
        {
            return _viewPort.rect;
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