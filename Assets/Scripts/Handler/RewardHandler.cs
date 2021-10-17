using System;
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