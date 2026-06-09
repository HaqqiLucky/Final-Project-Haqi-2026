using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotGridAtasKananKiriBawah : MonoBehaviour, IDropHandler
{
    [SerializeField] private int[] BagianAtasAngkaAngkaYangSudahMasukKeKotakTapiBelumDiCek = new int[5];
    [SerializeField] private int[] BagianBawahAngkaAngkaYangSudahMasukKeKotakTapiBelumDiCek = new int[5];
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount < 5)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItemAngkas draggableItemAngkas = dropped.GetComponent<DraggableItemAngkas>();
            draggableItemAngkas.parentAfterDrag = transform;
        }

    }
}
