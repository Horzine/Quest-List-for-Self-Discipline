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
        [SerializeField] private Button _close_btn;
        [SerializeField] private RectTransform _panel;

        public void Init(string questId, RectTransform entryRtf, Action<string> onClickEditEntryBtn, Action<string> onClickDeleteEntryBtn, Action onClickCloseBtn)
        {
            _edit_btn.onClick.AddListener(() => onClickEditEntryBtn?.Invoke(questId));
            _delete_btn.onClick.AddListener(() => onClickDeleteEntryBtn?.Invoke(questId));
            _close_btn.onClick.AddListener(() => onClickCloseBtn?.Invoke());

            RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(entryRtf.position), Camera.main, out Vector3 localPosition);
            _panel.position = localPosition;
        }
    }
}