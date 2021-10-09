using Newtonsoft.Json;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace DataStructure
{
    public class Quest
    {
        public Quest(string id, string description, int rewardPoint, bool accomplish)
        {
            Id = id;
            Description = description;
            RewardPoint = rewardPoint;
            Accomplish = accomplish;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; private set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; private set; }

        [JsonProperty(PropertyName = "reward_point")]
        public int RewardPoint { get; private set; }

        [JsonProperty(PropertyName = "accomplish")]
        public bool Accomplish { get; set; }

        public override string ToString()
        {
            return $"[Task]: Id = {Id}, Description = {Description}, RewardPoint = {RewardPoint}, Accomplish = {Accomplish}";
        }
    }
}