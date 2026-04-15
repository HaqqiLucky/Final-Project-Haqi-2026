using System.Linq;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class MengurutkanAngkaSceneControl : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject borderTengah;
    [SerializeField] private RectTransform panelAtas;
    [SerializeField] private RectTransform panelBawah;
    [SerializeField] private RectTransform panelKanan;
    [SerializeField] private RectTransform panelKiri;
    [SerializeField] private RectTransform panelTengahAtas;
    [SerializeField] private RectTransform panelTengahBawah;
    [SerializeField] private ParticleSystem ConfentiKiri;
    [SerializeField] private ParticleSystem ConfentiKanan;
    [SerializeField] private GameObject Trains;
    public bool SelesaiThisStage;
    //public int PenghitungAmalKebenaran;

    [Header("Arrays")]
    [SerializeField] private int[] Numbers = new int[20];
    private GameObject[] BoxTengahAtas = new GameObject[5];
    private GameObject[] BoxTengahBawah = new GameObject[5];
    private int[] JawabanBener = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    [SerializeField] private int[] JawabanUntukDiCek = new int[10];
    public GameObject[] BoxTengahBersatu = new GameObject[10];

    void Start()
    {
        panelAtas.gameObject.SetActive(true);
        panelBawah.gameObject.SetActive(true);
        panelKanan.gameObject.SetActive(true);
        panelKiri.gameObject.SetActive(true);
        RandomSerahkanPadaku();
        Prefabs55();
        BoxTengahBersatu = BoxTengahAtas.Concat(BoxTengahBawah).ToArray();

        //Trains.SetActive(false);
        

    }

    void Update()
    {
        //PengecekanRutinKotaks();
    }

    private void RandomSerahkanPadaku()
    {
        // ini ga di random
        for (int i = 0; i < 10; i++) // ini dari baris
        {
            Numbers[i] = i + 1; // ini value
            //Debug.Log("Ini interasi ke - " + Numbers[i]);
        }

        // yg ini di random
        for (int i = 10; i < Numbers.Length; i++)
        {
            Numbers[i] = Random.Range(1, 11);
        }

        // shuffle versi linq
        Numbers = Numbers.OrderBy(x => Random.value).ToArray();
    }

    private void Prefabs55()
    {
        // panel atas
        for (int i = 0 ; i <= 4; i++)
        {
            GameObject prefabsTralala = Instantiate(tilePrefab, panelAtas);
            prefabsTralala.transform.SetParent(panelAtas, false);
            TMP_Text Angka = prefabsTralala.GetComponent<TMP_Text>();
            Angka.text = Numbers[i].ToString();
        }

        // panel bawah
        for (int i = 5; i <= 9; i++)
        {
            GameObject prefabsTralala = Instantiate(tilePrefab, panelBawah);
            prefabsTralala.transform.SetParent(panelBawah, false);
            TMP_Text Angka = prefabsTralala.GetComponent<TMP_Text>();
            Angka.text = Numbers[i].ToString();
        }

        // panel kanan
        for (int i = 10; i <= 14; i++)
        {
            GameObject prefabsTralala = Instantiate(tilePrefab, panelKanan);
            prefabsTralala.transform.SetParent(panelKanan, false);
            TMP_Text Angka = prefabsTralala.GetComponent<TMP_Text>();
            Angka.text = Numbers[i].ToString();
        }

        // panel kiri
        for (int i = 15; i <= 19; i++)
        {
            GameObject prefabsTralala = Instantiate(tilePrefab, panelKiri);
            prefabsTralala.transform.SetParent(panelKiri, false);
            TMP_Text Angka = prefabsTralala.GetComponent<TMP_Text>();
            Angka.text = Numbers[i].ToString();
        }

        // panel Tengahatas
        for (int i = 0; i < 5; i++)
        {
            GameObject prefabsTengahAtas = Instantiate(borderTengah, panelTengahAtas);
            prefabsTengahAtas.transform.SetParent(panelTengahAtas, false);
            prefabsTengahAtas.GetComponent<SlotKotakTampung>().slotIndex = i; // yang dari ai dan aku blm paham
            //Debug.Log("Ini box atas ke - "+ i);
            BoxTengahAtas[i] = prefabsTengahAtas;
        }
        // panel Tengahbawah
        for (int i = 0; i < 5; i++)
        {
            GameObject prefabsTengahBawah = Instantiate(borderTengah, panelTengahBawah);
            prefabsTengahBawah.transform.SetParent(panelTengahBawah, false);
            BoxTengahBawah[i] = prefabsTengahBawah;
            prefabsTengahBawah.GetComponent<SlotKotakTampung>().slotIndex = i + 5; // yang dari ai dan aku blm paham
        }
    }

    public void PengecekanRutinKotaks() // ini juga function aku blm paham, walaupun bbrp ada yg phm
    {
        // Cek apakah array utamanya sudah siap
        if (BoxTengahBersatu == null || BoxTengahBersatu.Length == 0) return;

        for (int i = 0; i < BoxTengahBersatu.Length; i++)
        {
            // 1. Cek apakah objek kotaknya sendiri ada
            if (BoxTengahBersatu[i] == null) continue;

            // 2. Cari komponen TMP_Text di dalam kotak tersebut
            TMP_Text textDiDalam = BoxTengahBersatu[i].GetComponentInChildren<TMP_Text>();

            // 3. SARINGAN: Jika ada teksnya, ambil angkanya. Jika tidak ada, anggap 0.
            if (textDiDalam != null && !string.IsNullOrEmpty(textDiDalam.text))
            {
                // Gunakan TryParse supaya kalau teksnya bukan angka (misal simbol), tidak error
                if (int.TryParse(textDiDalam.text, out int angka))
                {
                    JawabanUntukDiCek[i] = angka;
                }
            }
            else
            {
                // Kotak kosong, kita kasih nilai 0 supaya array JawabanUntukDiCek tetap sinkron
                JawabanUntukDiCek[i] = 0;
            }

            //Debug.Log($"Kotak ke-{i} isinya: {JawabanUntukDiCek[i]}");
        }

        // 4. Bandingkan dengan jawaban yang benar
        if (JawabanBener.SequenceEqual(JawabanUntukDiCek))
        {
            StartCoroutine(MengurutkanAngkaSelesai());
        }
    }
    private void OnEnable()
    {
        DraggableItemAngkas.OnDropEndToCheckTheCurrentWaves += PengecekanRutinKotaks; // logika ini di draggalble aku blm paham
    }

    private void OnDisable()
    {
        DraggableItemAngkas.OnDropEndToCheckTheCurrentWaves -= PengecekanRutinKotaks;
        
    }

    IEnumerator MengurutkanAngkaSelesai()
    {
        SelesaiThisStage = true;
        yield return new WaitForSeconds(1f);
        Confenti();
        yield return new WaitForSeconds(3f);
        AnimasiSelesaiKotakNaik();
        panelAtas.gameObject.SetActive(false);
        panelBawah.gameObject.SetActive(false);
        panelKanan.gameObject.SetActive(false);
        panelKiri.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        //Trains.SetActive(true);


    }

    private void Confenti()
    {
        ConfentiKanan.Play();
        ConfentiKiri.Play();
    }

    private void AnimasiSelesaiKotakNaik()
    {
        LeanTween.moveY(panelTengahBawah, 200f, 1f)
            .setEaseOutBack();
    }

}
