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
    public abstract class ViewController : MonoBehaviour
    {
        protected ViewController PresentedVc { get; private set; }
        protected ViewController ParentVc { get; private set; }
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private Action _closeCallback;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            TryGetComponent(out _canvasGroup);
        }

        protected void PresentViewController(ViewController vc, Action closeCallback)
        {
            _closeCallback = closeCallback;

            if (PresentedVc == null)
            {
                PresentedVc = vc;
                RectTransform vcRtf = vc.GetComponent<RectTransform>();
                vcRtf.SetParent(_rectTransform);
                vcRtf.localPosition = Vector3.zero;
                vcRtf.localRotation = Quaternion.identity;
                vcRtf.localScale = Vector3.one;
                vcRtf.SetAsLastSibling();
                vcRtf.offsetMin = Vector2.zero;
                vcRtf.offsetMax = Vector2.zero;

                vc.SetParent(this);
                vc.OnPresent();
            }
            else
            {
                Debug.LogError("ViewController: Already Presented a View Controller");
            }
        }

        protected void DismissPresentedViewController()
        {
            if (!PresentedVc)
                return;

            if (PresentedVc.PresentedVc)
            {
                PresentedVc.DismissPresentedViewController();
            }

            PresentedVc = null;
            PresentedVc.SetParent(null);

            PresentedVc.OnDismiss();
        }

        private void SetParent(ViewController vc)
        {
            ParentVc = vc;
        }

        protected void SetViewInteractive(bool enable)
        {
            if (_canvasGroup != null)
            {
                _canvasGroup.interactable = enable;
            }
        }

        protected void Close()
        {
            _closeCallback?.Invoke();
        }

        protected virtual void OnPresent()
        {
            SetViewInteractive(true);
        }

        protected virtual void OnDismiss()
        {
            SetViewInteractive(false);
        }
    }
}