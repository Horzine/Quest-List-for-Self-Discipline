using Cache;
using Handler;
using UnityEngine;
using Views;
using Views.Reward;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Framework
{
    public class GameInitializer : MonoBehaviour
    {
        public static GameInitializer Instance { get; private set; }
        private QuestCache _questCache;
        private RewardHandler _rewardHandler;
        private MainView _mainView;
        private RectTransform _canvas;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;

                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        }

        private void Start()
        {
            LoadData();

            LoadMainView();

            HandleReward();

            InvokeHandleRewardAtTomorrowZero();
        }

        private void LoadData()
        {
            var questConfigHandler = new QuestConfigHandler();
            _questCache = new QuestCache(questConfigHandler);
            _questCache.Reload();
        }

        private void LoadMainView()
        {
            var prefab = AssetsLoader.GetInstance().LoadGameObject("Assets/Resources/Views/main_view.prefab");
            _mainView = Instantiate(prefab, _canvas).GetComponent<MainView>();
            _mainView.Init(_questCache);
        }

        private void HandleReward()
        {
            _rewardHandler = new RewardHandler(_questCache);
            _rewardHandler.SetNotifyRewardCallback((rewardPoint) =>
            {
                var prefab = AssetsLoader.GetInstance().LoadGameObject("Assets/Resources/Views/reward_view.prefab");
                var vc = Instantiate(prefab).GetComponent<RewardViewController>();
                vc.Init(_rewardHandler, rewardPoint);
                _mainView.DismissPresentedViewController();
                _mainView.PresentViewController(vc, _mainView.DismissPresentedViewController);
            });
            _rewardHandler.SetFinishClaimRewardCallback(InvokeHandleRewardAtTomorrowZero);
            _rewardHandler.HandleCurrentRewardState();
        }

        void InvokeHandleRewardAtTomorrowZero()
        {
            Invoke(nameof(HandleReward), RewardHandler.GetSecondsRemainingUntilTomorrow());
        }
    }
}
