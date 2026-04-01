using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SliderButHomeScene : MonoBehaviour,
    IDragHandler, IEndDragHandler
{
    [SerializeField] private Slider slider;
    public float playingPingPong = 0f;
    [SerializeField] private SelectPuzzle selectPuzzle;
    private int PuzzleScene;



    public void OnDrag(PointerEventData data)
    {


        float movingDirectionandSpeedDelta = data.delta.x / 40;


        playingPingPong += movingDirectionandSpeedDelta;
        slider.value = Mathf.PingPong(playingPingPong, slider.maxValue);

    }

    public void OnEndDrag(PointerEventData data)
    {
        if (slider.value > 95)
        {
            slider.value = Mathf.Lerp(slider.value, slider.maxValue, 1);
            //StartCoroutine(login.WakeMeUpInside());
            //selectPuzzle.PlayPuzzleThisOne();
            Debug.Log("Should be here");


            // animasi membesar hujan


            PlayPuzzleThisOne();
        }
    }

    public void PlayPuzzleThisOne()
    {
        PuzzleScene = selectPuzzle.Number;
        Debug.Log("Masuk ke play puzzle this one dengan number " + PuzzleScene);
        if (PuzzleScene == 0)
        {
            SceneManager.LoadScene(2);
            Debug.Log("masuk abcd puzzle pengenalan huruf");
        }
        else if (PuzzleScene == 1)
        {
            SceneManager.LoadScene(3);
            Debug.Log("masuk abcd puzzle pengenalan angka");
        }
        else if (PuzzleScene == 2)
        {
            SceneManager.LoadScene(4);
            Debug.Log("masuk abcd puzzle mengurutkan angka");
        }
        else
        {
            SceneManager.LoadScene(5);
            Debug.Log("masuk abcd puzzle hamilton puzzle");
        }
    }
}


