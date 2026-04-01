using UnityEngine;

public class Iamabird : MonoBehaviour
{
    private float journeyBird = 0f;
    [SerializeField] private float speed = 0.5f;
    private Vector3 posAwal;
    private Vector3 posAkhir;

    private void Start()
    {
        posAkhir = new Vector3(9.55f, transform.position.y, transform.position.z);
        posAwal = transform.position;
        //Debug.Log("pos awal : " + posAwal);
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
