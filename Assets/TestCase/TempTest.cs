using System.Collections;
using System.Collections.Generic;
using DataStructure;
using Newtonsoft.Json;
using UnityEngine;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace TestCase
{
    public class TempTest : MonoBehaviour
    {
        private void Start()
        {
            var result = JsonConvert.DeserializeObject<List<Quest>>(null);
            // Debug.Log(result.Count); //null;

            var cor = StartCoroutine(TestCoroutine());
            // cor = null;
            StopCoroutine(cor);
        }


        public IEnumerator TestCoroutine()
        {
            yield break;
        }
    }
}