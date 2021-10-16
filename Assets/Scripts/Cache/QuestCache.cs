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
        void OnEditQuest(Quest quest);
    }
    public class QuestCache
    {
        const string AccumulateCreateCountKeyName = "accumulate_create_count";

        private readonly Dictionary<string, Quest> _allQuests = new Dictionary<string, Quest>();
        private readonly List<IQuestCacheObserver> _observers = new List<IQuestCacheObserver>();
        private readonly QuestConfigHandler _configHandler;

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

        public IEnumerable<Quest> GetAllQuest()
        {
            var quests = _allQuests.Values.ToList();
            quests.Sort();
            return quests;
        }

        public void AddQuest(string description, int rewardPoint)
        {
            var (nextId, currentAcumulateCreateCount) = GetNextCreateQuestId();
            var quest = new Quest(nextId, description, rewardPoint, currentAcumulateCreateCount * 10);
            _allQuests.Add(nextId, quest);
            SaveAllQuestArchive();
            Archive.WriteValue(AccumulateCreateCountKeyName, currentAcumulateCreateCount + 1);
            NotifyObserver((observer) => observer.OnAddQuest(quest));
        }

        public void EditQuest(string questId, string description, int rewardPoint, int sortOrder)
        {
            var quest = GetQuest(questId);
            quest.Update(description, rewardPoint, sortOrder);
            SaveAllQuestArchive();
            NotifyObserver((observer) => observer.OnEditQuest(quest));
        }

        public (string nextId, int currentAcumulateCreateCount) GetNextCreateQuestId()
        {
            int accumulateCreateCount = Archive.HasKey(AccumulateCreateCountKeyName) ?
                Archive.ReadValue<int>(AccumulateCreateCountKeyName) : 0;
            return ($"quest_{accumulateCreateCount}", accumulateCreateCount);
        }

        public void RemoveQeust(string questId)
        {
            var quest = GetQuest(questId);
            _allQuests.Remove(questId);
            SaveAllQuestArchive();
            NotifyObserver((observer) => observer.OnRemoveQuest(quest));
        }

        public void AccomplishQuest(string questId)
        {
            var quest = GetQuest(questId);
            quest.Accomplish = true;
            SaveAllQuestArchive();
            NotifyObserver((observer) => observer.OnAccomplishQuest(quest));
        }

        public void RestoreQuest(string questId)
        {
            var quest = GetQuest(questId);
            quest.Accomplish = false;
            SaveAllQuestArchive();
            NotifyObserver((observer) => observer.OnRestoreQuest(quest));
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

        private void SaveAllQuestArchive()
        {
            var questList = _allQuests.Values.ToList();
            _configHandler.SaveConfig(JsonConvert.SerializeObject(questList));
        }
    }
}