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

        //int index = transform.childCount;
        //Debug.Log("ini gmn");
        //if (this.transform.position.y > 0)
        //{
        //    Debug.Log("masuk posisi atas");
        //    if (transform.childCount < 5)
        //    {
   
        //        Debug.Log("sampe sini1");
        //        TMP_Text[] semuaTeksC = GetComponentsInChildren<TMP_Text>();
        //        foreach (TMP_Text t in semuaTeksC)
        //        {
        //            Debug.Log("Isi teks di C adalah: " + t.text);
        //        }
        //        //int TeksAngkaJadiInt = int.Parse(dropped.GetComponentInChildren<TMP_Text>().text);
        //        //BagianAtasAngkaAngkaYangSudahMasukKeKotakTapiBelumDiCek[index] = TeksAngkaJadiInt;
        //        //index++;
        //    }
        //} else
        //{
        //    Debug.Log("masuk posisi bawah");
        //    if (transform.childCount < 5)
        //    {
        //        GameObject dropped = eventData.pointerDrag;
        //        DraggableItemAngkas draggableItemAngkas = dropped.GetComponent<DraggableItemAngkas>();
        //        draggableItemAngkas.parentAfterDrag = transform;
        //        Debug.Log("sampe sini1");
        //        int TeksAngkaJadiInt = int.Parse(dropped.GetComponentInChildren<TMP_Text>().text);
        //        BagianBawahAngkaAngkaYangSudahMasukKeKotakTapiBelumDiCek[index] = TeksAngkaJadiInt;
        //        index++;


    }
}
