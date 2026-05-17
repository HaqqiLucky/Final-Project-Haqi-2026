using DG.Tweening;
using System.Collections;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

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


    [Header("Audio Backsound - Audio Source LoginScene")]
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioClip backsoundLogin1;




    [SerializeField] private ParticleSystem anginKeKiri;

    //[SerializeField] private SliderController sliderBool;

    public bool keMainMenu = false;


    void Start()
    {
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
            Debug.Log("Ayok ke main menu");
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
