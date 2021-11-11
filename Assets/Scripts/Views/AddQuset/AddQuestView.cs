using Caches;
using Clients;
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
        private QuestClient _questClient;

        public void Init(QuestCache questCache, QuestClient questClient)
        {
            _questCache = questCache;
            _questClient = questClient;

            _create_btn.onClick.AddListener(OnClickCreateBtn);
            _create_txt.text = "Create";

            _close_btn.onClick.AddListener(OnClickCloseBtn);
            _close_txt.text = "Close";

            _id_txt.text = _questCache.GetNextCreateQuestId().nextId;

            _description_lable.text = "Description";
            _reward_label.text = "Reward Point";

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
            var (nextId, count) = _questCache.GetNextCreateQuestId();
            _questClient.AddQuest(nextId, count, description, int.Parse(rewardPoint));
            Close();
        }

        private void OnClickCloseBtn()
        {
            Close();
        }
    }
}