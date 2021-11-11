using Caches;
using DataStructure;
using Framework;
using Handler;
using Newtonsoft.Json;
using System;
using System.Linq;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Clients
{
    public class QuestClient
    {
        private QuestCache _questCache;
        private QuestConfigHandler _configHandler;

        public QuestClient(QuestCache questCache, QuestConfigHandler configHandler)
        {
            _questCache = questCache;
            _configHandler = configHandler;
        }

        public void FetchAllQuest(Action successCallback = null,
            Action failureCallback = null)
        {
            _questCache.Reload(_configHandler.LoadConfig());

            successCallback?.Invoke();
        }

        public void AddQuest(string id,
            int acumulateCreateCount,
            string description,
            int rewardPoint,
            Action successCallback = null,
            Action failureCallback = null)
        {
            var quest = new Quest(id, description, rewardPoint, acumulateCreateCount * 10);
            _questCache.OnAddQuest(quest);
            SaveAllQuestArchive();
            Archive.WriteValue(QuestCache.AccumulateCreateCountKeyName, acumulateCreateCount + 1);

            successCallback?.Invoke();
        }

        public void EditQuest(string questId,
            string description,
            int rewardPoint,
            int sortOrder,
            Action successCallback = null,
            Action failureCallback = null)
        {
            _questCache.OnEditQuest(questId, description, rewardPoint, sortOrder);
            SaveAllQuestArchive();

            successCallback?.Invoke();
        }

        public void RemoveQuest(string questId,
            Action successCallback = null,
            Action failureCallback = null)
        {
            _questCache.OnRemoveQuest(questId);
            SaveAllQuestArchive();

            successCallback?.Invoke();
        }

        public void AccomplishQuest(string questId,
            Action successCallback = null,
            Action failureCallback = null)
        {
            _questCache.OnAccomplishQuest(questId);
            SaveAllQuestArchive();

            successCallback?.Invoke();
        }

        public void RestoreQuest(string questId,
            Action successCallback = null,
            Action failureCallback = null)
        {
            _questCache.OnRestoreQuest(questId);
            SaveAllQuestArchive();

            successCallback?.Invoke();
        }

        public void ResetAllQuestAccomplishState(Action successCallback = null,
            Action failureCallback = null)
        {
            _questCache.OnResetAllQuestAccomplishState();
            SaveAllQuestArchive();

            successCallback?.Invoke();
        }

        private void SaveAllQuestArchive()
        {
            var questList = _questCache.GetAllQuest().ToList();
            _configHandler.SaveConfig(JsonConvert.SerializeObject(questList));
        }
    }
}