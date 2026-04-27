using System.Collections.Generic; // buat list
using UnityEngine;
using UnityEngine.UI;


public class PuzzleController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject puzzle1House;
    [SerializeField] private Slider sliderValue;

    // save save an

    //private List<Vector3> puzzle1HouseList = new List<Vector3>();
    //private List<Vector3> puzzle1HouseListRandomed = new List<Vector3>();
    //private List<RectTransform> puzzle1HouseListPrefabs = new List<RectTransform>();

    private Vector3[] puzzle1PosArray;
    private Vector3[] puzzle1PosArrayRandomized;

    void Start()
    {
        //puzzle1PosArray = null;
        //PuzzleOneHouse();
        //Debug.Log(puzzle1House);
        //PreparingPuzzle();
        InsertPuzzlePositionIntoArray();
        RandomPuzzlePosition(); 
    }

    void Update()
    {
        float t = sliderValue.value / 100f;
        for (int i = 0; i < puzzle1House.transform.childCount; i++)
        {
            Transform child = puzzle1House.transform.GetChild(i);
            child.position = Vector3.Lerp(puzzle1PosArrayRandomized[i], puzzle1PosArray[i], t);
        }

    }

    void InsertPuzzlePositionIntoArray()
    {
        int allChild = puzzle1House.transform.childCount;
        puzzle1PosArray = new Vector3[allChild];

        for (int i = 0; i < allChild; i++)
        {
            Transform child = puzzle1House.transform.GetChild(i);
            puzzle1PosArray[i] = child.position;

            //Debug.Log($"Stored child {i} at position: {puzzle1PosArray[i]}");
        }
    }   

    void RandomPuzzlePosition()
    {
        int allChild = puzzle1House.transform.childCount;
        puzzle1PosArrayRandomized = new Vector3[allChild];

        for (int i = 0; i < allChild; i++)
        {
            float randomX = Random.Range(-7f, 7f);
            Transform childPosToRandom = puzzle1House.transform.GetChild(i);

            Vector3 randomNewPos = new Vector3(childPosToRandom.position.x + randomX, childPosToRandom.position.y, childPosToRandom.position.z);
            childPosToRandom.position = randomNewPos;
            puzzle1PosArrayRandomized[i] = randomNewPos;
        }
    }


    //void PuzzleOneHouse()
    //{
    //    foreach (RectTransform child in puzzle1House.transform)
    //    {
    //        Vector3 positionxyz = child.position;

    //        puzzle1HouseList.Add(positionxyz);
    //        puzzle1HouseListPrefabs.Add(child);
    //    }
    //}

    //void PreparingPuzzle()
    //{
    //    for (int i = 0; i < puzzle1HouseList.Count; i++)
    //    {
    //        float randomX = Random.Range(-500f, 500f); // jarak acak

    //        Vector3 posX = puzzle1HouseList[i];
    //        posX.x += randomX;

    //        puzzle1HouseListRandomed.Add(posX);
    //    }
    //    RectTransform[] puzzle1HouseListPrefabs = puzzle1House.GetComponentInChildren<RectTransform>();
    //    foreach (Rect)
    //}

    //void PuzzleSlider()
    //{
    //    for (int i = 0; i < puzzle1HouseListPrefabs.Count; i++)
    //    {
    //        puzzle1HouseListPrefabs[i].localPosition = Vector3.Lerp(puzzle1HouseListRandomed[i], puzzle1HouseList[i], slider.playingPingPong);
    //    }
    //}

}