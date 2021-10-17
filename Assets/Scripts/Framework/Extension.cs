using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Framework
{
    public static class Extension
    {
        public static long GetTimeStamp(this DateTime dataTime)
        {
            var timeSpan = dataTime - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(timeSpan.TotalSeconds);
        }

    }
}