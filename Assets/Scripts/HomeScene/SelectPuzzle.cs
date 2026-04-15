using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectPuzzle : MonoBehaviour
{
    public GameObject[] PuzzleOption;
    public int Number;
    [SerializeField] private GameObject PlayButton;
    [SerializeField] private HomeSceneControl home;
    [SerializeField] private GameObject Puzzle;

    public void ChangePuzzle(int Num)
    {
        Debug.Log("masuk puzle");
        for (int i = 0; i < PuzzleOption.Length; i++)
        {
            PuzzleOption[i].SetActive(false);
        }


        Number += Num;
        if (Number > PuzzleOption.Length-1)
        {
            Number = 0;
        }


        if (Number < 0)
        {
            Number = PuzzleOption.Length-1;
        }

        PuzzleOption[Number].SetActive(true);

    }



    public void ConfirmLagiButForButton()
    {
        StartCoroutine(ConfirmationToChangeScene());
    }

    IEnumerator ConfirmationToChangeScene()
    {
        home.PuzzleMenuBalloonXClick();
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(home.KameraNaik());
        Puzzle.SetActive(true);
        Debug.Log("sampai di selek puzel");
    }
}
