using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWord : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform parentAfterDrag;
    [SerializeField] private TextMeshProUGUI text;

    public void OnBeginDrag(PointerEventData eventData)
    {
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
}
