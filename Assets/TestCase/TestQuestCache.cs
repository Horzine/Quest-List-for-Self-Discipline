using System.Collections.Generic;
using Cache;
using DataStructure;
using Newtonsoft.Json;
using UnityEngine;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace TestCase
{
    public class TestQuestCacheObserver : IQuestCacheObserver
    {
        private string _name;
        public TestQuestCacheObserver(string name)
        {
            _name = name;
        }
        public void OnAccomplishQuest(Quest quest)
        {
            Debug.Log($"{_name} OnAccomplishQuest {quest.Id}");
        }

        public void OnAddQuest(Quest quest)
        {
            throw new System.NotImplementedException();
        }

        public void OnCacheReloaded()
        {
            Debug.Log($"{_name} OnCacheReloaded");
        }

        public void OnRemoveQuest(Quest quest)
        {
            throw new System.NotImplementedException();
        }

        public void OnRestoreQuest(Quest quest)
        {
            Debug.Log($"{_name} OnRestoreQuest {quest.Id}");
        }
    }
    public class TestQuestCache : MonoBehaviour
    {
        private void Start()
        {
            var list = new List<Quest>
            {
                new Quest("q_1", "qdes_1", 1, 1, false),
                new Quest("q_2", "qdes_2", 1, 1, false),
                new Quest("q_3", "qdes_3", 1, 1, false),
                new Quest("q_4", "qdes_4", 1, 1, false)
            };
            string json = JsonConvert.SerializeObject(list);
            // var cache = new QuestCache();
            // var ob_1 = new TestQuestCacheObserver("ob_1");
            // var ob_2 = new TestQuestCacheObserver("ob_2");
            // var ob_3 = new TestQuestCacheObserver("ob_3");
            // cache.AddObserver(ob_1);
            // cache.AddObserver(ob_2);
            // cache.AddObserver(ob_3);
            // cache.Reload(json);
            // 
            // cache.AccomplishQuest("");
            // cache.AccomplishQuest("q_1");
            // 
            // cache.RemoveObserver(ob_2);
            // cache.RestoreQuest("q_1");
        }
    }
}