using System.Collections;
using System.Collections.Generic;
using Framework;
using Newtonsoft.Json.Linq;
using UnityEngine;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Handler
{
    public class QuestConfigHandler
    {
        const string ConfigKeyName = "quests";

        public string LoadConfig()
        {
            return Archive.HasKey(ConfigKeyName) ? Archive.ReadValue<string>(ConfigKeyName) : null;
        }

        public void SaveConfig(string config)
        {
            Archive.WriteValue(ConfigKeyName, config);
        }

    }
}