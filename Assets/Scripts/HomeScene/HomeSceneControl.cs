using System.Collections;
using UnityEngine;

public class HomeSceneControl : MonoBehaviour
{
    [Header("Kebutuhan Script")]
    [SerializeField] private GameObject Baloon;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float t;

    [Header("GameObject")]
    [SerializeField] private GameObject MenuToChangeColorBaloon;
    [SerializeField] private GameObject ClickableObjectAtWorldSpace;
    [SerializeField] private GameObject PaperBackgroundItem;
    [SerializeField] private GameObject PaperBackgroundCantBeSelected;
    [SerializeField] private GameObject ButtonsInPuzzleMenu;
    [SerializeField] private GameObject AwanOut;
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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AwanOut.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 balonUdaraAwal = Baloon.transform.position;
        Vector3 balonUdaraAkhir = target.position;
        Baloon.transform.position = Vector3.MoveTowards(balonUdaraAwal, Vector3.Lerp(balonUdaraAwal, balonUdaraAkhir, t), speed);

        
    }


    public void OpenChangeBaloon()
    {
        if (MenuToChangeColorBaloon == true)
        {
            AnimasiLeanTweenDiUI();
            ClickableObjectAtWorldSpace.SetActive(false);
        } else
        {
            
        }
    }

 
    private void AnimasiLeanTweenDiUI()
    {
        MenuToChangeColorBaloon.SetActive(true);
        PaperBackgroundItem.LeanMoveLocal(PaperTargetIn, 1f)
            .setEaseInOutBack();
        PaperBackgroundCantBeSelected.LeanMoveLocal(PaperTargetCantBeSelectedIn, 1f)
            .setEaseInOutBack();
    }


    public void ClickedCancel()
    {
        PaperBackgroundItem.LeanMoveLocal(PaperTargetOut, 1f)
            .setEaseInOutBack();

        PaperBackgroundCantBeSelected.LeanMoveLocal(PaperTargetCantBeSelectedOut, 1f)
            .setEaseInOutBack();

        ClickableObjectAtWorldSpace.SetActive(true);
    }


    public void PuzzleMenuBalloonOnClick()
    {
        ClickableObjectAtWorldSpace.SetActive(false);
        PaperPuzzleMenu.LeanMoveLocal(PaperTargetPuzzleMenuIn, 1.2f)
            .setEaseInOutBack()
            .setOnComplete(() =>
            {
            ButtonsInPuzzleMenu.SetActive(true);
            });;
        //LeanTween.alphaCanvas(ButtonsInPuzzleMenu, 1f, 0.5f);


    }

    public void PuzzleMenuBalloonXClick()
    {
        ClickableObjectAtWorldSpace.SetActive(true);
        PaperPuzzleMenu.LeanMoveLocal(PaperTargetPuzzleMenuOut, 1.2f)
            .setEaseInOutBack()
            .setOnComplete(() =>
            {
                ButtonsInPuzzleMenu.SetActive(false);
            });
        //LeanTween.alphaCanvas(ButtonsInPuzzleMenu, 0f, 0.5f)
        //    .setDelay(2.0f);
    }

    //IEnumerator HomeSceneScenario()
    //{
    //    yield return new WaitForSeconds(1f);

    //}

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

    //public void FlyUpHigh()
    //{
    //    StartCoroutine(KameraNaik());
    //}
    //public void FlyDown()
    //{
    //    StartCoroutine(KameraTurun());
    //}

    //public void KameraTurun()
    //{
    //    Camera.LeanMoveLocal(TargetKameraOut, 1f)
    //        .setEaseInOutBack();
    //}

}
