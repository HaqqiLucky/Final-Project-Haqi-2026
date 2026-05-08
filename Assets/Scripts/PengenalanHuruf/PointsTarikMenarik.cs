using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Radishmouse;
using UnityEngine.InputSystem; // Tambahkan ini!

public class PointsTarikMenarik : MonoBehaviour
{
    private UILineRenderer line;
    public string idWarna;
    private Vector2 posisiAwal;
    private bool isLocked = false;

    void Start()
    {
        line = GetComponent<UILineRenderer>();
        if (line.points.Length >= 1) posisiAwal = line.points[0];
    }

    void Update()
    {
        if (isLocked) return;

        // Mouse.current.leftButton.isPressed menggantikan Input.GetMouseButton(0)
        if (Mouse.current.leftButton.isPressed)
        {
            TarikKabelKeMouse();
        }

        // Mouse.current.leftButton.wasReleasedThisFrame menggantikan Input.GetMouseButtonUp(0)
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            CekKoneksi();
        }
    }

    void TarikKabelKeMouse()
    {
        // Mouse.current.position.ReadValue() menggantikan Input.mousePosition
        Vector2 mousePos = Mouse.current.position.ReadValue();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent as RectTransform,
            mousePos,
            null,
            out Vector2 localPos);

        line.points[1] = localPos;
        line.SetAllDirty();
    }

    void CekKoneksi()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        bool bener = false;
        foreach (var r in results)
        {
            PenandaBuletan socket = r.gameObject.GetComponent<PenandaBuletan>();
            if (socket != null && socket.idKabel == idWarna && !socket.isOutput)
            {
                bener = true;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    transform.parent as RectTransform,
                    socket.transform.position,
                    null,
                    out Vector2 socketPos);

                line.points[1] = socketPos;
                isLocked = true;
                break;
            }
        }

        if (!bener)
        {
            line.points[1] = posisiAwal;
        }
        line.SetAllDirty();
    }
}