using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TracingHurufSceneControl : MonoBehaviour
{
    [SerializeField] GameObject PanelDanBurung;
    [SerializeField] Image transition;
    [SerializeField] GameObject PanelTracing;
    private string hurufSekarang;

    //public bool sudahKlik = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //TransisiBuka();
        //Invoke("TransisiTutup", 2f);
        PanelTracing.SetActive(false);
        StartCoroutine(StartUpPanelNaik());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StartUpPanelNaik()
    {
        LeanTween.moveLocalY(PanelDanBurung, 0, 4f)
        .setOnComplete(() => {
            LeanTween.moveLocalY(PanelDanBurung, 4f, 2f)
                //.setEaseInOutSine()
                .setLoopPingPong(); 
        });
        yield return null;
    }

    //IEnumerator Transisi()
    //{

    //}

    private void TransisiBuka()
    {
        transition.fillClockwise =  true;
        LeanTween.value(transition.gameObject, transition.fillAmount, 1, 1f)
            .setOnUpdate((float val) => {
                transition.fillAmount = val;
            }).setOnComplete(() =>
            {
                PanelDanBurung.SetActive(false);
            });
    }
    private void TransisiTutup()
    {
        transition.fillClockwise =  false;
        LeanTween.value(transition.gameObject, transition.fillAmount, 0, 1f)
            .setOnUpdate((float val) =>
            {
                transition.fillAmount = val;
                PanelTracing.SetActive(true);
            });
            //.setOnComplete(() =>
            //{

            //});
    }

    public void PendahuluanSetelahButtonDiKlik(string hurufYangDipilih)
    {
        //Debug.Log(hurufYangDipilih + "dari scene control");
        hurufSekarang = hurufYangDipilih.ToString().ToUpper() + hurufYangDipilih.ToString().ToLower();
        //Debug.Log(hurufSekarang);
        TransisiBuka();
        StartCoroutine(StartTuring());
    }


    IEnumerator StartTuring()
    {
        PanelTracing.GetComponentInChildren<TextMeshProUGUI>().text = hurufSekarang;
        yield return new WaitForSeconds(1f);
        TransisiTutup();

    }


    //private void Alfabet()
    //{
    //    //for (char CC = 'A'; CC <= 'Z'; CC++)
    //    //{
    //    //    hurufSekarang += CC.ToString();
    //    //}
    //    //for (char cc = 'a'; cc <= 'z'; cc++)
    //    //{
    //    //    hurufSekarang += cc.ToString();
    //    //}
    //    //Debug.Log(hurufSekarang);
    //}

}
