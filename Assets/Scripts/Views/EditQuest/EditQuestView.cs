using Cache;
using DataStructure;
using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Views.EditQuest
{
    public class EditQuestView : ViewController
    {
        [SerializeField] private Button _modify_btn;
        [SerializeField] private TextMeshProUGUI _modify_txt;
        [SerializeField] private Button _close_btn;
        [SerializeField] private TextMeshProUGUI _close_txt;
        [SerializeField] private TextMeshProUGUI _description_lable;
        [SerializeField] private InputField _description_ipf;
        [SerializeField] private TextMeshProUGUI _reward_label;
        [SerializeField] private InputField _reward_ipf;
        [SerializeField] private TextMeshProUGUI _sortOrder_label;
        [SerializeField] private InputField _sortOrder_ipf;

        private QuestCache _questCache;
        private string _questId;

        public void Init(Quest quest, QuestCache questCache)
        {
            _questCache = questCache;
            _questId = quest.Id;

            _modify_btn.onClick.AddListener(OnClickModifyBtn);
            _modify_txt.text = "Modify";

            _close_btn.onClick.AddListener(OnClickCloseBtn);
            _close_txt.text = "Close";

            SetupView(quest);
        }

        private void SetupView(Quest quest)
        {
            _description_lable.text = "Description";
            _reward_label.text = "Reward Point";
            _sortOrder_label.text = "Sorting Order";

            _description_ipf.text = quest.Description;
            _reward_ipf.text = quest.RewardPoint.ToString();
            _sortOrder_ipf.text = quest.SortOrder.ToString();
        }

        private void OnClickModifyBtn()
        {
            string description = _description_ipf.text;
            string rewardPoint = _reward_ipf.text;
            string sortOrder = _sortOrder_ipf.text;
            if (string.IsNullOrWhiteSpace(description) ||
                string.IsNullOrWhiteSpace(rewardPoint) ||
                string.IsNullOrWhiteSpace(sortOrder))
            {
                Debug.LogError("Description is Empty or Reward is Empty or SortOrder is Empty");
                return;
            }
            _questCache.EditQuest(_questId, description, int.Parse(rewardPoint), int.Parse(sortOrder));
            Close();
        }

        private void OnClickCloseBtn()
        {
            Close();
        }

    }
}