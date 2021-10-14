using System;
using Newtonsoft.Json;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace DataStructure
{
    public class Quest : IComparable<Quest>
    {
        public Quest(string id, string description, int rewardPoint, int sortOrder, bool accomplish = false)
        {
            Id = id;
            Description = description;
            RewardPoint = rewardPoint;
            SortOrder = sortOrder;
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

        [JsonProperty(PropertyName = "sort_order")]
        public int SortOrder { get; set; }

        public int CompareTo(Quest other)
        {
            return SortOrder.CompareTo(other.SortOrder);
        }

        public override string ToString()
        {
            return $"[Task]: Id = {Id}, Description = {Description}, RewardPoint = {RewardPoint}, Accomplish = {Accomplish}, SortOrder = {SortOrder}";
        }
    }
}