using Unity.VisualScripting;
using UnityEngine;

public class ClickableItemBaloon : MonoBehaviour
{
    public GameObject[] BaloonColor;
    private int number;

    public void Balon(int id)
    {
        number = id;
        Debug.Log("Kamu memilih balon nomor: " + number);
        
        for (int i = 0; i < BaloonColor.Length; i++)
        {
            BaloonColor[i].SetActive(false);
        }
        BaloonColor[id].SetActive(true);
    }
    
}
