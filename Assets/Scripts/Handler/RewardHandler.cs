using System;
using Cache;
using Framework;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Handler
{
    public class RewardHandler
    {
        const string TomorrowRewardTimeKeyName = "tomorrow_reward_time";

        private QuestCache _questCache;
        private Action<int> _notifyReward;

        public RewardHandler(QuestCache questCache)
        {
            _questCache = questCache;
        }

        public void SetNotifyRewardCallback(Action<int> notifyReward)
        {
            _notifyReward = notifyReward;
        }

        public void HandleCurrentRewardState()
        {
            if (IsReachRewardTime())
            {
                _notifyReward?.Invoke(_questCache.CalculateCurrentReward());
            }
        }

        public void ClaimReward()
        {
            Archive.WriteValue(TomorrowRewardTimeKeyName, GetTomorrowZeroTimeStamp());
            _questCache.ResetAllQuestAccomplishState();
        }

        private bool IsReachRewardTime()
        {
            if (!Archive.HasKey(TomorrowRewardTimeKeyName))
            {
                Archive.WriteValue(TomorrowRewardTimeKeyName, GetTomorrowZeroTimeStamp());
                return false;
            }

            return GetNowTimeStamp() >= Archive.ReadValue<long>(TomorrowRewardTimeKeyName);
        }

        public static long GetNowTimeStamp()
        {
            return DateTime.Now.GetTimeStamp();
        }

        public static long GetTomorrowZeroTimeStamp()
        {
            var now = DateTime.Now;
            var tomorrow = new DateTime(now.Year, now.Month, now.Day + 1);
            return tomorrow.GetTimeStamp();
        }

        public static int GetSecondsRemainingUntilTomorrow()
        {
            return (int)(GetTomorrowZeroTimeStamp() - GetNowTimeStamp());
        }

    }
}