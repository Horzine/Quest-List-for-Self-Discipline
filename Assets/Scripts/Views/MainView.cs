using Caches;
using Clients;
using Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Views.AddQuset;
using Views.EditQuest;
using Views.EntryOperation;
using Views.QuestList;
using Cache = Caches.Cache;

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
    public class MainView : ViewController, IMainViewProtocol, ICacheObserver
    {
        [SerializeField] private RectTransform _panel;
        [SerializeField] private TextMeshProUGUI _motto_txt;
        [SerializeField] private Button _addQuest_btn;
        [SerializeField] private TextMeshProUGUI _addQuest_txt;
        [SerializeField] private Button _reload_btn;
        [SerializeField] private TextMeshProUGUI _reload_txt;
        [SerializeField] private Button _deleteAll_btn;
        [SerializeField] private TextMeshProUGUI _deleteAll_txt;

        private QuestCache _questCache;
        private QuestClient _questClient;
        private QuestListView _questListView;
        private EntryOperationView _entryOperationView;
        private GameObject _addQuestViewPrefab;
        private GameObject _questListViewPrefab;
        private GameObject _entryOperationViewPrefab;
        private GameObject _editQuestViewPrefab;

        public void Init(QuestCache questCache, QuestClient questClient)
        {
            _questClient = questClient;
            _questCache = questCache;
            _questCache.AddObserver(this);

            SetupView();

            _panel.offsetMax = PanelAdaptationFullScreen.CalculateOffsetMax(_panel.rect.width);
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
            _deleteAll_txt.text = "Delete All";

            _addQuest_btn.onClick.AddListener(OnClickAddQuestBtn);

            _reload_btn.gameObject.SetActive(Constants.DebugMode);
            _reload_btn.onClick.AddListener(OnClickReloadBtn);

            _deleteAll_btn.gameObject.SetActive(Constants.DebugMode);
            _deleteAll_btn.onClick.AddListener(OnClickDeleteAllBtn);
        }

        private void OnClickAddQuestBtn()
        {
            if (!_addQuestViewPrefab)
                _addQuestViewPrefab = AssetsLoader.GetInstance().LoadGameObject("Assets/Resources/Views/add_quest_view.prefab");

            var view = Instantiate(_addQuestViewPrefab).GetComponent<AddQuestView>();
            view.Init(_questCache, _questClient);
            PresentViewController(view, DismissPresentedViewController);
        }

        private void LoadQuestListView()
        {
            if (!_questListViewPrefab)
                _questListViewPrefab = AssetsLoader.GetInstance().LoadGameObject("Assets/Resources/Views/quest_list_sv.prefab");

            _questListView = Instantiate(_questListViewPrefab, _panel).GetComponent<QuestListView>();
            _questListView.Init(_questCache, _questClient, _questCache.GetAllQuest(), this);
        }

        private void OnClickDeleteQuestBtn(string questId)
        {
            _questClient.RemoveQuest(questId);

            OnClickOperationViewCloseBtn();
        }

        private void OnClickEditQuestBtn(string questId)
        {
            OnClickOperationViewCloseBtn();

            if (!_editQuestViewPrefab)
                _editQuestViewPrefab = AssetsLoader.GetInstance().LoadGameObject("Assets/Resources/Views/edit_quest_view.prefab");

            var view = Instantiate(_editQuestViewPrefab).GetComponent<EditQuestView>();
            view.Init(_questCache.GetQuest(questId), _questClient);
            PresentViewController(view, DismissPresentedViewController);
        }

        private void OnClickOperationViewCloseBtn()
        {
            Destroy(_entryOperationView.gameObject);
            _entryOperationView = null;
        }

        private void OnClickDeleteAllBtn()
        {
            Archive.DeleteAllKeys();

            OnClickReloadBtn();
        }

        private void OnClickReloadBtn()
        {
            _questClient.FetchAllQuest();
        }

        // Interface APIs
        public void OnCacheChanged(Cache cache)
        {
            if (cache is QuestCache questCache)
            {
                if (_questListView != null)
                {
                    Destroy(_questListView.gameObject);
                    _questListView = null;
                }
                LoadQuestListView();
            }
        }

        public void ShowEntryOperationView(string questId, RectTransform entryViewRtf)
        {
            if (!_entryOperationViewPrefab)
                _entryOperationViewPrefab = AssetsLoader.GetInstance().LoadGameObject("Assets/Resources/Views/entry_operation_panel.prefab");

            _entryOperationView = Instantiate(_entryOperationViewPrefab, GetComponent<RectTransform>()).GetComponent<EntryOperationView>();
            _entryOperationView.Init(questId, entryViewRtf, OnClickEditQuestBtn, OnClickDeleteQuestBtn, OnClickOperationViewCloseBtn);
        }
    }
}