using Cache;
using DataStructure;
using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views.AddQuset;
using Views.QuestList;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Views
{
    public interface IMainViewProtocol { }
    public class MainView : ViewController, IMainViewProtocol, IQuestCacheObserver
    {
        [SerializeField] private TextMeshProUGUI _motto_txt;
        [SerializeField] private Button _addQuest_btn;
        [SerializeField] private TextMeshProUGUI _addQuest_txt;

        private QuestCache _questCache;
        private QuestListView _questListView;

        public void Init(QuestCache questCache)
        {
            _questCache = questCache;
            _questCache.AddObserver(this);

            SetupView();

            LoadQuestListView();
        }

        private void OnDestroy()
        {
            _questCache?.RemoveObserver(this);
        }

        private void SetupView()
        {
            _motto_txt.text = "Dedication Focus Discipline\nPractice more !!!";
            _addQuest_txt.text = "Add Quest";

            _addQuest_btn.onClick.AddListener(OnClickAddQuestBtn);
        }

        private void OnClickAddQuestBtn()
        {
            var prefab = AssetsLoader.GetInstance().LoadGameObject("Assets/Resources/Views/add_quest_view.prefab");
            var view = Instantiate(prefab).GetComponent<AddQuestView>();
            view.Init(_questCache);
            PresentViewController(view, DismissPresentedViewController);
        }

        private void LoadQuestListView()
        {
            var prefab = AssetsLoader.GetInstance().LoadGameObject("Assets/Resources/Views/quest_list_sv.prefab");
            _questListView = Instantiate(prefab, GetComponent<RectTransform>()).GetComponent<QuestListView>();
            _questListView.Init();
        }

        // Interface APIs
        public void OnAccomplishQuest(Quest quest) { }

        public void OnCacheReloaded() { }

        public void OnRestoreQuest(Quest quest) { }

        public void OnAddQuest(Quest quest) { }

        public void OnRemoveQuest(Quest quest) { }
    }
}