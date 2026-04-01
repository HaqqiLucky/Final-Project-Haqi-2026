using UnityEngine;
using UnityEngine.UI;

public class PuzzleButHomeScene : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject puzzle1House;
    [SerializeField] private Slider sliderValue;

    private Vector3[] puzzle1PosArray;
    private Vector3[] puzzle1PosArrayRandomized;


    void Start()
    {
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
        }
    }

    void RandomPuzzlePosition()
    {
        int allChild = puzzle1House.transform.childCount;
        puzzle1PosArrayRandomized = new Vector3[allChild];

        for (int i = 0; i < allChild; i++)
        {
            float randomX = Random.Range(-500f, 500f);
            Transform childPosToRandom = puzzle1House.transform.GetChild(i);

            Vector3 randomNewPos = new Vector3(childPosToRandom.position.x + randomX, childPosToRandom.position.y, childPosToRandom.position.z);
            childPosToRandom.position = randomNewPos;
            puzzle1PosArrayRandomized[i] = randomNewPos;
        }
    }

}

