using Cache;
using DataStructure;
using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views.AddQuset;
using Views.EntryOperation;
using Views.QuestList;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Views
{
    public interface IMainViewProtocol
    {
        void ShowEntryOperationView(string questId, RectTransform entryViewRtf);
    }
    public class MainView : ViewController, IMainViewProtocol, IQuestCacheObserver
    {
        [SerializeField] private TextMeshProUGUI _motto_txt;
        [SerializeField] private Button _addQuest_btn;
        [SerializeField] private TextMeshProUGUI _addQuest_txt;

        private QuestCache _questCache;
        private QuestListView _questListView;
        private EntryOperationView _entryOperationView;
        private GameObject _addQuestViewPrefab;
        private GameObject _questListViewPrefab;
        private GameObject _entryOperationViewPrefab;


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
            if (!_addQuestViewPrefab)
                _addQuestViewPrefab = AssetsLoader.GetInstance().LoadGameObject("Assets/Resources/Views/add_quest_view.prefab");

            var view = Instantiate(_addQuestViewPrefab).GetComponent<AddQuestView>();
            view.Init(_questCache);
            PresentViewController(view, DismissPresentedViewController);
        }

        private void LoadQuestListView()
        {
            if (!_questListViewPrefab)
                _questListViewPrefab = AssetsLoader.GetInstance().LoadGameObject("Assets/Resources/Views/quest_list_sv.prefab");

            _questListView = Instantiate(_questListViewPrefab, GetComponent<RectTransform>()).GetComponent<QuestListView>();
            _questListView.Init(_questCache, _questCache.GetAllQuest(), this);
        }

        private void OnClickDeleteQuestBtn(string questId)
        {
            Debug.Log("On Click Delete Quest Btn");
        }

        private void OnClickEditQuestBtn(string questId)
        {
            Debug.Log("On Click Edit Quest Btn");
        }

        // Interface APIs
        public void OnAccomplishQuest(Quest quest) { }

        public void OnCacheReloaded() { }

        public void OnRestoreQuest(Quest quest) { }

        public void OnAddQuest(Quest quest) { }

        public void OnRemoveQuest(Quest quest) { }

        public void ShowEntryOperationView(string questId, RectTransform entryViewRtf)
        {
            if (!_entryOperationViewPrefab)
                _entryOperationViewPrefab = AssetsLoader.GetInstance().LoadGameObject("Assets/Resources/Views/entry_operation_panel.prefab");

            _entryOperationView = Instantiate(_entryOperationViewPrefab, entryViewRtf).GetComponent<EntryOperationView>();
            _entryOperationView.Init(questId, _questListView.GetViewPortRect(), OnClickEditQuestBtn, OnClickDeleteQuestBtn);
        }
    }
}