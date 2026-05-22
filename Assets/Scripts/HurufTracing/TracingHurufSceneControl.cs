using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TracingHurufSceneControl : MonoBehaviour
{
    [SerializeField] GameObject PanelDanBurung;
    [SerializeField] Image transition;
    [SerializeField] GameObject Blur;
    [SerializeField] GameObject HurufParentUlala;
    [SerializeField] TextMeshProUGUI HurufMunculSekarang;
    [SerializeField] GameObject ButtonNext;
    [SerializeField] CanvasGroup buttons;
    [SerializeField] GameObject PenghalangButton;
    public List<string> setiapEmpat = new List<string>();


    //[SerializeField] Button AllButtonInGridParent;
    private string hurufSekarang; // ini cyma nampung
    
    private GameObject currentImageThisLetterHehe;
    //private bool loadingProses = false;
    [Header("PengenalanHurufMoozik")]
    [SerializeField] private AudioSource Adsos;
    [SerializeField] private AudioSource AdsosCanvas;
    [SerializeField] private AudioClip[] musiks;
    private int indexMusik = 0;

    [Range(0f, 1f)]
    [SerializeField] private float volume = 0.362f;

    AudioClip untukApel = null;
    AudioClip A;
    //private AudioClip A, untukApel;
    //public bool sudahKlik = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        //TransisiBuka();
        //Invoke("TransisiTutup", 2f);
        Blur.SetActive(false);
        StartCoroutine(StartUpPanelNaik()); //ddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd gantii ini ini aku comen soalnya buat lama lama ajah

        musiks = musiks.OrderBy(x => Random.value).ToArray();
        PlayNext();
    }
    private void PlayNext()
    {
        Adsos.clip = musiks[indexMusik];
        Adsos.Play();
        indexMusik = (indexMusik + 1) % musiks.Length;
    }

    //private void Awake()
    //{
    //    HurufMunculSekarang 
    //}

    // Update is called once per frame
    void Update()
    {
        // buat volume
        Adsos.volume = volume;
        if (!Adsos.isPlaying) PlayNext();
    }
    IEnumerator StartUpPanelNaik()
    {
        LeanTween.moveLocalY(PanelDanBurung, 0, 0.2f) // ini td 4
        .setOnComplete(() => {
            //OuterButton.interactable = true;
            PenghalangButton.SetActive(false);
            LeanTween.moveLocalY(PanelDanBurung, 4f, 2f)
                //.setEaseInOutSine()
                .setLoopPingPong(); 
        });
        yield return null;
    }
    //RefreshHurufdanGambar(hurufYangDiKlik);


    //public void DiKlikDaiButtonHurufPanelAwal(string hurufYangDiKlik)
    //{


    //    TransisiTutup();
    //    //StartCoroutine(SkenarioBukaTutupDiawal(hurufYangDiKlik));
    //}

    private void LoadingHurufGambar()
    {
        char baseChar = hurufSekarang[0];
        //setiapEmpatKali.appe
        string formatTampilan = baseChar.ToString().ToUpper() + baseChar.ToString().ToLower();

        HurufMunculSekarang.text = formatTampilan;

        //add audio
        CariAudioDariResouces();                
        CariGambarDariParentGambarUlala();
    }

    public IEnumerator SkenarioBukaTutupDiawal(string hurufBaru)
    {
        // 1. Jalankan Transisi Buka (Layar tertutup/hitam)
        TransisiBuka();
        yield return new WaitForSeconds(1.0f);

        // 2. Update Data saat layar sedang tertutup
        hurufSekarang = hurufBaru;
        LoadingHurufGambar();

        if (PanelDanBurung.activeSelf) PanelDanBurung.SetActive(false);

        // 3. Jalankan Transisi Tutup (Layar terbuka kembali)
        TransisiTutup();
        LeanTween.value(buttons.gameObject, 0, 1, 5f)
            .setOnUpdate((float val) =>
            {
                buttons.alpha += val;
            });
    }
    // animasi transisi
    private void TransisiBuka()
    {
        LeanTween.cancel(PanelDanBurung);
        transition.fillClockwise =  true;
        LeanTween.value(transition.gameObject, transition.fillAmount, 1, 0.1f) // sama
            .setOnUpdate((float val) =>
            {
                transition.fillAmount = val;
            });
    }
    private void TransisiTutup()
    {

        transition.fillClockwise =  false;
        LeanTween.value(transition.gameObject, transition.fillAmount, 0, 0.1f) // ini tadi 1f
            .setOnUpdate((float val) =>
            {
                transition.fillAmount = val;

                // pas tutup, buka panel abcd dengan gambar dan huruf singel singel
                Blur.SetActive(true);
            });
            //.setOnComplete(() =>
            //{
            //    loadingProses = false;
            //});

    }

    private void CariGambarDariParentGambarUlala()
    {
        foreach (Transform child in HurufParentUlala.transform)
        {
            child.gameObject.SetActive(false);
            if (child.name.StartsWith(hurufSekarang[0]))
            {
                child.gameObject.SetActive(true);
                //Debug.Log("sampesini");
            }
        }
    }

    private void CariAudioDariResouces()
    {
        
        // Path harus sesuai folder di gambar
        AudioClip[] SemuaClipUntuk = Resources.LoadAll<AudioClip>("PengenalanHuruf/untukkamu");

        // Pastikan hurufSekarang tidak kosong
        if (string.IsNullOrEmpty(hurufSekarang)) return;

        // Ambil huruf pertama (pakai index 0, bukan 1 atau -1)
        char hurufTarget = hurufSekarang[0];

        foreach (AudioClip untuk in SemuaClipUntuk)
        {
            // File "untukapel" -> index 5 adalah 'a'
            if (untuk.name.Length > 5)
            {
                // Gunakan char.ToLower supaya 'A' cocok dengan 'a' di nama file
                if (char.ToLower(untuk.name[5]) == char.ToLower(hurufTarget))
                {
                    untukApel = untuk;
                    break; // Keluar loop jika sudah ketemu
                }
            }
        }

        // Load suara hurufnya (misal "a.mp3" di folder PengenalanHuruf)
        A = Resources.Load<AudioClip>("PengenalanHuruf/" + hurufTarget);

        if (A != null && untukApel != null)
        {
            StartCoroutine(AntrianAudio(A, untukApel));
        }
        else
        {
            Debug.LogError("Audio tidak ditemukan! Cek nama file atau path folder.");
        }
    }


    public void HurufClicked()
    {
        AdsosCanvas.clip = A;
        AdsosCanvas.Play();
    }
    public void GambarClicked()
    {
        AdsosCanvas.clip = untukApel;
        AdsosCanvas.Play();
    }

    IEnumerator AntrianAudio(AudioClip pertama, AudioClip kedua)
    {
        AdsosCanvas.clip = pertama;
        AdsosCanvas.PlayDelayed(1f);
        yield return new WaitForSeconds(pertama.length + 1f);
        AdsosCanvas.clip = kedua;
        AdsosCanvas.Play();
    }



    public void NextButtonInAlphabet()
    {
        // 1. Ambil karakter saat ini (asumsi formatnya "A" atau "Aa")
        char currentKarakter = hurufSekarang[0];


        // 2. Tambahkan satu karakter
        currentKarakter++;

        // 3. Cek batas: Jika setelah 'Z', maka balik ke 'A'
        if (currentKarakter > 'Z')
        {
            currentKarakter = 'A';
        }

        // 4. Jalankan skenario animasi
        // Gunakan StopAllCoroutines() jika ingin mencegah user klik spam tombol next
        StopAllCoroutines();
        StartCoroutine(SkenarioBukaTutupDiawal(currentKarakter.ToString()));
    }

}
