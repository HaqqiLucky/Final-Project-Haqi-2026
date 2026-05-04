using System.Collections;
using TMPro;
using UnityEngine;

public class RandomXSpawner : MonoBehaviour
{

    [SerializeField] GameObject TeksPrefab;
    private string[] hurufBesar = new string[]
        {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
        "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
        };
    [SerializeField] float SecondSpawn;
    [SerializeField] float minTras;
    [SerializeField] float maxTras;
    private Transform ParentHurufCanvas;
    private string[] kumpulanKata = new string[]
        {
        "Astronot",    // A - Visua
        "Boba",        // B - Visual: Gelas plastik dengan bola-bola hitam
        "Cokelat",     // C - Visual: Batangan cokelat yang digigit sedikit
        "Donat",       // D - Visual: Bulat, ada lubang, meses warna-warni
        "Eskrim",      // E - Visual: Es krim cone warna pink/cokelat
        "Foto",        // F - Visual: Kamera instan atau pose selfie
        "Gitar",       // G - Visual: Alat musik kayu dengan senar
        "Helikopter",  // H - Visual: Kendaraan baling-baling di langit
        "Ikan",        // I - Visual: Ikan badut (oranye-putih) seperti Nemo
        "Jamur",       // J - Visual: Payung merah bintik putih (estetika game)
        "Kamera",      // K - Visual: Bentuk kamera digital atau ikon kamera HP
        "Lego",        // L - Visual: Balok susun bertumpuk warna-warni
        "Mobil",       // M - Visual: Mobil sport merah yang keren
        "Nugget",      // N - Visual: Gorengan ayam bentuk huruf atau hewan
        "Onde-onde",   // O - Visual: Bola wijen (makanan lokal yang ikonik)
        "Paket",       // P - Visual: Kardus cokelat dengan lakban (ikon belanja online)
        "QRIS",        // Q - Visual: Kotak barcode (sangat relate pas jajan)
        "Robot",       // R - Visual: Robot besi lucu dengan antena
        "Susu",        // S - Visual: Kotak susu dengan gambar sapi
        "Tablet",      // T - Visual: Gadget layar lebar (alat main mereka)
        "Uang",        // U - Visual: Lembaran kertas warna merah/biru
        "Video Game",  // V - Visual: Stick/Controller game (Playstation/Xbox)
        "WiFi",        // W - Visual: Simbol garis melengkung biru (sinyal)
        "Xilofon",     // X - Visual: Alat musik bilah pelangi (sangat kontras)
        "Yoyo",        // Y - Visual: Mainan bulat dengan tali melingkar
        "Zebra",       // Z - Visual: Kuda garis-garis hitam putih
        };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnRandomHuruf());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        //Debug.Log("bang")
        GameObject CanvasGO = GameObject.Find("CanvasRoot");
        ParentHurufCanvas = CanvasGO.GetComponent<Transform>();

    }

    IEnumerator SpawnRandomHuruf()
    {
        while (true)
        {
            var wanted = Random.Range(minTras, maxTras);
            Vector3 position = new Vector3(wanted, transform.localPosition.y, 0);
            GameObject go = Instantiate(TeksPrefab, position, Quaternion.identity, parent:ParentHurufCanvas);
            string randomChar = hurufBesar[Random.Range(0, hurufBesar.Length)];
            go.GetComponent<TextMeshProUGUI>().text = randomChar;

            //Vector3 spawnPos = new Vector3();

            //// 4. Atur posisi lokalnya terhadap parent
            //go.transform.localPosition = spawnPos;

            //yield return new WaitForSeconds(SecondSpawn);
        }
    }
}
