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
        public event Action<GameObject, PointerEventData> _onPointerClick;
        public event Action<GameObject, PointerEventData> _onPointerDown;
        public event Action<GameObject, PointerEventData> _onPointerEnter;
        public event Action<GameObject, PointerEventData> _onPointerExit;
        public event Action<GameObject, PointerEventData> _onPointerUp;
        public event Action<GameObject, PointerEventData> _onBeginDrag;
        public event Action<GameObject, PointerEventData> _onDrag;
        public event Action<GameObject, PointerEventData> _onDrop;
        public event Action<GameObject, PointerEventData> _onEndDrag;
        public event Action<GameObject, PointerEventData> _onInitializePotentialDrag;
        public event Action<GameObject, PointerEventData> _onScroll;
        public event Action<GameObject, BaseEventData> _onSelect;
        public event Action<GameObject, BaseEventData> _onUpdateSelected;
        public event Action<GameObject, BaseEventData> _onCancel;
        public event Action<GameObject, BaseEventData> _onDeselect;
        public event Action<GameObject, BaseEventData> _onSubmit;
        public event Action<GameObject, AxisEventData> _onMove;


        static public EventTriggerListener Get(GameObject go)
        {
            var listener = go.GetComponent<EventTriggerListener>();
            if (listener == null)
                listener = go.AddComponent<EventTriggerListener>();
            return listener;
        }

        static public EventTriggerListener Get(Transform transform)
        {
            var listener = transform.GetComponent<EventTriggerListener>();
            if (listener == null)
                listener = transform.gameObject.AddComponent<EventTriggerListener>();
            return listener;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            _onPointerClick?.Invoke(gameObject, eventData);
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            _onPointerDown?.Invoke(gameObject, eventData);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            _onPointerEnter?.Invoke(gameObject, eventData);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            _onPointerExit?.Invoke(gameObject, eventData);
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            _onPointerUp?.Invoke(gameObject, eventData);
        }
        public override void OnBeginDrag(PointerEventData eventData)
        {
            _onBeginDrag?.Invoke(gameObject, eventData);
        }
        public override void OnDrag(PointerEventData eventData)
        {
            _onDrag?.Invoke(gameObject, eventData);
        }
        public override void OnDrop(PointerEventData eventData)
        {
            _onDrop?.Invoke(gameObject, eventData);
        }
        public override void OnEndDrag(PointerEventData eventData)
        {
            _onEndDrag?.Invoke(gameObject, eventData);
        }
        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            _onInitializePotentialDrag?.Invoke(gameObject, eventData);
        }
        public override void OnScroll(PointerEventData eventData)
        {
            _onScroll?.Invoke(gameObject, eventData);
        }
        public override void OnSelect(BaseEventData eventData)
        {
            _onSelect?.Invoke(gameObject, eventData);
        }
        public override void OnUpdateSelected(BaseEventData eventData)
        {
            _onUpdateSelected?.Invoke(gameObject, eventData);
        }
        public override void OnCancel(BaseEventData eventData)
        {
            _onCancel?.Invoke(gameObject, eventData);
        }
        public override void OnDeselect(BaseEventData eventData)
        {
            _onDeselect?.Invoke(gameObject, eventData);
        }
        public override void OnSubmit(BaseEventData eventData)
        {
            _onSubmit?.Invoke(gameObject, eventData);
        }
        public override void OnMove(AxisEventData eventData)
        {
            _onMove?.Invoke(gameObject, eventData);
        }
    }
}
