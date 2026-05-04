using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaloonHomeSceneClicked : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Outline outlineBalon;
    private int klikKe;
    public void OnPointerClick(PointerEventData eventData) // wgen click
    {
        klikKe++;
    }

    public void OnPointerEnter(PointerEventData eventData) // hover
    {
        outlineBalon.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outlineBalon.enabled = false;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outlineBalon = GetComponent<Outline>();
        outlineBalon.enabled = false;
        klikKe = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
