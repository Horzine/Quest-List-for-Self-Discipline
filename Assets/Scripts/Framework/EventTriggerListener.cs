using System.Collections;
using System.Collections.Generic;
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
        public delegate void VoidDelegate(GameObject go);
        public delegate void BoolDelegate(GameObject go, bool state);
        public delegate void FloatDelegate(GameObject go, float delta);
        public delegate void VectorDelegate(GameObject go, Vector2 delta);
        public delegate void ObjectDelegate(GameObject go, GameObject obj);
        public delegate void KeyCodeDelegate(GameObject go, KeyCode key);

        public VoidDelegate onClick;
        public VoidDelegate onDown;
        public VoidDelegate onEnter;
        public VoidDelegate onExit;
        public VoidDelegate onUp;
        public VoidDelegate onSelect;
        public VoidDelegate onUpdateSelect;

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
            onClick?.Invoke(gameObject);
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            onDown?.Invoke(gameObject);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            onEnter?.Invoke(gameObject);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            onExit?.Invoke(gameObject);
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            onUp?.Invoke(gameObject);
        }
        public override void OnSelect(BaseEventData eventData)
        {
            onSelect?.Invoke(gameObject);
        }
        public override void OnUpdateSelected(BaseEventData eventData)
        {
            onUpdateSelect?.Invoke(gameObject);
        }

    }
}
