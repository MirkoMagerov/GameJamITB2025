using UnityEngine;
using UnityEngine.EventSystems;

public class WordSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedWord = eventData.pointerDrag;
        DraggableWord draggableWord = droppedWord.GetComponent<DraggableWord>();
        if (transform.childCount > 0)
        {
            GameObject existingWord = transform.GetChild(0).gameObject;
            DraggableWord existingDraggable = existingWord.GetComponent<DraggableWord>();

            Transform draggableOriginalParent = draggableWord.GetOriginalParent();

            existingWord.transform.SetParent(draggableOriginalParent);
            existingWord.transform.localPosition = Vector3.zero;
            existingDraggable.parentAfterDrag = draggableOriginalParent;
        }
        draggableWord.parentAfterDrag = transform;
    }
}
