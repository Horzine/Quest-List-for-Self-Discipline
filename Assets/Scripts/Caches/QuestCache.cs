using DataStructure;
using Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Caches
{
    public interface IQuestCacheObserver
    {
        void OnCacheReloaded();
        void OnAccomplishQuest(Quest quest);
        void OnRestoreQuest(Quest quest);
        void OnAddQuest(Quest quest);
        void OnRemoveQuest(Quest quest);
        void OnEditQuest(Quest quest);
        void OnResetAllQuestAccomplishState();
    }
    public class QuestCache : Cache
    {
        public const string AccumulateCreateCountKeyName = "accumulate_create_count";
        const string QuestIdTemplate = "quest_";

        private readonly Dictionary<string, Quest> _allQuests = new Dictionary<string, Quest>();
        private readonly List<IQuestCacheObserver> _questCacheObservers = new List<IQuestCacheObserver>();

        public QuestCache()
        {

        }

        public void Reload(string quests)
        {
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

            NotifyQuestCacheObserver((observer) => observer.OnCacheReloaded());

            NotifyCacheObserver();
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

        public (string nextId, int currentAcumulateCreateCount) GetNextCreateQuestId()
        {
            int accumulateCreateCount = Archive.HasKey(AccumulateCreateCountKeyName) ?
                Archive.ReadValue<int>(AccumulateCreateCountKeyName) : 0;
            return (QuestIdTemplate + accumulateCreateCount.ToString(), accumulateCreateCount);
        }

        private void NotifyQuestCacheObserver(Action<IQuestCacheObserver> action)
        {
            var copy = _questCacheObservers.GetRange(0, _questCacheObservers.Count);
            foreach (var item in copy)
            {
                action?.Invoke(item);
            }
        }

        public void AddQuestCacheObserver(IQuestCacheObserver observer)
        {
            var ob = _questCacheObservers.Find(o => o == observer);
            if (ob == null)
            {
                _questCacheObservers.Add(observer);
            }
        }

        public void RemoveQuestCacheObserver(IQuestCacheObserver observer)
        {
            _questCacheObservers.Remove(observer);
        }

        public int CalculateCurrentReward()
        {
            int reward = 0;
            foreach (var item in _allQuests)
            {
                if (item.Value.Accomplish)
                {
                    reward += item.Value.RewardPoint;
                }
            }
            return reward;
        }

        public void OnAddQuest(Quest quest)
        {
            _allQuests.Add(quest.Id, quest);

            NotifyQuestCacheObserver((observer) => observer.OnAddQuest(quest));
        }

        public void OnEditQuest(string questId, string description, int rewardPoint, int sortOrder)
        {
            var quest = GetQuest(questId);
            quest.Update(description, rewardPoint, sortOrder);

            NotifyQuestCacheObserver((observer) => observer.OnEditQuest(quest));
        }

        public void OnRemoveQuest(string questId)
        {
            var quest = GetQuest(questId);
            _allQuests.Remove(questId);

            NotifyQuestCacheObserver((observer) => observer.OnRemoveQuest(quest));
        }

        public void OnAccomplishQuest(string questId)
        {
            var quest = GetQuest(questId);
            quest.Accomplish = true;

            NotifyQuestCacheObserver((observer) => observer.OnAccomplishQuest(quest));
        }

        public void OnRestoreQuest(string questId)
        {
            var quest = GetQuest(questId);
            quest.Accomplish = false;

            NotifyQuestCacheObserver((observer) => observer.OnRestoreQuest(quest));
        }

        public void OnResetAllQuestAccomplishState()
        {
            foreach (var item in _allQuests)
            {
                item.Value.Accomplish = false;
            }

            NotifyQuestCacheObserver((observer) => observer.OnResetAllQuestAccomplishState());
        }
    }
}