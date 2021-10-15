using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Views.EntryOperation
{
    public class EntryOperationView : MonoBehaviour
    {
        [SerializeField] private CanvasRenderer _canvasRenderer;
        [SerializeField] private Button _edit_btn;
        [SerializeField] private Button _delete_btn;

        public void Init(string questId, Rect rectForClipping, Action<string> onClickEditEntryBtn, Action<string> onClickDeleteEntryBtn)
        {
            _canvasRenderer.EnableRectClipping(rectForClipping);
            _edit_btn.onClick.AddListener(() => onClickEditEntryBtn?.Invoke(questId));
            _delete_btn.onClick.AddListener(() => onClickDeleteEntryBtn?.Invoke(questId));
        }
    }
}