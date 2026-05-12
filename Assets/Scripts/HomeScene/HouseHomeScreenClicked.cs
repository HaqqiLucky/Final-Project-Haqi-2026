using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HouseHomeScreenClicked : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Outline outlineHouse;
    [SerializeField] private GameObject Kereta;
    private int klikKe;
    private bool KeretanyaJalan = false;

    public void OnPointerClick(PointerEventData eventData) // wgen click
    {
        //throw new System.NotImplementedException();
        //Debug.Log("klik ke - " + klikKe);
        klikKe++;
        if (klikKe == 12)
        {
            KeretanyaJalan = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) // hover
    {
        outlineHouse.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outlineHouse.enabled = false;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outlineHouse = GetComponent<Outline>();
        outlineHouse.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (KeretanyaJalan)
        {
            KeretaLaju();
        }
    }

    private void KeretaLaju()
    {
        Kereta.transform.Translate(Vector3.right * 5f * Time.deltaTime);

        if (Kereta.transform.position.x >= 197)
        {
            Destroy(Kereta);
        }
    }
}
