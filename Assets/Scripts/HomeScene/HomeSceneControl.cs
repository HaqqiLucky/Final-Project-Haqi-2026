using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;

public class HomeSceneControl : MonoBehaviour
{
    [Header("Kebutuhan Script")]
    [SerializeField] private GameObject Baloon;
    //[SerializeField] private Transform target;
    //[SerializeField] private float speed;
    //[SerializeField] private float t;
    private bool diKlik = false;

    [Header("GameObject")]
    //[SerializeField] private GameObject MenuToChangeColorBaloon;
    //[SerializeField] private GameObject ClickableObjectAtWorldSpace;
    //[SerializeField] private GameObject PaperBackgroundItem;
    //[SerializeField] private GameObject PaperBackgroundCantBeSelected;
    [SerializeField] private GameObject ButtonsInPuzzleMenu;
    //[SerializeField] private GameObject AwanOut;
    [SerializeField] private GameObject PaperPuzzleMenu;
    [SerializeField] private GameObject Camera;
    [SerializeField] private ParticleSystem Angin;

    [Header("Vector Animasi Paper In")]
    Vector3 PaperTargetIn = new Vector3(493, 497, 0);
    Vector3 PaperTargetCantBeSelectedIn = new Vector3(-609, 340, 0);
    Vector3 PaperTargetPuzzleMenuIn = new Vector3(-1, (float)2.29, (float)-0.75);
    Vector3 TargetKameraIn = new Vector3(0, (float)26.52, -10);

    [Header("Vector Animasi Paper Out")]
    Vector3 PaperTargetOut = new Vector3(-1735, 458, 0);
    Vector3 PaperTargetCantBeSelectedOut = new Vector3(1696, 340, 0);
    Vector3 PaperTargetPuzzleMenuOut = new Vector3(-1, (float)-8.78, (float)-0.75);
    Vector3 TargetKameraOut = new Vector3(0, 0, (float)-10);

    [Header("MainMenuNew")]
    [SerializeField] private GameObject GridMainMenu;
    [SerializeField] private GameObject Puzzled;
    [SerializeField] private GameObject GambarBuatKlikDiluar;
    public int NumberYangDiKlik = 0 ;
    [SerializeField] private GameObject KeluargaButton;
    [SerializeField] private GameObject ClickableHomeBaloon;

    [Header("BacksoundHomeScreen")]
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioClip[] musiks;
    private int indexMusik = 0;

    [Range(0f, 1f)]
    [SerializeField] private float volume = 0.362f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //AwanOut.SetActive(true);
        //MoodletBacksoundSoundEffectController.InstanceMoodlet.ChangeMoodlet(MoodletBacksoundSoundEffectController.MoodletState.happy);
        LeanTween.moveLocal(Baloon, new Vector3(-309, -28, 0f), 4f).setEase(LeanTweenType.easeOutQuart);

        // ini ordering musik
        musiks = musiks.OrderBy(x => Random.value).ToArray();
        PlayNext();

    // Update is called once per frame
    }
    void Update()
    {

        // buat volume
        bgm.volume = volume;
        if (!bgm.isPlaying) PlayNext();



        //if (Mouse.current.leftButton.wasPressedThisFrame)
        //{
        //    // 1. Siapkan data posisi pointer
        //    PointerEventData eventData = new PointerEventData(EventSystem.current);
        //    eventData.position = Mouse.current.position.ReadValue();

        //    // 2. Wadah untuk menampung semua UI yang kena tembak klik
        //    List<RaycastResult> results = new List<RaycastResult>();

        //    // 3. Tembakkan raycast ke semua UI di posisi tersebut
        //    EventSystem.current.RaycastAll(eventData, results);

        //    if (results.Count > 0)
        //    {
        //        // results[0] adalah yang PALING DEPAN (yang menghalangi klik)
        //        Debug.Log("Objek yang kena klik: " + results[0].gameObject.name);

        //        // Kalau kamu mau liat semua lapisan yang kena klik:
        //        foreach (var hit in results)
        //        {
        //            Debug.Log("Di lapisan bawahnya ada: " + hit.gameObject.name);
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("Klik di area kosong (tidak kena UI apapun)");
        //    }
        //}
    }

    // bwat gaca lagu
    private void PlayNext()
    {
        bgm.clip = musiks[indexMusik];
        bgm.Play();
        indexMusik = (indexMusik + 1) % musiks.Length;
    }


    public void PuzzleMenuBalloonOnClick()
    {
        if (diKlik == false)
        {
            diKlik = true;
            PaperPuzzleMenu.LeanMoveLocal(PaperTargetPuzzleMenuIn, 1.2f)
                .setEaseInOutBack()
                .setOnComplete(() =>
                {
                    ButtonsInPuzzleMenu.SetActive(true);
                });
        } else
        {
            diKlik = false;
            PaperPuzzleMenu.LeanMoveLocal(PaperTargetPuzzleMenuOut, 1.2f)
                .setEaseInOutBack()
                .setOnComplete(() =>
                {
                    ButtonsInPuzzleMenu.SetActive(false);
                }); 
        }
    }



    public IEnumerator KameraNaik()
    {
        //Angin.Play();
        Camera.LeanMoveLocal(TargetKameraIn, 1f);
        yield return null;
    }

    public IEnumerator KameraTurun()
    {
        //Angin.Stop();
        Camera.LeanMoveLocal(TargetKameraOut, 1f)
            .setEaseInOutBack();
        yield return null;
    }

    public void NaikkanBendaMainMenu()
    {
        //MoodletBacksoundSoundEffectController.InstanceMoodlet.ChangeMoodlet(MoodletBacksoundSoundEffectController.MoodletState.ok);
        GambarBuatKlikDiluar.SetActive(true);

        LeanTween.moveY(GridMainMenu, 815f, 2f)
            .setEaseInOutBounce();
    }

    public void TurunkanBendaMainMenu()
    {
        //if (NumberYangDiKlik != 0)
        //{
        //    MoodletBacksoundSoundEffectController.InstanceMoodlet.ChangeMoodlet(MoodletBacksoundSoundEffectController.MoodletState.kaget);
        //} else
        //{
        //    MoodletBacksoundSoundEffectController.InstanceMoodlet.ChangeMoodlet(MoodletBacksoundSoundEffectController.MoodletState.yawn);

        //}

        GambarBuatKlikDiluar.SetActive(false);

        LeanTween.moveY(GridMainMenu, -313f, 2f)
            .setEaseInOutBounce();
    }



    // ini scene control litteraly habis ini aku ngatur semua navigasi
    public void AyokKePengenalanAngka(int inputhm)
    {
        Debug.Log(inputhm);
        TurunkanBendaMainMenu();
        StartCoroutine(ConfirmationToChangeScene());
        NumberYangDiKlik = inputhm;
    }
    public void AyokKePengenalanHuruf(int inputhm)
    {
        Debug.Log(inputhm);
        TurunkanBendaMainMenu();
        StartCoroutine(ConfirmationToChangeScene());
        NumberYangDiKlik = inputhm;
    }
    public void AyokKeGameAngka(int inputhm)
    {
        Debug.Log(inputhm);
        StartCoroutine(ConfirmationToChangeScene());
        TurunkanBendaMainMenu();
        NumberYangDiKlik = inputhm;
    }
    public void AyokKeGameHuruf(int inputhm)
    {
        Debug.Log(inputhm);
        TurunkanBendaMainMenu();
        StartCoroutine(ConfirmationToChangeScene());
        NumberYangDiKlik = inputhm;
    }
    public void AyokKCreditScene(int inputhm)
    {
        Debug.Log(inputhm);
        TurunkanBendaMainMenu();
        StartCoroutine(ConfirmationToChangeScene());
        NumberYangDiKlik = inputhm;

    }
    public void AyokKeluar()
    {
        Application.Quit();
    }


    // ini di panggil tiap button di semua main menu di klik

    IEnumerator ConfirmationToChangeScene()
    {
        //home.PuzzleMenuBalloonXClick();
        yield return new WaitForSeconds(3);
        ClickableHomeBaloon.SetActive(false);
        KeluargaButton.SetActive(false);
        yield return StartCoroutine(KameraNaik());
        Puzzled.SetActive(true);
        Debug.Log("sampai di selek puzel");
    }

    //private void KuranginAlphaKeluagaCanvasIni()
    //{
    //    LeanTween.value(KeluargaButton.gameObject, 1f, 0f, 0.2f)
    //        .setOnUpdate((float val) => {
    //            KeluargaButton.alpha = val;
    //        });
    //    LeanTween.value(ClickableHomeBaloon.gameObject, 1f, 0f, 0.2f)
    //        .setOnUpdate((float val) => {
    //            ClickableHomeBaloon.alpha = val;
    //        });

    //}




}
