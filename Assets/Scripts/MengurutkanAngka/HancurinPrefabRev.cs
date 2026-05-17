using UnityEngine;
using UnityEngine.EventSystems;

public class HancurinPrefabRev : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [Header("Batas Geser")]
    public float minX;
    public float maxX;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //this.gameObject.SetActive(false);   


        Vector2 arahTendang = new Vector2(-1f, 0f);
        float kekuatanTendang = 5f;

        // PANGGIL FUNGSINYA DI SINI
        TendangObjek(arahTendang, kekuatanTendang);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TendangObjek(Vector2 arah, float kekuatan)
    {
        // ForceMode2D.Impulse memberikan seluruh kekuatan secara instan di awal (seperti ditendang)
        rb.AddForce(arah.normalized * kekuatan, ForceMode2D.Impulse);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 1. Hitung target posisi baru (Gunakan posisi global/world jika memakai Rigidbody)
        Vector3 newPos = transform.position;

        // Ambil delta pergerakan mouse dalam koordinat dunia (World Space)
        // Ini jauh lebih aman untuk Rigidbody dibanding memakai localPosition
        float deltaWorldX = eventData.delta.x * 0.01f; // Sesuaikan angka pengali ini dengan scale canvas-mu
        newPos.x += deltaWorldX;

        // 2. Batasi nilainya agar tidak keluar batas
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);

        // 3. KUNCI UTAMA: Pindahkan menggunakan Rigidbody, BUKAN transform!
        // MovePosition membuat Unity menghitung gesekan fisik, jadi dia gak bakal nembus/keterusan
        rb.MovePosition(newPos);
    }

    // FUNGSI BARU: Otomatis dipanggil Unity begitu tangan lepas dari klik/drag mouse
    public void OnEndDrag(PointerEventData eventData)
    {
        // REM TOTAL: Begitu dilepas, paksa semua kecepatan fisika jadi 0 detik itu juga
        rb.linearVelocity = Vector2.zero;

        // Opsional: Untuk mematikan sisa gaya putar (jika rotasi tidak dikunci)
        rb.angularVelocity = 0f;    
    }
}
