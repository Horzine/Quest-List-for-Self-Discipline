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
        [SerializeField] private TextMeshProUGUI _title_txt;
        [SerializeField] private TextMeshProUGUI _rewardPoint_txt;
        [SerializeField] private TextMeshProUGUI _claimBtn_txt;
        [SerializeField] private Button _claimReward_btn;

        private RewardHandler _rewardHandler;

        public void Init(RewardHandler rewardHandler, int rewardPoint)
        {
            _title_txt.text = "Reward";
            _claimBtn_txt.text = "Claimed";
            _rewardPoint_txt.text = rewardPoint.ToString();
            _rewardHandler = rewardHandler;

            _claimReward_btn.onClick.AddListener(OnClickClaimRewardBtn);
        }

        private void OnClickClaimRewardBtn()
        {
            _rewardHandler.ClaimReward();

            Close();
        }
    }
}