using UnityEngine;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace TestCase
{
    public class SimpleTest : MonoBehaviour
    {
        public RectTransform viewPort;
        private void Start()
        {
            GetComponent<CanvasRenderer>().EnableRectClipping(viewPort.rect);
        }

    }
}