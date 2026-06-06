using UnityEngine;
using UnityEngine.EventSystems;

// TAMBAHKAN IBeginDragHandler di baris ini
public class HancurinPrefabRev : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Batas Geser")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //private void Update()
    //{
    //    transform.position = Vector2.Lerp(transform.position, new Vector2(Mathf.Clamp(targetPos.x, -5f, 5f), Mathf.Clamp(targetPos.y, -3f, 3f)), 10f * Time.deltaTime);
    //}

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
        // Menggunakan posisi dari eventData (Aman untuk New Input System)
        Vector3 posisiScreen = new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(posisiScreen);

        // 1. Ambil X dari mouse sebagai target langsung
        float targetX = mousePos.x;

        // 2. Batasi (Clamp) targetnya
        targetX = Mathf.Clamp(targetX, minX, maxX);

        // 3. Gunakan Lerp dari posisi sekarang ke posisi mouse yang sudah dibatasi
        Vector2 newPos = rb.position;
        newPos.x = Mathf.Lerp(newPos.x, targetX, 0.2f); // Naikkan ke 0.4f atau 0.5f jika terasa kurang cepat mengejar mouse

        rb.MovePosition(newPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}