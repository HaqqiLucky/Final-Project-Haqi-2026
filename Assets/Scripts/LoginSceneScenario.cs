using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoginSceneScenario : MonoBehaviour
{
    [Header("Background")]
    [SerializeField] private GameObject backgroundSky;
    [SerializeField] private GameObject backgroundLogin;
    [Header("animation")]
    [SerializeField] private GameObject fadeIn;
    //[SerializeField] private GameObject fadeOut;
    [SerializeField] private GameObject fadeInOutCanvasForCircle;
    private Animator animBalonUdara;

    [Header("object")]
    [SerializeField] private GameObject balonUdara;
    [SerializeField] private GameObject puzzles;
    [SerializeField] private TMPro.TextMeshProUGUI tekanUntukMulai;



    //[SerializeField] private SliderController sliderBool;




    void Start()
    {
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

        GoingToMenu();
    }

    public IEnumerator WakeMeUpInside()
    {
        //StartCoroutine(SkenarioHasLogin());
        fadeInOutCanvasForCircle.SetActive(true);
        yield return new WaitForSeconds(1f);
        //AnimatorBalonUdara();
        StartCoroutine(SkenarioLogin());
    }

    IEnumerator SkenarioLogin()
    {
        backgroundSky.SetActive(true);
        Debug.Log("background aman");
        yield return new WaitForSeconds(1f);
        balonUdara.SetActive(true);
        yield return new WaitForSeconds(4f);
        tekanUntukMulai.gameObject.SetActive(true);
        Debug.Log("masuk poit");
        GoingToMenu();

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
        Debug.Log("its about to going down");   
        yield return new WaitForSeconds(1);
        tekanUntukMulai.gameObject.SetActive(false);
        fadeIn.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }

}
