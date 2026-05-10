using UnityEngine;
using UnityEngine.UI;

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
    }


    private void instantiateYangButuhParaneterYuhu()
    {
        string pathFolder = "ContohKata/";

        int[] urutanGambar = new int[] { 0, 1, 2 };

        for (int i = 0; i < urutanGambar.Length; i++)
        {
            int temp = urutanGambar[i];
            int randomIndex = Random.Range(i, urutanGambar.Length);
            urutanGambar[i] = urutanGambar[randomIndex];
            urutanGambar[randomIndex] = temp;
        }
        // --- Langkah 2: Munculkan Teks ---
        for (int i = 0; i < namaBuahHewanPilihan.Length; i++)
        {
            GameObject goTeks = Instantiate(ParentHuruf, SuperParentHuruf.transform);

            // PAKSA NAMA: Jadi "Apel", bukan "ParentHuruf(Clone)"
            goTeks.name = namaBuahHewanPilihan[i];

            var teksTMP = goTeks.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (teksTMP != null) teksTMP.text = namaBuahHewanPilihan[i];
        }

        // --- Langkah 3: Munculkan Gambar ---
        for (int i = 0; i < urutanGambar.Length; i++)
        {
            int indexAcak = urutanGambar[i];
            GameObject goGambar = Instantiate(ParentGambar, SuperParentGambar.transform);

            // PAKSA NAMA: Jadi "Apel", bukan "ParentGambar(Clone)"
            goGambar.name = namaBuahHewanPilihan[indexAcak];

            Image img = goGambar.GetComponentInChildren<Image>();
            if (img != null)
            {
                img.sprite = Resources.Load<Sprite>(pathFolder + namaBuahHewanPilihan[indexAcak]);
            }
        }
    }
}


