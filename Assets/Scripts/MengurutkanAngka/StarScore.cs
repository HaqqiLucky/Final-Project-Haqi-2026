using System.Collections;
using TMPro;
using UnityEngine;

public class StarScore : MonoBehaviour
{
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;
    [SerializeField] private TextMeshPro teksSkor3D;
    [SerializeField] private CanvasGroup buttons;
    //private Vector3 BintangBelumNaik = new Vector3();
    [SerializeField] MengurutkanAngkaSceneControl sceneControl;
    void Start()
    {
        //StarScenario();
        //star1.transform.position = new Vector3(transform.position.x, 175f, 0);
        //star2.transform.position = new Vector3(transform.position.x, 175f, 0);
        //star3.transform.position = new Vector3(transform.position.x, 175f, 0);
        StarScenario();
        StartCoroutine(AnimasiCountingSkorDiLayar());


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AnimasiCountingSkorDiLayar()
    {
        //teksSkor = GetComponent
        int targetSkor = sceneControl.HitungSkorAkhir();
        float durasi = 5f;
        float waktuBerjalan = 0f;
        int skorAwal = 0;

        while (waktuBerjalan < durasi)
        {
            waktuBerjalan += Time.deltaTime;

            float lerpPercent = waktuBerjalan / durasi;
            int skorSekarang = Mathf.RoundToInt(Mathf.Lerp(skorAwal, targetSkor, lerpPercent));

            teksSkor3D.text = skorSekarang.ToString();

            yield return null;
        }
        yield return new WaitForSeconds(2f);
        //LeanTween.value(buttons.alpha, 0, 1, 2f);
        LeanTween.alphaCanvas(buttons, 1, 1f);
    }

    private void StarScenario()
    {
        int skor = sceneControl.HitungSkorAkhir();
        //Debug.Log(skor);
        if (skor >= 1000)
        {
            LeanTween.moveY(star1, 2f, 1f)
                .setEaseInOutBack();
        }
        if (skor >= 4900)
        {
            LeanTween.moveY(star2, 2f, 1f)
                .setEaseInOutBack()
                .setDelay(0.5f);
        }
        if (skor >= 8600)   
        {
            LeanTween.moveY(star3, 3f, 1f)
                .setEaseInOutBack()
                .setDelay(1);
        }
    }
}
