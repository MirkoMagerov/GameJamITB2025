using UnityEngine;
using UnityEngine.EventSystems;

public class WordSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject droppedWord = eventData.pointerDrag;
            DraggableWord draggableWord = droppedWord.GetComponent<DraggableWord>();
            draggableWord.parentAfterDrag = transform;
        }
    }
}
