using UnityEngine;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Framework
{
    public class AssetsLoader
    {
        private static AssetsLoader _instance = null;

        public static AssetsLoader GetInstance()
        {
            return _instance ??= new AssetsLoader();
        }

        public GameObject LoadGameObject(string fullPath)
        {
            string path = fullPath.Replace("Assets/Resources/", "").Replace(".prefab", "");
            return Resources.Load<GameObject>(path);
        }
    }
}