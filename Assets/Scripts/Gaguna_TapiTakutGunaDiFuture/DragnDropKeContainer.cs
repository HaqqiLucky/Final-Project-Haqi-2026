using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragnDropKeContainer : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image image;
    Transform ParentAfterDrag;
    [SerializeField] private int gridSize;
    //[SerializeField] private GameObject Container;
    public Vector3 PosisiAwal;

    private void Start()
    {
        PosisiAwal = transform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData) // begin dragging
    {
        //Debug.Log("begin draging");
        ParentAfterDrag = transform.parent;
        image.raycastTarget = false;    
        //transform.SetParent(transform.root);
        //transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData) //while draging
    {
        //Debug.Log("while draging");
        transform.position = eventData.position;
    }

 

    public void OnEndDrag(PointerEventData eventData) //dragged
    {
        //Debug.Log("end draging");
        transform.position = new Vector2(Mathf.Round(transform.position.x / gridSize) * gridSize, Mathf.Round(transform.position.y / gridSize) * gridSize);
        //transform.SetParent(ParentAfterDrag);
        image.raycastTarget = true;
    }

}
