using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoginSceneScenario : MonoBehaviour
{
    [Header("Background")]
    [SerializeField] private GameObject backgroundSky;
    [SerializeField] private GameObject backgroundLogin;
    [Header("animation")]
    //[SerializeField] private GameObject fadeIn;
    //[SerializeField] private GameObject fadeOut;
    [SerializeField] private GameObject fadeInOutCanvasForCircle;
    private Animator animBalonUdara;
    private bool isTransitioning = false;

    [Header("object")]
    [SerializeField] private GameObject balonUdara;
    [SerializeField] private GameObject puzzles;
    //[SerializeField] private GameObject sliderControl;
    [SerializeField] private TMPro.TextMeshProUGUI tekanUntukMulai;
    [SerializeField] private GameObject moodletEmoji;
    [SerializeField] private CanvasGroup ButtonsHomeScreen;
    [SerializeField] private RectTransform panelHurufRect;
    [SerializeField] private RectTransform panelAngkaRect;
    [SerializeField] private GameObject LayarHitam;
    [SerializeField] private GameObject MouseYuhuu;


    [Header("Audio Backsound - Audio Source LoginScene")]
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioClip backsoundLogin1;




    [SerializeField] private ParticleSystem anginKeKiri;

    //[SerializeField] private SliderController sliderBool;

    public bool keMainMenu = false;
    [SerializeField] private CanvasGroup LayarPertama;

    private void SlowlyFade()
    {
        LeanTween.value(LayarPertama.gameObject,1,0, 2f )
            .setEaseInOutExpo()
            .setOnUpdate((float val) =>
            {
                LayarPertama.alpha = val;
            })
            .setOnComplete(() =>
            {
                LayarPertama.gameObject.SetActive(false);
                KlikVisual();
            });
    }

    private void KlikVisual()
    {
        MouseYuhuu.SetActive(true);
        LeanTween.scale(MouseYuhuu, new Vector2(1.1f, 1.1f), 1f)
                .setEaseInOutSine()
                .setLoopPingPong();
        //MouseYuhuu.GetComponent<Animator>().Play("Mouse");
    }
    void Start()
    {
        LayarPertama.gameObject.SetActive(true);
        //SlowlyFade();
        Invoke("SlowlyFade", 0.5f);
        ButtonsHomeScreen.gameObject.SetActive(false);
        moodletEmoji.SetActive(true);
        BacksoundLogin();
    }

    //void AnimatorBalonUdara()
    //{
    //    animBalonUdara = balonUdara.GetComponent<Animator>();
    //    Debug.Log("anim balud msuk");
    //    StartCoroutine(SkenarioLogin(animBalonUdara));
    //}

    void Update()
    {
        if (!isTransitioning)
        {
            GoingToMenu();
        }
    }

    public IEnumerator WakeMeUpInside()
    {
        //MoodletBacksoundSoundEffectController.InstanceMoodlet.ChangeMoodlet(MoodletBacksoundSoundEffectController.MoodletState.confetii);
        //StartCoroutine(SkenarioHasLogin());
        MouseYuhuu.SetActive(false);
        fadeInOutCanvasForCircle.SetActive(true);
        //Debug.Log("masuk skenario ini");
        yield return new WaitForSeconds(1f);
        //AnimatorBalonUdara();
        //sliderControl.SetActive(true);
        StartCoroutine(SkenarioLogin());
        //Debug.Log("masuk skenario login");
    }

    IEnumerator SkenarioLogin()
    {
        backgroundSky.SetActive(true);
        //Debug.Log("background amandddddd");
        yield return new WaitForSeconds(1f);
        balonUdara.SetActive(true);
        yield return new WaitForSeconds(4f);
        tekanUntukMulai.gameObject.SetActive(true);
        //MoodletBacksoundSoundEffectController.InstanceMoodlet.ChangeMoodlet(MoodletBacksoundSoundEffectController.MoodletState.happy);
        //Debug.Log("background aman");
        //Debug.Log("masuk poit");
        //GoingToMenu();

    }

    void GoingToMenu()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame && tekanUntukMulai.gameObject.activeSelf)
        {
            //Debug.Log("Ayok ke main menu");
            StartCoroutine(PindahKeMainMenu());

            //StartCoroutine(ItsGoingDown());
        }
    }

    //private void PerpindahanKeMainMenu()
    //{
    //    keMainMenu = true;

    //}

    IEnumerator PindahKeMainMenu()
    {
        tekanUntukMulai.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        keMainMenu = true;
        anginKeKiri.Play();
        //anginKeKiri.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        keMainMenu = !keMainMenu;
        anginKeKiri.Stop();
        yield return new WaitForSeconds(1f);
        NyalakanButtons();
        //ButtonsHomeScreen.SetActive(true);
        //anginKeKiri.gameObject.SetActive(false);
    }

    private void NyalakanButtons()
    {
        ButtonsHomeScreen.gameObject.SetActive(true);
        LeanTween.value(ButtonsHomeScreen.gameObject, 0, 1, 1f)
            .setOnUpdate((float val) =>
            {
                ButtonsHomeScreen.alpha = val;
            });
    }

    public void NaikanPanelHuruf()
    {
        LayarHitam.SetActive(true);
        LeanTween.moveY(panelHurufRect, 0f, 1.2f)
            .setEase(LeanTweenType.easeOutBack);
    }
    public void NaikanPanelAngka()
    {
        LayarHitam.SetActive(true);
        LeanTween.moveY(panelAngkaRect, 0f, 1.2f)
            .setEase(LeanTweenType.easeOutBack);
    }

    public void LayarHitamDiklik()
    {

        LayarHitam.SetActive(false);
        LeanTween.moveY(panelAngkaRect, -1500f, 1.2f)
            .setEase(LeanTweenType.easeOutBack);

        LeanTween.moveY(panelHurufRect, -1500f, 1.2f)
            .setEase(LeanTweenType.easeOutBack);
    }

    public void KePengenalanHuruf()
    {
        LoadingScreenSceneControl.Instance.LoadScene("PengenalanHuruf");
    }
    public void KePengenalanAngka()
    {
        LoadingScreenSceneControl.Instance.LoadScene("PengenalanAngka");
    }
    public void KeGameHuruf()
    {
        LoadingScreenSceneControl.Instance.LoadScene("GamePengenalanHuruf");
    }
    public void KeGameAngka()
    {
        LoadingScreenSceneControl.Instance.LoadScene("MengurutkanAngka");
    }
    public void KeCreditScene()
    {
        LoadingScreenSceneControl.Instance.LoadScene("CreditScene");
    }
    public void Keluar()
    {
        Application.Quit();
    }

    IEnumerator ItsGoingDown()
    {
        isTransitioning = true;
        //Debug.Log("its about to going down");   
        yield return new WaitForSeconds(1);
        tekanUntukMulai.gameObject.SetActive(false);
        //fadeIn.SetActive(true);
        yield return new WaitForSeconds(3);
        //SceneManager.LoadScene(1);

        bgm.DOFade(0, 2f).OnComplete(() => {
            LoadingScreenSceneControl.Instance.LoadScene("HomeScene");
        });

        //LoadingScreenSceneControl.TargetSceneName = "HomeScene";
        
        // SceneManager.LoadScene("LoadingScene"); // loading scene
        //LoadingScreenSceneControl.Instance.SwitchToScene(1);
    }

    private void BacksoundLogin()
    {
        //backsoundLoginWillbePlayed = Random.
        bgm.clip = backsoundLogin1;
        bgm.loop = true;
        bgm.Play();
    }

}
