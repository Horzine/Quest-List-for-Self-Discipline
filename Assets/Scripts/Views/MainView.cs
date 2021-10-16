using Cache;
using DataStructure;
using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views.AddQuset;
using Views.EditQuest;
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
        [SerializeField] private Button _reload_btn;
        [SerializeField] private TextMeshProUGUI _reload_txt;

        private QuestCache _questCache;
        private QuestListView _questListView;
        private EntryOperationView _entryOperationView;
        private GameObject _addQuestViewPrefab;
        private GameObject _questListViewPrefab;
        private GameObject _entryOperationViewPrefab;
        private GameObject _editQuestViewPrefab;

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
            _reload_txt.text = "Reload";

            _addQuest_btn.onClick.AddListener(OnClickAddQuestBtn);
            _reload_btn.onClick.AddListener(_questCache.Reload);
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
            _questCache.RemoveQeust(questId);

            OnClickOperationViewCloseBtn();
        }

        private void OnClickEditQuestBtn(string questId)
        {
            OnClickOperationViewCloseBtn();

            if (!_editQuestViewPrefab)
                _editQuestViewPrefab = AssetsLoader.GetInstance().LoadGameObject("Assets/Resources/Views/edit_quest_view.prefab");

            var view = Instantiate(_editQuestViewPrefab).GetComponent<EditQuestView>();
            view.Init(_questCache.GetQuest(questId), _questCache);
            PresentViewController(view, DismissPresentedViewController);
        }

        private void OnClickOperationViewCloseBtn()
        {
            Destroy(_entryOperationView.gameObject);
            _entryOperationView = null;
        }

        // Interface APIs
        public void OnAccomplishQuest(Quest quest) { }

        public void OnCacheReloaded()
        {
            if (_questListView != null)
            {
                Destroy(_questListView.gameObject);
            }
            LoadQuestListView();
        }

        public void OnRestoreQuest(Quest quest) { }

        public void OnAddQuest(Quest quest) { }

        public void OnRemoveQuest(Quest quest) { }

        public void OnEditQuest(Quest quest) { }

        public void ShowEntryOperationView(string questId, RectTransform entryViewRtf)
        {
            if (!_entryOperationViewPrefab)
                _entryOperationViewPrefab = AssetsLoader.GetInstance().LoadGameObject("Assets/Resources/Views/entry_operation_panel.prefab");

            _entryOperationView = Instantiate(_entryOperationViewPrefab, GetComponent<RectTransform>()).GetComponent<EntryOperationView>();
            _entryOperationView.Init(questId, entryViewRtf, OnClickEditQuestBtn, OnClickDeleteQuestBtn, OnClickOperationViewCloseBtn);
        }
    }
}