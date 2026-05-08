using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.EventSystems; // Perlu ini untuk deteksi input UI

// Tambahkan Interface IPointerDownHandler & IDragHandler agar bisa mendeteksi klik/drag
public class CekUILineRenderer : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private UILineRenderer lineRender;
    private RectTransform myRectTransform;

    // Masukkan RectTransform lingkaran (titik awal) di Inspector
    public RectTransform circleStart;

    // Canvas utama untuk konversi koordinat mouse
    private Canvas mainCanvas;

    void Start()
    {
        lineRender = GetComponent<UILineRenderer>();
        myRectTransform = GetComponent<RectTransform>();

        // Cari Canvas induk
        mainCanvas = GetComponentInParent<Canvas>();

        // Pastikan Line Renderer di posisi 0,0,0 sesuai aturan awal
        myRectTransform.anchoredPosition = Vector2.zero;

        // Inisialisasi 2 titik di posisi awal lingkaran
        Vector2 startPos = circleStart.anchoredPosition;
        lineRender.Points = new Vector2[] { startPos, startPos };
    }

    // Fungsi ini dipanggil saat pertama kali lingkaran diklik
    public void OnPointerDown(PointerEventData eventData)
    {
        UpdatePointKePosisiMouse(eventData);
    }

    // Fungsi ini dipanggil terus-menerus selama mouse ditekan sambil digerakkan
    public void OnDrag(PointerEventData eventData)
    {
        UpdatePointKePosisiMouse(eventData);
    }

    private void UpdatePointKePosisiMouse(PointerEventData eventData)
    {
        if (mainCanvas == null) return;

        Vector2 mouseLocalPos;

        // --- Langkah Krusial: Konversi Posisi Mouse ke Koordinat Lokal Canvas ---
        // Kita mengubah posisi mouse di layar (screen space) ke posisi relatif terhadap 
        // RectTransform LineRenderer kita (local space).
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            myRectTransform,
            eventData.position, // Posisi mouse di layar
            eventData.pressEventCamera, // Kamera UI
            out mouseLocalPos // Hasil konversi disimpan di sini
        );

        // --- Update Titik Garis ---
        Vector2[] points = lineRender.Points;

        // Titik 0 (Awal) tetap di lingkaran
        points[0] = circleStart.anchoredPosition;

        // Titik 1 (Akhir) mengikuti konversi posisi mouse tadi
        points[1] = mouseLocalPos;

        // Terapkan kembali dan refresh visual
        lineRender.Points = points;
        lineRender.SetAllDirty(); // WAJIB
    }
}