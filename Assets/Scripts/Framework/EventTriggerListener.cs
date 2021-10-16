using System;
using UnityEngine;
using UnityEngine.EventSystems;

/*
  ┎━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┒
  ┃   Dedication Focus Discipline   ┃
  ┃        Practice more !!!        ┃
  ┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛
*/
namespace Framework
{
    public class EventTriggerListener : EventTrigger
    {
        public event Action<GameObject, PointerEventData> PointerClickEvent;
        public event Action<GameObject, PointerEventData> PointerDownEvent;
        public event Action<GameObject, PointerEventData> PointerEnterEvent;
        public event Action<GameObject, PointerEventData> PointerExitEvent;
        public event Action<GameObject, PointerEventData> PointerUpEvent;
        public event Action<GameObject, PointerEventData> BeginDragEvent;
        public event Action<GameObject, PointerEventData> DragEvent;
        public event Action<GameObject, PointerEventData> DropEvent;
        public event Action<GameObject, PointerEventData> EndDragEvent;
        public event Action<GameObject, PointerEventData> InitializePotentialDragEvent;
        public event Action<GameObject, PointerEventData> ScrollEvent;
        public event Action<GameObject, BaseEventData> SelectEvent;
        public event Action<GameObject, BaseEventData> UpdateSelectedEvent;
        public event Action<GameObject, BaseEventData> CancelEvent;
        public event Action<GameObject, BaseEventData> DeselectEvent;
        public event Action<GameObject, BaseEventData> SubmitEvent;
        public event Action<GameObject, AxisEventData> MoveEvent;


        public static EventTriggerListener Get(GameObject go)
        {
            var listener = go.GetComponent<EventTriggerListener>();
            if (listener == null)
                listener = go.AddComponent<EventTriggerListener>();
            return listener;
        }

        public static EventTriggerListener Get(Transform transform)
        {
            var listener = transform.GetComponent<EventTriggerListener>();
            if (listener == null)
                listener = transform.gameObject.AddComponent<EventTriggerListener>();
            return listener;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            PointerClickEvent?.Invoke(gameObject, eventData);
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            PointerDownEvent?.Invoke(gameObject, eventData);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnterEvent?.Invoke(gameObject, eventData);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            PointerExitEvent?.Invoke(gameObject, eventData);
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            PointerUpEvent?.Invoke(gameObject, eventData);
        }
        public override void OnBeginDrag(PointerEventData eventData)
        {
            BeginDragEvent?.Invoke(gameObject, eventData);
        }
        public override void OnDrag(PointerEventData eventData)
        {
            DragEvent?.Invoke(gameObject, eventData);
        }
        public override void OnDrop(PointerEventData eventData)
        {
            DropEvent?.Invoke(gameObject, eventData);
        }
        public override void OnEndDrag(PointerEventData eventData)
        {
            EndDragEvent?.Invoke(gameObject, eventData);
        }
        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            InitializePotentialDragEvent?.Invoke(gameObject, eventData);
        }
        public override void OnScroll(PointerEventData eventData)
        {
            ScrollEvent?.Invoke(gameObject, eventData);
        }
        public override void OnSelect(BaseEventData eventData)
        {
            SelectEvent?.Invoke(gameObject, eventData);
        }
        public override void OnUpdateSelected(BaseEventData eventData)
        {
            UpdateSelectedEvent?.Invoke(gameObject, eventData);
        }
        public override void OnCancel(BaseEventData eventData)
        {
            CancelEvent?.Invoke(gameObject, eventData);
        }
        public override void OnDeselect(BaseEventData eventData)
        {
            DeselectEvent?.Invoke(gameObject, eventData);
        }
        public override void OnSubmit(BaseEventData eventData)
        {
            SubmitEvent?.Invoke(gameObject, eventData);
        }
        public override void OnMove(AxisEventData eventData)
        {
            MoveEvent?.Invoke(gameObject, eventData);
        }
    }
}
