using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class OnDropDiContain : MonoBehaviour, IDropHandler
{
    //[SerializeField] private List<int> ListNumberYangSudahDimasukanBelumDiCek = new List<int>();
    [SerializeField] private int[] ListNumberYangSudahDimasukanBelumDiCek = new int[10];
    DragnDropKeContainer draganddrop;
    //float randomMentalX = Random.Range(-200)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void OnDrop(PointerEventData eventData)
    {
        TMP_Text Kocak = eventData.pointerDrag.GetComponentInChildren<TMP_Text>();
        int ConvertKeInt = int.Parse(Kocak.text);
        for (int i = 0; i < ListNumberYangSudahDimasukanBelumDiCek.Length; i++)
        {
            if (ListNumberYangSudahDimasukanBelumDiCek[i] != 0)
            {
                ListNumberYangSudahDimasukanBelumDiCek[i] = ConvertKeInt;
                eventData.pointerDrag.transform.SetParent(this.transform, transform);
                break;
            }
        }
        //if (transform.childCount == 0)
        //    {


        //    }
    }
}
