using System;
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
    public class TestPlayerPerfsHandler : MonoBehaviour
    {
        public int _i = 19;
        public bool _b = true;
        public string _s = "str";
        public float _f = 6.9f;

        private void Start()
        {
            TestWrite();

            TestLoad();
        }

        void TestWrite()
        {
            PlayerPrefsHandler.WriteValue("_i", _i);
            PlayerPrefsHandler.WriteValue("_b", _b);
            PlayerPrefsHandler.WriteValue("_s", _s);
            PlayerPrefsHandler.WriteValue("_f", _f);
        }

        void TestLoad()
        {
            Debug.Log(PlayerPrefsHandler.ReadValue<int>("_i"));
            Debug.Log(PlayerPrefsHandler.ReadValue<string>("_s"));
            Debug.Log(PlayerPrefsHandler.ReadValue<float>("_f"));
            Debug.Log(PlayerPrefsHandler.ReadValue<bool>("_b"));
        }

    }
}