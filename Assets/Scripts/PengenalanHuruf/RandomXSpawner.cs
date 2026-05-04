using System.Collections;
using TMPro;
using UnityEngine;

public class RandomXSpawner : MonoBehaviour
{

    [SerializeField] GameObject TeksPrefab;
    private string[] hurufBesar = new string[]
        {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
        "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
        };
    [SerializeField] float SecondSpawn;
    [SerializeField] float minTras;
    [SerializeField] float maxTras;
    private Transform ParentHurufCanvas;

    void Start()
    {
        StartCoroutine(SpawnRandomHuruf());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        //Debug.Log("bang")
        GameObject CanvasGO = GameObject.Find("CanvasRoot");
        ParentHurufCanvas = CanvasGO.GetComponent<Transform>();

    }

    IEnumerator SpawnRandomHuruf()
    {
        while (true)
        {
            var wantedX = Random.Range(minTras, maxTras);

            // 1. Spawn dulu tanpa posisi (otomatis nempel ke parent)
            GameObject go = Instantiate(TeksPrefab, ParentHurufCanvas);

            // 2. ATUR POSISI LOKAL (Relatif terhadap Canvas)
            // Gunakan transform.localPosition.y agar dia spawn setinggi spawner ini
            go.transform.localPosition = new Vector3(wantedX, transform.localPosition.y, 0);

            // 3. Set teks
            string randomChar = hurufBesar[Random.Range(0, hurufBesar.Length)];
            go.GetComponent<TextMeshProUGUI>().text = randomChar;

            yield return new WaitForSeconds(SecondSpawn);
        }
    }
}
