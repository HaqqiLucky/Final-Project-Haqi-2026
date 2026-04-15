using UnityEngine;
using UnityEngine.SceneManagement;

public class IamaBirdbutPengenalanAngka : MonoBehaviour
{
    private float journeyBird = 0f;
    [SerializeField] private float speed = 0.5f;
    private Vector3 posAwal;
    private Vector3 posAkhir;
    [SerializeField] private PengenalanAngkaSceneControl AnimTransisi;
    private int indexSceneScenario;

    private void Start()
    {
        indexSceneScenario = SceneManager.GetActiveScene().buildIndex;
        posAkhir = new Vector3(9.55f, transform.position.y, transform.position.z);
        posAwal = transform.position;
    }
    void Update()
    {
        float currentSpeed = speed;
        if (indexSceneScenario == 3 && AnimTransisi.AnimasiJalankah == true)
        {
            currentSpeed = speed * 2f;
        }

        journeyBird += Time.deltaTime * currentSpeed;

        transform.position = Vector3.Lerp(posAwal, posAkhir, journeyBird);

        if (transform.position.x >= 9.23f)
        {
            transform.position = posAwal;
            journeyBird = 0f;
        }
    }
}
