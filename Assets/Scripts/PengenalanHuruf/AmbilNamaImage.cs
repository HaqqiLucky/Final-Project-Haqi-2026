using UnityEngine;
using UnityEngine.UI;

public class AmbilNamaImage : MonoBehaviour
{

    [SerializeField]private Image imageSekarang;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageSekarang = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
