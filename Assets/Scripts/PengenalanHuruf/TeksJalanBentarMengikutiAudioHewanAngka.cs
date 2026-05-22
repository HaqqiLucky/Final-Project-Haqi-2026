using System.Collections;
using System.Drawing;
using TMPro;
using UnityEngine;

public class TeksJalanBentarMengikutiAudioHewanAngka : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI TulisanDiBagianBawah;
    [SerializeField] private float JedaPertamaBanget;
    [SerializeField] private string TeksPertama;
    [SerializeField] private float JedaPertamaKedua;
    [SerializeField] private string TeksKedua;
    [SerializeField] private float JedaKeduaKetiga;
    [SerializeField] private string TeksKetiga;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GantiWarna(JedaPertamaBanget,TeksPertama, JedaPertamaKedua, TeksKedua, JedaKeduaKetiga, TeksKetiga));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GantiWarna(float jedaPertamaBanget,string teksPertama, float jedaPertamaKedua, string teksKedua, float jedaKeduaKetiga = 0, string teksKetiga = "")
    {
        yield return new WaitForSeconds(jedaPertamaBanget);
        TulisanDiBagianBawah.text = teksPertama;
        //yield return new WaitForSeconds(jedaPertamaKedua);
        //TulisanDiBagianBawah.text = teksKedua;
        if (!string.IsNullOrEmpty(TeksKedua) && JedaPertamaKedua > 0)
        {
            yield return new WaitForSeconds(JedaPertamaKedua);
            TulisanDiBagianBawah.text = TeksKedua;
        }

        if (!string.IsNullOrEmpty(TeksKetiga) && JedaKeduaKetiga > 0)
        {
            yield return new WaitForSeconds(JedaKeduaKetiga);
            TulisanDiBagianBawah.text = TeksKetiga;
        }
    }
}


//<color=#65FF23FF>A<color=#FFFFFFFF>pel

//A itu warna ijo sisanya putih


//<color=#FFFFFFFF>A<color=#65FF23FF>pel 


//< color =#65FF23FF>Xi<color=#FFFFFFFF>lo<color=#FFFFFFFF>fon
//< color =#FFFFFFFF>Xi<color=#65FF23FF>lo<color=#FFFFFFFF>fon
//< color =#FFFFFFFF>Xi<color=#FFFFFFFF>lo<color=#65FF23FF>fon