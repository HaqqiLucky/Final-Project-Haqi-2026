using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
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

    //public bool sudahKlik = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        //TransisiBuka();
        //Invoke("TransisiTutup", 2f);
        Blur.SetActive(false);
        StartCoroutine(StartUpPanelNaik());
    }

    //private void Awake()
    //{
    //    HurufMunculSekarang 
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StartUpPanelNaik()
    {
        LeanTween.moveLocalY(PanelDanBurung, 0, 4f)
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
        LeanTween.value(transition.gameObject, transition.fillAmount, 1, 1f)
            .setOnUpdate((float val) =>
            {
                transition.fillAmount = val;
            });
    }
    private void TransisiTutup()
    {

        transition.fillClockwise =  false;
        LeanTween.value(transition.gameObject, transition.fillAmount, 0, 1f)
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



    // setiap 5 huruf yang 

    



}
