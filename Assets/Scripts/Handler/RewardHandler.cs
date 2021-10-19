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
        private Action<int> _notifyRewardCallback;
        private Action _finishClaimRewardCallback;

        public RewardHandler(QuestCache questCache)
        {
            _questCache = questCache;
        }

        public void SetNotifyRewardCallback(Action<int> notifyReward)
        {
            _notifyRewardCallback = notifyReward;
        }

        public void SetFinishClaimRewardCallback(Action finishClaimRewardCallback)
        {
            _finishClaimRewardCallback = finishClaimRewardCallback;
        }

        public void HandleCurrentRewardState()
        {
            if (IsReachRewardTime())
            {
                _notifyRewardCallback?.Invoke(_questCache.CalculateCurrentReward());
            }
        }

        public void ClaimReward()
        {
            Archive.WriteValue(TomorrowRewardTimeKeyName, GetTomorrowZeroTimeStamp());
            _questCache.ResetAllQuestAccomplishState();
            _finishClaimRewardCallback?.Invoke();
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
            if (Constants.DebugMode)
            {
                return GetNowTimeStamp() + 15;
            }
            else
            {
                var now = DateTime.Now;
                var tomorrow = new DateTime(now.Year, now.Month, now.Day + 1);
                return tomorrow.GetTimeStamp();
            }
        }

        public static int GetSecondsRemainingUntilTomorrow()
        {
            return (int)(GetTomorrowZeroTimeStamp() - GetNowTimeStamp());
        }
    }
}