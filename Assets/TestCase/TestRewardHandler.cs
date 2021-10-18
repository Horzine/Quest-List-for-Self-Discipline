using System.Collections;
using System.Collections.Generic;
using Handler;
using UnityEngine;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace TestCase
{
    public class TestRewardHandler : MonoBehaviour
    {

        private void Start()
        {
            Debug.Log($"Now: {RewardHandler.GetNowTimeStamp()}");
            Debug.Log($"Tomorrow: {RewardHandler.GetTomorrowZeroTimeStamp()}");
            Debug.Log($"Remaining: {RewardHandler.GetSecondsRemainingUntilTomorrow()}");
        }



    }
}