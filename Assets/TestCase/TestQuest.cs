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
    public class TestQuest : MonoBehaviour
    {
        private void Start()
        {
            var quest = new Quest("q_id", "q_des", 10, false);
            Debug.Log(quest);

            string json = JsonConvert.SerializeObject(quest, Formatting.Indented);
            Debug.Log(json);

            string newJson = @"{'id': 'new_id','description': 'new_des','reward_point': 99,'accomplish': true}";
            var newQuest = JsonConvert.DeserializeObject<Quest>(newJson);
            Debug.Log(newQuest);

        }
    }
}