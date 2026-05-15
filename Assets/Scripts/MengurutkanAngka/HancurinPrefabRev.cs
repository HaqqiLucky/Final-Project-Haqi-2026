using UnityEngine;
using UnityEngine.EventSystems;

public class HancurinPrefabRev : MonoBehaviour, IDragHandler
{
    [Header("Batas Geser")]
    public float minX;
    public float maxX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //this.gameObject.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 1. Ambil posisi mouse/input saat ini
        Vector3 newPos = transform.localPosition;

        // 2. Tambahkan perubahan posisi (delta) hanya pada sumbu X
        // Kita bagi dengan canvas scale agar pergerakan presisi
        newPos.x += eventData.delta.x;

        // 3. Batasi nilainya menggunakan Mathf.Clamp
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);

        // 4. Terapkan posisi baru
        transform.localPosition = newPos;
    }   
}
