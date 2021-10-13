using System;
using System.Collections.Generic;
using System.Linq;
using DataStructure;
using Framework;
using Handler;
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
        void OnAddQuest(Quest quest);
        void OnRemoveQuest(Quest quest);
    }
    public class QuestCache
    {
        const string AccumulateCreateCountKeyName = "accumulate_create_count";

        private Dictionary<string, Quest> _allQuests = new Dictionary<string, Quest>();
        private List<IQuestCacheObserver> _observers = new List<IQuestCacheObserver>();
        private QuestConfigHandler _configHandler;

        public QuestCache(QuestConfigHandler confighandler)
        {
            _configHandler = confighandler;
        }

        public void Reload()
        {
            string quests = _configHandler.LoadConfig();
            _allQuests.Clear();

            if (quests != null)
            {
                var questList = JsonConvert.DeserializeObject<List<Quest>>(quests);
                foreach (var item in questList)
                {
                    if (item != null)
                    {
                        _allQuests.Add(item.Id, item);
                    }
                }
            }

            NotifyObserver((observer) => observer.OnCacheReloaded());
        }

        public Quest GetQuest(string questId)
        {
            return _allQuests.ContainsKey(questId) ? _allQuests[questId] : null;
        }

        public void AddQuest(string description, int rewardPoint)
        {
            var (nextId, currentAcumulateCreateCount) = GetNextCreateQuestId();
            var quest = new Quest(nextId, description, rewardPoint);
            _allQuests.Add(nextId, quest);

            var questList = _allQuests.Values.ToList();
            _configHandler.SaveConfig(JsonConvert.SerializeObject(questList));

            Archive.WriteValue(AccumulateCreateCountKeyName, ++currentAcumulateCreateCount);

            NotifyObserver((observer) => observer.OnAddQuest(quest));
        }

        public (string nextId, int currentAcumulateCreateCount) GetNextCreateQuestId()
        {
            int accumulateCreateCount = Archive.HasKey(AccumulateCreateCountKeyName) ?
                Archive.ReadValue<int>(AccumulateCreateCountKeyName) : 0;
            return ($"quest_{accumulateCreateCount + 1}", accumulateCreateCount);
        }

        public void RemoveQeust()
        {

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