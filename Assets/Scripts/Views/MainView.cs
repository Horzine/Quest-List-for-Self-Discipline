using System.Collections;
using System.Collections.Generic;
using Cache;
using DataStructure;
using UnityEngine;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Views
{
    public interface IMainViewProtocol { }
    public class MainView : MonoBehaviour, IMainViewProtocol, IQuestCacheObserver
    {
        private QuestCache _questCache;

        public void Init(QuestCache questCache)
        {
            _questCache = questCache;
        }

        // Interface APIs
        public void OnAccomplishQuest(Quest quest) { }

        public void OnCacheReloaded() { }

        public void OnRestoreQuest(Quest quest) { }
    }
}