using System.Collections.Generic;
using Caches;
using DataStructure;
using Handler;
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
    public class TestQeustConfigHandler : MonoBehaviour
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
            string config = JsonConvert.SerializeObject(list);

            var handler = new QuestConfigHandler();
            handler.SaveConfig(config);

            var cache = new QuestCache();
            // cache.Reload();

            Debug.Log(cache.GetQuest("q_1").Description);
        }
    }
}