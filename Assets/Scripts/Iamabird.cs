using UnityEngine;
using UnityEngine.SceneManagement;

public class Iamabird : MonoBehaviour
{
    private float journeyBird = 0f;
    [SerializeField] private float speed = 0.5f;
    private Vector3 posAwal;
    private Vector3 posAkhir;

    private void Start()
    {
        //int indexSceneScenario = SceneManager.GetActiveScene().buildIndex;

        posAkhir = new Vector3(9.55f, transform.position.y, transform.position.z);
        posAwal = transform.position;
        //if (indexSceneScenario == 3 &&  )
    }

    void Update()
    {
        journeyBird += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(posAwal, posAkhir, journeyBird);
        if (transform.position.x >= 9.23f)
        {
            transform.position = posAwal;
            journeyBird = 0f;
        }
    }
}
