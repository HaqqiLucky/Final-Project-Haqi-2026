using UnityEngine;
using UnityEngine.UI;

public class AkuTidakMauTahuIniHarusMengestuckAnimasiDisabled : MonoBehaviour
{
    private Button theButton;
    [SerializeField] public GameObject backrgroundAngka;
    Vector3 posisiSekarang;
    //public int buttonYgSdhDiKlik = 0;
    public PengenalanAngkaSceneControl SceneControlMainScript;


    void Awake()
    {
        theButton = GetComponent<Button>();

    }

    public void OnButtonClick()
    {
        Debug.Log("Button diklik!");
        theButton.interactable = false;
        //backrgroundAngka.SetActive(true);
        posisiSekarang = transform.position;
        Debug.Log("Posisi skrng disini yh : " + posisiSekarang);
        AngkaNaikKaloDiPencethehe();
        SceneControlMainScript.EveryButtonClicked();
        //buttonYgSdhDiKlik++;
        //UpdateAngka();
    }

    private void AngkaNaikKaloDiPencethehe()
    {
        backrgroundAngka.SetActive(true);
        backrgroundAngka.transform.position = posisiSekarang + new Vector3(-50f, 50f, 0f);
    }


}
