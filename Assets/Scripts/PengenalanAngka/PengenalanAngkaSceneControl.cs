 using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PengenalanAngkaSceneControl : MonoBehaviour
{
    [Header("GameObject Important")]
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject prefabsGridElement;
    [SerializeField] private GameObject parentofAllEntities;
    [SerializeField] private GameObject gridParent;
    [SerializeField] private GameObject Balud;
    [SerializeField] private GameObject backrgroundAngka;
    [SerializeField] private ParticleSystem AnginKeKanan;
    //[SerializeField] private GameObject backrgroundAngka;
    //[SerializeField] TextMeshProUGUI number;
    private GameObject[] Animals;
    private int currentAngka = 1;
    private string namaHewan;
    public int JumlahHewanYangDiklik = 0;
    public bool AnimasiJalankah = false;
    

    [Header("Gameobject not really important")]
    public AkuTidakMauTahuIniHarusMengestuckAnimasiDisabled butonKlik;
    [SerializeField] private TextMeshProUGUI HeadlineKeluarDariScript;
    [SerializeField] private ParticleSystem Confentii;
    [SerializeField] private PergerakanBalonUdara gerakBalud;
    //[SerializeField] private InfiniteSky speedAwan;

    //[Header("Prefabs Skies animasi transisi ganti angka")]
    //private GameObject Skyday1;


    void Start()
    {
        Camera.main.gameObject.transform.position = Vector3.zero;
        MainFunctionPerulanganMasuk();
    }


    private void Update()
    {
        if (currentAngka > 10)
        {
            SceneManager.LoadScene(1);
        }
    }

    private void MainFunctionPerulanganMasuk()
    {
        backrgroundAngka.SetActive(false);
        TakeTheAnimals();
        Angka1Sampai10danButtonAcak();
        LeanTween.moveLocalX(panel, 311f, 1f)
            .setEase(LeanTweenType.linear);

        LeanTween.moveLocalY(panel, panel.transform.localPosition.y + 3f, 0.5f)
            .setEase(LeanTweenType.easeInOutQuad)
            .setLoopPingPong()
            .setRepeat(-1);

        Headline();



    }

    private void TakeTheAnimals()
    {
        // ngambil anak anak
        int allEntities = parentofAllEntities.transform.childCount;
        Animals = new GameObject[allEntities];
        for (int i = 0; i < allEntities; i++)
        {
            Animals[i] = parentofAllEntities.transform.GetChild(i).gameObject;
        }
    }

    private GameObject RandomAnimals()
    {
        int animalsRandom = Random.Range(0, Animals.Length);
        return Animals[animalsRandom];
    }

    private void Angka1Sampai10danButtonAcak()
    {
        int currentAngkaBuatDiPakediFunction = currentAngka;
        int jumlahButton = currentAngkaBuatDiPakediFunction;
        //RandomAnimals().SetActive(true);
        GameObject hewanPilihan = RandomAnimals();
        GameObject cloneHewan = Instantiate(hewanPilihan, prefabsGridElement.transform);
        cloneHewan.SetActive(true);
        cloneHewan.transform.localPosition = Vector3.zero;


        prefabsGridElement.transform.GetChild(1).gameObject.SetActive(true);
        prefabsGridElement.transform.GetChild(1).localPosition = Vector3.zero;
        if (prefabsGridElement.transform.childCount <= 2)
        {
            Debug.Log("masuk if");
            for (int i = 1; i < currentAngkaBuatDiPakediFunction; i++)
            {
                Debug.Log("masuk for");
                Instantiate(prefabsGridElement, gridParent.transform);
                //Vector3 PrefabLoc = PrefabIniBuatAkuMurka.transform.position;
                //Vector3 PrefabLocal = PrefabIniBuatAkuMurka.transform.localPosition;
                
                //Instantiate(backrgroundAngka,PrefabLoc, Quaternion.identity);
                //Debug.Log("Objek ke-" + i + " ada di lokasi: " + PrefabLoc);
                //Debug.Log("Objek ke-" + i + " ada di lokasi lokal: " + PrefabLocal);
            }
        } else
        {
            Debug.Log("Gatau ah");
        }

    }




    private void Headline()
    {
        string entitySekarang = prefabsGridElement.transform.GetChild(1).name;
        if (entitySekarang.Contains("Landak"))
        {
            namaHewan = "Landak";
        }
        else if (entitySekarang.Contains("Fox"))
        {
            namaHewan = "Rubah";
        }
        else if (entitySekarang.Contains("Cat"))
        {
            namaHewan = "Kucing";
        }
        else if (entitySekarang.Contains("RedPanda"))
        {
            namaHewan = "Panda Merah";
        }
        else if (entitySekarang.Contains("Hoddie"))
        {
            namaHewan = "Arwah jaket merah";
        }
        else if (entitySekarang.Contains("Bunny"))
        {
            namaHewan = "Anak kelinci";
        }



    }


    public void EveryButtonClicked()
    {
        JumlahHewanYangDiklik++;

        if (currentAngka == JumlahHewanYangDiklik)
        {
            HeadlineKeluarDariScript.text = JumlahHewanYangDiklik.ToString() + " " + namaHewan;
            //JumlahHewanYangDiklik = butonKlik.buttonYgSdhDiKlik.ToString();
            Debug.Log("Ini Jumlah hewan yg di kklik : " + JumlahHewanYangDiklik);
            StartCoroutine(BalonNaikGantiLevel());  
        }

    }


    IEnumerator BalonNaikGantiLevel()
    {
        yield return new WaitForSeconds(0.5f);
        Confentii.Play();
        yield return new WaitForSeconds(2f);
        PanelNgaleh();
        yield return new WaitForSeconds(1f);
        //gerakBalud.BalonUdaraTerbangHabisSelesai(new Vector2(-6.13f, 9.58f), 2f);
        currentAngka++;
        JumlahHewanYangDiklik = 0;

        StartCoroutine(KorotinUntukAnimasiMengancurkanPrefabdanAnimasiHehe());
        // masukin korotin


        // animasi dlu baru hancurkan prefab hahaa


        // 4 april 2026, ini ga usah naik, animasinya terbang ke kanan aja, balon agak di miringin, bu

        //LeanTween.moveY(Camera.main.gameObject, 12, 1f)
        //    .setOnComplete(() =>
        //{
        //    MainFunctionPerulanganMasuk();
        //    LeanTween.delayedCall(0.7f, () =>
        //    {
        //        LeanTween.moveY(Balud, 12f, 2f)
        //                    .setEaseInOutBack();
        //    });
            
        //});
    }

    private void HancurkanPrefabHahahaha()
    {
        foreach (Transform child in prefabsGridElement.transform)
        {
            if (child.name == "BackgroundNumber")
            {
                continue;
            }
            DestroyImmediate(child.gameObject);
        }

        HeadlineKeluarDariScript.text = "--------------------------";

        for (int i = gridParent.transform.childCount - 1; i > 0; i--)
        {
            DestroyImmediate(gridParent.transform.GetChild(i).gameObject);
        }
    }

    private void PanelNgaleh()
    {
        LeanTween.moveLocalX(panel, 1737f, 1f)
            .setEaseInBack();
    }

    IEnumerator KorotinUntukAnimasiMengancurkanPrefabdanAnimasiHehe()
    {
        TriggerAnimasiTerbangkeKanan();
        AnginKeKanan.Play();
        yield return new WaitForSeconds(3.0f);
        AnginKeKanan.Stop();
        AnimasiJalankah = false;
        LeanTween.rotate(Balud, Vector3.zero, 1)
            .setEaseInOutBack();
        HancurkanPrefabHahahaha();
        MainFunctionPerulanganMasuk();
    }

    private void TriggerAnimasiTerbangkeKanan()
    {
        AnimasiJalankah = true;
        StartCoroutine(BalonMiringJam1lewat35());
        Debug.Log("Omaga sampai sini");
        //StartCoroutine(AnimasiAwanKeKananTerbangPanelnya());

    }

    //IEnumerator AnimasiAwanKeKananTerbangPanelnya()
    //{
    //    speedAwan.speed *= 3;
    //    yield return new WaitForSeconds(3f);
    //    speedAwan.speed *= 1;
    //}

    IEnumerator BalonMiringJam1lewat35()
    {
        LeanTween.rotate(Balud, new Vector3(0, 0, -15.48f), 0.4f);
        yield return null;
    }



}


