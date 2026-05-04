using UnityEngine;
using UnityEngine.EventSystems;

public class BasketDragged : MonoBehaviour, IDragHandler
{
    private RectTransform BasketPos;
    [SerializeField]private Canvas canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BasketPos = GetComponent<RectTransform>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos = BasketPos.anchoredPosition;
        currentPos.x += eventData.delta.x / canvas.scaleFactor;
        currentPos.x = Mathf.Clamp(currentPos.x, -858f, 213.955f); // ngikut rect transform pos x y

        BasketPos.anchoredPosition = currentPos;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
