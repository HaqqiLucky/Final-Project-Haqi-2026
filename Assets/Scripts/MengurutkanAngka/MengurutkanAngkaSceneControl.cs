using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject TangkapIniHancurinPrefa;
    [SerializeField] private Image overlayUIOut;
    [SerializeField] private Image overlayUIIn;
    [SerializeField] private ParticleSystem fireworks;
    [SerializeField] private GameObject shape1;
    [SerializeField] private GameObject shape2;
    public bool SelesaiThisStage;
    [SerializeField] SliderTimer timersSui;
    //public int PenghitungAmalKebenaran;
    [SerializeField] private GameObject skorParent;
    //private bool isSelesaiRunning = false;
    //[SerializeField] private SlotKotakTampung slotKotakTampung;

    [Header("Arrays")]
    [SerializeField] private int[] Numbers = new int[20];
    private GameObject[] BoxTengahAtas = new GameObject[5];
    private GameObject[] BoxTengahBawah = new GameObject[5];
    private int[] JawabanBener = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    [SerializeField] private int[] JawabanUntukDiCek = new int[10];
    public GameObject[] BoxTengahBersatu = new GameObject[10];


    [Header("Kebutuhan Plot")]
    public int TotalYangSudahDiHancurkan;
    public bool SudahNaik = false;

    [Header("Referensi")]
    [SerializeField] private CanvasGroup ButtonsYangTerakhir;

    [Header("MengurutkanAngkaMoozik")]
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioClip[] musiks;
    private int indexMusik = 0;
    [SerializeField] private AudioClip confentii;

    [Range(0f, 1f)]
    [SerializeField] private float volume = 0.362f;

    void Start()
    {
        shape1.transform.localScale = new Vector3(0.1f, 0.02f, 1);
        TotalYangSudahDiHancurkan = 0;
        TransisiKebalikan();
        panelAtas.gameObject.SetActive(true);
        panelBawah.gameObject.SetActive(true);
        panelKanan.gameObject.SetActive(true);
        panelKiri.gameObject.SetActive(true);
        RandomSerahkanPadaku();
        Prefabs55();
        BoxTengahBersatu = BoxTengahAtas.Concat(BoxTengahBawah).ToArray();
        overlayUIOut.gameObject.SetActive(false);
        TangkapIniHancurinPrefa.SetActive(false);


        musiks = musiks.OrderBy(x => Random.value).ToArray();
        PlayNext();


    }


    void Update()
    {
        //PengecekanRutinKotaks();
        bgm.volume = volume;
        if (!bgm.isPlaying) PlayNext();
    }

    // bwat gaca lagu
    private void PlayNext()
    {
        bgm.clip = musiks[indexMusik];
        bgm.Play();
        indexMusik = (indexMusik + 1) % musiks.Length;
    }

    private void TransisiKebalikan()
    {
        LeanTween.value(overlayUIIn.gameObject, 1f, 0f, 2f)
           .setEaseInOutBack()
           .setOnUpdate((float val) => {
               overlayUIIn.fillAmount = val;
           })
           .setOnComplete(() =>
           {
               overlayUIIn.gameObject.SetActive(false);
               timersSui.StartTimer();
           });
        
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

    public IEnumerator MengurutkanAngkaSelesai()
    {
        //Debug.Log("jujur janggar");
        SelesaiThisStage = true;
        timersSui.StopTimer();
        //Debug.Log("sampe sini23");
        yield return new WaitForSeconds(1f);
        timersSui.gameObject.SetActive(false);
        Confenti();
        //Debug.Log("sampe sini24");
        yield return StartCoroutine(AnimasiSelesaiKotakNaik());
        //yield return StartCoroutine(Train());
        //yield return new WaitForSeconds(3f);
        //Debug.Log("sampe sini2");
        //AnimasiSelesaiKotakNaik();



        //yield return new wait
    }

    //IEnumerator 

    private void Confenti()
    {
        ConfentiKanan.Play();
        ConfentiKiri.Play();
    }
    //IEnumerator Train()
    //{
    //    //Debug.Log("sampe sini88");
    //    //yield return new WaitForSecondsRealtime(2f);
    //    TangkapIni.SetActive(true);
    //    yield return new WaitForSeconds(47f);
    //    TangkapIni.SetActive(false);
    //    Skoring();
    //    //Debug.Log("sampe sini1");

    //}

    IEnumerator KelarJatoh()
    {
        yield return new WaitForSeconds(14f);
        Skoring();
    }

    private void Skoring()
    {
        HitungSkorAkhir();
        OverlayUIWork();
        //Debug.Log("sampe siiniyeyeyeyey");
    }

    IEnumerator AnimasiSelesaiKotakNaik()
    {
        yield return new WaitForSeconds(3);
        LeanTween.moveY(panelTengahBawah, 200f, 1f)
            .setEaseOutBack();
        panelAtas.gameObject.SetActive(false);
        panelBawah.gameObject.SetActive(false);
        panelKanan.gameObject.SetActive(false);
        panelKiri.gameObject.SetActive(false);
        //Debug.Log("sampe sini");
        yield return new WaitForSeconds(2);
        SudahNaik = true;
        TangkapIniHancurinPrefa.SetActive(true);
        //NaikinAlphaPenghancurPrefab();
        //Debug.Log("harusnya udah true");
        StartCoroutine(KelarJatoh());
    }

    //private void NaikinAlphaPenghancurPrefab()
    //{
    //    LeanTween.value(TangkapIniHancurinPrefa.gameObject, 0f, 1f, 1f)
    //     .setOnUpdate((float val) => {
    //         // 'val' adalah angka yang terus berubah dari 0 ke 1
    //         TangkapIniHancurinPrefa.alpha = val;
    // });
    //}

    private void OverlayUIWork()
    {
        TangkapIniHancurinPrefa.SetActive(false);

        overlayUIOut.gameObject.SetActive(true);
        LeanTween.value(overlayUIOut.gameObject, 0f, 1f, 2f)
           .setEaseInOutBack()
           .setOnUpdate((float val) => {
            overlayUIOut.fillAmount = val;
         })
           .setOnComplete(() => {
               StartCoroutine(SkoringScenario());
           });
    }

    IEnumerator SkoringScenario()
    {
        fireworks.Play();
        bgm.PlayOneShot(confentii);
        shape1.SetActive(true);
        LeanTween.scaleX(shape1, 7, 2f).setEaseOutBack();
        shape2.SetActive(true);
        yield return null;
        skorParent.SetActive(true);
        HitungSkorAkhir();
        yield return new WaitForSeconds(5f);
        ButtonsActiveandAnimate();
    }

    private void ButtonsActiveandAnimate()
    {
        ButtonsYangTerakhir.gameObject.SetActive(true);
        LeanTween.value(ButtonsYangTerakhir.gameObject, 0f, 1f, 1f)
            .setOnUpdate((float val) => {
                // 'val' adalah angka yang terus berubah dari 0 ke 1
                ButtonsYangTerakhir.alpha = val;
            });
    }

    // penghitungan sekor tralala
    public int HitungSkorAkhir()
    {
        // current time di kali dengan 100
        //timersSui.currentTime
        int ParsingCurrentTime = Mathf.RoundToInt(timersSui.Durasi);
        int skorAkhirDariTimer = ParsingCurrentTime * 100;

        // ini total prefab yang hancur nanti di kali dengan 300
        int skorAkhirDariPrefabs = TotalYangSudahDiHancurkan * 300;

        // sampe sini 9000
        int skorakhirPrefabsDanTimer = skorAkhirDariPrefabs + skorAkhirDariTimer;

        // di tambah 1000
        int skorTotalDiShowKeGame = skorakhirPrefabsDanTimer + 1000;

        return skorTotalDiShowKeGame;
        // paling besar 10.000
        // paling kecil 1000
        

    }

    public void GoReplay()
    {
        //LoadingScreenSceneControl.TargetSceneName = "MengurutkanAngka";
        LoadingScreenSceneControl.Instance.LoadScene("MengurutkanAngka");

        //SceneManager.LoadScene(6);
    }
    public void GoHome()
    {
        //LoadingScreenSceneControl.TargetSceneName = "HomeScene";
        LoadingScreenSceneControl.Instance.LoadScene("HomeScene");

        //SceneManager.LoadScene(6);
    }
}
