using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AkuTidakMauTahuIniHarusMengestuckAnimasiDisabled : MonoBehaviour
{
    private Button theButton;
    [SerializeField] private GameObject backrgroundAngka;
    Vector3 posisiSekarang;
    //[SerializeField] GameObject SirkelBackgroundAngka;
    public PengenalanAngkaSceneControl SceneControlMainScript;
    [SerializeField] TextMeshProUGUI number;

    void Awake()
    {
        theButton = GetComponent<Button>();

    }

    public void OnButtonClick()
    {
        //Debug.Log("Button diklik!");
        theButton.interactable = false;
        posisiSekarang = transform.position;
        //Debug.Log("Posisi skrng disini yh : " + posisiSekarang);
        AngkaNaikKaloDiPencethehe();
        SceneControlMainScript.EveryButtonClicked();

    }



    private void AngkaNaikKaloDiPencethehe()
    {
        backrgroundAngka.SetActive(true);
        //Instantiate(backrgroundAngka, posisiSekarang + new Vector3(-1f, 0f, 0f), Quaternion.identity);
        int a = 1 + SceneControlMainScript.JumlahHewanYangDiklik;
        number.text = a.ToString();
        //backrgroundAngka.transform.position = posisiSekarang + new Vector3(-1f, 0f, 0f);

        //Instantiate(backrgroundAngka, backrgroundAngka.transform.position, Quaternion.identity);
    }


}
