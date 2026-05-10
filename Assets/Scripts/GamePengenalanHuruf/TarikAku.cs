using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI.Extensions;

public class TarikAku : MonoBehaviour
{
    private UILineRenderer lineRenderer;
    private Vector2 startPoint;
    private bool isDragging = false;
    private RectTransform canvasRect;

    private void Awake()
    {
        lineRenderer = GetComponent<UILineRenderer>();
        canvasRect = transform.parent as RectTransform;
        lineRenderer.Points = new Vector2[0];
    }


    private void StartDraggingTheLine(Vector2 screenPos)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out startPoint))
        {
            isDragging = true;
            lineRenderer.Points = new Vector2[2] { startPoint, startPoint };// ini apa ya
        }
    }

    private void UpdateLine(Vector2 screenPos)
    {
        Vector2 currentMousePos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out currentMousePos))
        {
            Vector2[] newPoints = new Vector2[2];
            newPoints[0] = startPoint;
            newPoints[1] = currentMousePos;

            lineRenderer.Points = newPoints;
            lineRenderer.SetAllDirty();
        }
    }

    private void StopDraggingTheLine()
    {
        isDragging = false;
        lineRenderer.Points = new Vector2[0];
    }


    // Update is called once per frame
    void Update()
    {
        var pointer = Pointer.current;
        if (pointer == null) return;
        if (pointer.press.wasPressedThisFrame)
        {
            StartDraggingTheLine(pointer.position.ReadValue());
        }
        if (isDragging)
        {
            UpdateLine(pointer.position.ReadValue());
        }
        if (pointer.press.wasReleasedThisFrame)
        {
            StopDraggingTheLine();
        }
    }
}
