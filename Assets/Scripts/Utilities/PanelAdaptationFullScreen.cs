using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Utilities
{
    public class PanelAdaptationFullScreen : MonoBehaviour
    {
        public static Vector2 CalculateOffsetMax(float panelWidth)
        {
            float delta = Screen.height - Screen.safeArea.yMax;
            return new Vector2(0, -(delta * panelWidth / Screen.width));
        }
    }
}