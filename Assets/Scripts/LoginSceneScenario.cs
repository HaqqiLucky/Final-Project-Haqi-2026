using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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






    //[SerializeField] private SliderController sliderBool;




    void Start()
    {
        moodletEmoji.SetActive(true);
        //StartCoroutine(SkenarioLogin());
        //StartCoroutine(SkenarioHasLogin());
        //AnimatorBalonUdara();
        
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
        MoodletBacksoundSoundEffectController.InstanceMoodlet.ChangeMoodlet(MoodletBacksoundSoundEffectController.MoodletState.confetii);
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
        MoodletBacksoundSoundEffectController.InstanceMoodlet.ChangeMoodlet(MoodletBacksoundSoundEffectController.MoodletState.happy);
        //Debug.Log("background aman");
        //Debug.Log("masuk poit");
        //GoingToMenu();

    }

    void GoingToMenu()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame && tekanUntukMulai.gameObject.activeSelf)
        {
            StartCoroutine(ItsGoingDown());
        }
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
        //LoadingScreenSceneControl.TargetSceneName = "HomeScene";
        LoadingScreenSceneControl.Instance.LoadScene("HomeScene");
        // SceneManager.LoadScene("LoadingScene"); // loading scene
        //LoadingScreenSceneControl.Instance.SwitchToScene(1);
    }



}
