using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWord : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform parentAfterDrag;
    [SerializeField] private TextMeshProUGUI text;
    private Vector3 originalPosition;
    private Transform originalParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = transform.localPosition;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        text.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        text.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        transform.localPosition = Vector3.zero;
    }

    public void SetOriginalParent(Transform parent)
    {
        originalParent = parent;
    }

    public Transform GetOriginalParent()
    {
        return originalParent;
    }
}
