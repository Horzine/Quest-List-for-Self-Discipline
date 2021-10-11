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
            Archive.WriteValue("_i", _i);
            Archive.WriteValue("_b", _b);
            Archive.WriteValue("_s", _s);
            Archive.WriteValue("_f", _f);
        }

        void TestLoad()
        {
            Debug.Log(Archive.ReadValue<int>("_i"));
            Debug.Log(Archive.ReadValue<string>("_s"));
            Debug.Log(Archive.ReadValue<float>("_f"));
            Debug.Log(Archive.ReadValue<bool>("_b"));
        }

    }
}