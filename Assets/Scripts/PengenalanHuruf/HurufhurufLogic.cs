using TMPro;
using UnityEngine;

public class HurufhurufLogic : MonoBehaviour
{
    private Transform ParentHurufCanvas;
    
    private char hurufRandomed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }   

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PenghancurPrefab"))
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("PrefabGoneNow"))
        {

            PembacaTeks(this.gameObject);
            Destroy(this.gameObject);
        }

    }

    //private void RandomLetter()
    //{
    //    int acak = Random.Range(0, abcd.Length);
    //    hurufRandomed = abcd[acak];
    //}

    private string PembacaTeks(GameObject go )
    {
        string hallucination = go.GetComponent<TextMeshProUGUI>().text;
        Debug.Log(hallucination);
        return hallucination;
    }


}
