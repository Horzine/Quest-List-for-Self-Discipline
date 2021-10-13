using Cache;
using Handler;
using UnityEngine;
using Views;

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
            var view = Instantiate(prefab, _canvas).GetComponent<MainView>();
            view.Init(_questCache);
        }
    }
}
