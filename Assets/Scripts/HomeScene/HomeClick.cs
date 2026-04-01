using UnityEngine;
using UnityEngine.EventSystems;

public class HomeClick : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerEnterHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("klick");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        Debug.Log("enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
    }
}
