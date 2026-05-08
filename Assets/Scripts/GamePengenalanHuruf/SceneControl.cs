using UnityEngine;

public class SceneControl : MonoBehaviour
{
    private string[] namaBuahHewan = new string[26]
    {
        "Apel",     // A
        "Bola",     // B
        "Cicak",    // C
        "Donat",    // D
        "Es krim",  // E
        "Feri",     // F
        "Gajah",    // G
        "Harimau",  // H
        "Ikan",     // I
        "Jerapah",  // J
        "Kucing",   // K
        "Lampu",    // L
        "Mobil",    // M
        "Nanas",    // N
        "Obat",     // O
        "Pisang",   // P
        "Quran",    // Q
        "Roti",     // R
        "Susu",     // S
        "Topi",     // T
        "Ular",     // U
        "Vas",      // V
        "Wortel",   // W
        "Xilofon",  // X
        "Yoyo",     // Y
        "Zebra"     // Z
    };
    [SerializeField] private string[] namaBuahHewanPilihan = new string[3];
    [SerializeField] private GameObject ParentHuruf;
    [SerializeField] private GameObject SuperParentHuruf;
    [SerializeField] private GameObject ParentGambar;
    [SerializeField] private GameObject SuperParentGambar;

    private void Awake()
    {
        PilihTigaAcak();
        
    }

    void Start()
    {
        instantiateYangButuhParaneterYuhu();
    }

    void Update()
    {
        
    }

    private void PilihTigaAcak()
    {
        // 1. Acak daftar utamanya dulu (Fisher-Yates Shuffle)
        for (int i = 0; i < namaBuahHewan.Length; i++)
        {
            string temp = namaBuahHewan[i];
            int randomIndex = Random.Range(i, namaBuahHewan.Length);
            namaBuahHewan[i] = namaBuahHewan[randomIndex];
            namaBuahHewan[randomIndex] = temp;
        }

        // 2. Masukkan 3 item pertama yang sudah teracak ke array pilihan
        // Karena daftar utama sudah diacak, 3 pertama pasti akan selalu beda tiap dijalankan
        namaBuahHewanPilihan[0] = namaBuahHewan[0];
        namaBuahHewanPilihan[1] = namaBuahHewan[1];
        namaBuahHewanPilihan[2] = namaBuahHewan[2];

        // Cek hasil di Console
        Debug.Log("Pilihan 1: " + namaBuahHewanPilihan[0]);
        Debug.Log("Pilihan 2: " + namaBuahHewanPilihan[1]);
        Debug.Log("Pilihan 3: " + namaBuahHewanPilihan[2]);
    }


    private void instantiateYangButuhParaneterYuhu()
    {
        // Kita lakukan looping 3 kali sesuai jumlah pilihan
        for (int i = 0; i < namaBuahHewanPilihan.Length; i++)
        {
            // 1. Munculkan Prefab ParentGambar di dalam SuperParentGambar
            GameObject objBaru = Instantiate(ParentGambar, SuperParentGambar.transform);

            // 2. Beri nama objeknya supaya sama dengan nama buah/hewan (opsional tapi membantu)
            objBaru.name = namaBuahHewanPilihan[i];

            // 3. AMBIL Komponen Text atau Script di dalam objBaru untuk diubah teksnya
            // Contoh: Jika di dalam ParentGambar ada TextMeshProUGUI
            // objBaru.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = namaBuahHewanPilihan[i];
        }
    }
}
