using System;
using System.Collections.Generic;
using DataStructure;
using Newtonsoft.Json;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Cache
{
    public interface IQuestCacheObserver
    {
        void OnCacheReloaded();
        void OnAccomplishQuest(Quest quest);
        void OnRestoreQuest(Quest quest);
    }
    public class QuestCache
    {
        private Dictionary<string, Quest> _allQuests = new Dictionary<string, Quest>();
        private List<IQuestCacheObserver> _observers = new List<IQuestCacheObserver>();

        public void Init()
        {

        }

        public void Reload(string quests)
        {
            _allQuests.Clear();
            var questList = JsonConvert.DeserializeObject<List<Quest>>(quests);
            foreach (var item in questList)
            {
                if (item != null)
                {
                    _allQuests.Add(item.Id, item);
                }
            }

            NotifyObserver((observer) => observer.OnCacheReloaded());
        }

        public Quest GetQuest(string questId)
        {
            return _allQuests.ContainsKey(questId) ? _allQuests[questId] : null;
        }

        public void AccomplishQuest(string questId)
        {
            var quest = GetQuest(questId);
            if (quest != null)
            {
                quest.Accomplish = true;

                NotifyObserver((observer) => observer.OnAccomplishQuest(quest));
            }
        }

        public void RestoreQuest(string questId)
        {
            var quest = GetQuest(questId);
            if (quest != null)
            {
                quest.Accomplish = false;

                NotifyObserver((observer) => observer.OnRestoreQuest(quest));
            }
        }

        private void NotifyObserver(Action<IQuestCacheObserver> action)
        {
            var copy = _observers.GetRange(0, _observers.Count);
            foreach (var item in copy)
            {
                action?.Invoke(item);
            }
        }

        public void AddObserver(IQuestCacheObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IQuestCacheObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}