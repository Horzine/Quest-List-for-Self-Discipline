using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace TestCase
{
    public class TestAssetsLoader : MonoBehaviour
    {
        private void Start()
        {
            var go = AssetsLoader.GetInstance().LoadGameObject("Assets/Resources/Views/MainView.prefab");
            Instantiate(go);
        }



    }
}