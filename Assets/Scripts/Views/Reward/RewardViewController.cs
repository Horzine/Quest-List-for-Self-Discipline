using System.Collections;
using System.Collections.Generic;
using Framework;
using Handler;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Views.Reward
{
    public class RewardViewController : ViewController
    {
        [SerializeField] private TextMeshProUGUI _rewardPoint;
        [SerializeField] private Button _claimReward;

        private RewardHandler _rewardHandler;

        public void Init(RewardHandler rewardHandler, int rewardPoint)
        {
            _rewardPoint.text = rewardPoint.ToString();
            _rewardHandler = rewardHandler;

            _claimReward.onClick.AddListener(OnClickClaimRewardBtn);
        }

        private void OnClickClaimRewardBtn()
        {
            _rewardHandler.ClaimReward();
        }
    }
}