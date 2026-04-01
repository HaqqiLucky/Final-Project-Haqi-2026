using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PengenalanAngkaSceneControl : MonoBehaviour
{
    [Header("GameObject Important")]
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject prefabsGridElement;
    [SerializeField] private GameObject parentofAllEntities;
    [SerializeField] private GameObject gridParent;
    private GameObject[] Animals;
    private int currentAngka = 1;
    private string namaHewan;
    private int JumlahHewanYangDiklik = 0;
    

    [Header("Gameobject not really important")]
    public AkuTidakMauTahuIniHarusMengestuckAnimasiDisabled butonKlik;
    [SerializeField] private TextMeshProUGUI HeadlineKeluarDariScript;
    [SerializeField] private ParticleSystem Confentii;
    [SerializeField] private PergerakanBalonUdara gerakBalud;



    void Start()
    {
        Camera.main.gameObject.transform.position = Vector3.zero;
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
        int currentAngkaBuatDiPakediFunction = currentAngka - 1;
        int jumlahButton = currentAngkaBuatDiPakediFunction;
        //RandomAnimals().SetActive(true);
        RandomAnimals().transform.SetParent(prefabsGridElement.transform, false);
        prefabsGridElement.transform.GetChild(0).gameObject.SetActive(true);
        prefabsGridElement.transform.GetChild(0).localPosition = Vector3.zero;
        if (prefabsGridElement.transform.childCount == 1)
        {
            Debug.Log("masuk if");
            for (int i = 1; i < currentAngkaBuatDiPakediFunction; i++)
            {
                Debug.Log("masuk for");
                Instantiate(prefabsGridElement, new Vector3(0, 0, 0), Quaternion.identity, gridParent.transform);
                Debug.Log("done for");
            }
        } else
        {
            Debug.Log("Gatau ah");
        }

    }


    private void Headline()
    {
        string entitySekarang = prefabsGridElement.transform.GetChild(0).name;
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
        HeadlineKeluarDariScript.text = JumlahHewanYangDiklik.ToString()+ " " + namaHewan;
        //JumlahHewanYangDiklik = butonKlik.buttonYgSdhDiKlik.ToString();
        Debug.Log("Ini Jumlah hewan yg di kklik : " + JumlahHewanYangDiklik);

        if (currentAngka == JumlahHewanYangDiklik)
        {
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
        LeanTween.moveY(Camera.main.gameObject, 12, 1f);
    }

    private void PanelNgaleh()
    {
        LeanTween.moveLocalX(panel, 1737f, 1f)
            .setEaseInBack();
    }


}
