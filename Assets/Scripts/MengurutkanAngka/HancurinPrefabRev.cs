using UnityEngine;
using UnityEngine.EventSystems;

// TAMBAHKAN IBeginDragHandler di baris ini
public class HancurinPrefabRev : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Batas Geser")]
    public float minX;
    public float maxX;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Vector2 arahTendang = new Vector2(-1f, 0f);
        float kekuatanTendang = 10f;

        TendangObjek(arahTendang, kekuatanTendang);
    }

    private void TendangObjek(Vector2 arah, float kekuatan)
    {
        rb.AddForce(arah.normalized * kekuatan, ForceMode2D.Impulse);
    }

    // FUNGSI BARU: Dipanggil TEPAT saat mouse pertama kali klik & mau geser
    public void OnBeginDrag(PointerEventData eventData)
    {
        // MATIKAN GAYA MELUNCUR: Buat kecepatannya jadi 0 supaya enteng saat digeser
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = transform.position;

        // Jika masih terasa lambat/berat, angka 0.01f ini bisa kamu naikkan (misal ke 0.03f atau 0.05f)
        float deltaWorldX = eventData.delta.x * 0.04f;
        newPos.x += deltaWorldX;

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);

        rb.MovePosition(newPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}