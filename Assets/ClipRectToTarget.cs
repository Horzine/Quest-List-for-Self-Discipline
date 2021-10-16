using UnityEngine;

public class ClipRectToTarget : MonoBehaviour
{
    [SerializeField] private RectTransform target;
    public RectTransform Target
    {
        get => target;
        private set
        {
            target = value;
            if (enabled)
            {
                SetTargetClippingRect();
                currentPos = lastPos = target.position;
            }
        }
    }

    private CanvasRenderer canvasRenderer;
    private Vector2 lastPos;
    private Vector2 currentPos;

    private void Awake()
    {
        canvasRenderer = GetComponent<CanvasRenderer>();
        if (target != null)
        {
            currentPos = lastPos = target.position;
        }
    }

    private void Update()
    {
        if (target == null) return;

        currentPos = target.position;
        if (currentPos != lastPos)
        {
            SetTargetClippingRect();
        }
        lastPos = currentPos;
    }

    private void OnEnable()
    {
        if (target != null)
        {
            SetTargetClippingRect();
        }
    }

    private void OnDisable()
    {
        canvasRenderer.DisableRectClipping();
    }

    private void SetTargetClippingRect()
    {
        var rect = target.rect;
        Vector2 offset = target.localPosition;
        Transform parent = target.parent;
        while (parent.GetComponent<Canvas>() == null || !parent.GetComponent<Canvas>().isRootCanvas)
        {
            Debug.Log($"{parent.name},local: {(Vector2)parent.localPosition}, Offset: {offset}");
            offset += (Vector2)parent.localPosition;
            parent = parent.parent;

        }
        rect.x += offset.x;
        rect.y += offset.y;
        canvasRenderer.EnableRectClipping(rect);
    }
}