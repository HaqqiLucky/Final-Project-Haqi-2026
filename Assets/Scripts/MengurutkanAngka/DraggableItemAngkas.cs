using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;
using System;

public class DraggableItemAngkas : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    public TextMeshProUGUI tmpro;
    //public UnityEvent OnDropEndToCheckTheCurrentWaves;
    public static Action OnDropEndToCheckTheCurrentWaves;
    //[SerializeField] MengurutkanAngkaSceneControl sceneControl;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Start drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        //GetComponent<CanvasGroup>().blocksRaycasts = false;
        tmpro.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) // yg ini kode berubah dan aku ga paham soalnya pake ai lngsng
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        //Debug.Log("while drag");
        //transform.position = eventData.position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 movePos
        );
        transform.localPosition = new Vector3(movePos.x, movePos.y, 0f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("end drag");
        transform.SetParent(parentAfterDrag);
        //GetComponent<CanvasGroup>().blocksRaycasts = true;
        tmpro.raycastTarget = true;
        //sceneControl.PengecekanRutinKotaks();
        OnDropEndToCheckTheCurrentWaves?.Invoke();
    }
}
