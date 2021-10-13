using Cache;
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
namespace Views.AddQuset
{
    public class AddQuestView : ViewController
    {
        [SerializeField] private Button _create_btn;
        [SerializeField] private TextMeshProUGUI _create_txt;
        [SerializeField] private Button _close_btn;
        [SerializeField] private TextMeshProUGUI _close_txt;
        [SerializeField] private TextMeshProUGUI _id_txt;
        [SerializeField] private TextMeshProUGUI _description_lable;
        [SerializeField] private InputField _description_ipf;
        [SerializeField] private TextMeshProUGUI _reward_label;
        [SerializeField] private InputField _reward_ipf;

        private QuestCache _questCache;

        public void Init(QuestCache questCache)
        {
            _questCache = questCache;

            _create_btn.onClick.AddListener(OnClickCreateBtn);
            _create_txt.text = "Create";

            _close_btn.onClick.AddListener(OnClickCloseBtn);
            _close_txt.text = "Close";

            _id_txt.text = _questCache.GetNextCreateQuestId().nextId;
        }

        private void OnClickCreateBtn()
        {
            string description = _description_ipf.text;
            string rewardPoint = _reward_ipf.text;
            if (string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(rewardPoint))
            {
                Debug.LogError("Description is Empty or Reward is Empty");
                return;
            }
            _questCache.AddQuest(description, int.Parse(rewardPoint));
            Close();
        }

        private void OnClickCloseBtn()
        {
            Close();
        }
    }
}