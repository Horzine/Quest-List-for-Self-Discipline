using System.Collections.Generic;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Caches
{
    public interface ICacheObserver
    {
        void OnCacheChanged(Cache cache);
    }
    public abstract class Cache
    {
        private readonly List<ICacheObserver> _cacheObservers = new List<ICacheObserver>();

        public void AddObserver(ICacheObserver observer, bool addOnly = false)
        {
            var ob = _cacheObservers.Find(o => o == observer);
            if (ob == null)
            {
                _cacheObservers.Add(observer);
                if (!addOnly)
                {
                    observer.OnCacheChanged(this);
                }
            }
        }

        public void RemoveObserver(ICacheObserver observer)
        {
            _cacheObservers.Remove(observer);
        }

        protected void NotifyCacheObserver()
        {
            var cacheObsCopy = _cacheObservers.GetRange(0, _cacheObservers.Count);
            foreach (var ob in cacheObsCopy)
            {
                ob.OnCacheChanged(this);
            }
        }
    }
}