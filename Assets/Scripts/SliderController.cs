using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
    //IDragHandler, IEndDragHandler
{
    [SerializeField] private Slider sliderPanjang;
    //public float playingPingPong = 0f;
    [SerializeField] private LoginSceneScenario login;
    [SerializeField] private Slider sliderBulet;
    //private bool doneKah = false;
    //private bool bolehSad = false;
    private bool bolehDitekan = false;
    [SerializeField] private TextMeshProUGUI teksBeginner;
    [SerializeField] Animator MouseKiku;
    [SerializeField] GameObject MouseYuhuu; 

    //[Header("Perubahan rencana")]
    //private float waktu = 0f;

    private void Start()
    {
        
    }

    private void Update()
    {

        if (Mouse.current.leftButton.wasReleasedThisFrame & !bolehDitekan)
        {
            MouseYuhuu.SetActive(false);
            bolehDitekan = true;
            LeanTween.value(0f, 100f, 5)
                .setOnUpdate((float val) =>
                {
                    sliderPanjang.value = val;
                    //if (val == 20)
                    //{
                    //    
                    //}
                })
                .setOnComplete(() =>
                {
                    //doneKah = true;
                    sliderPanjang.value = 100;
                    StartCoroutine(login.WakeMeUpInside());
                    sliderBulet.gameObject.SetActive(false);
                    sliderPanjang.gameObject.SetActive(false);
                });
        }

        //    // buat animasi
        //    if (Mouse.current.leftButton.isPressed && !sedangDitekan)
        //    {
        //        sedangDitekan = true;

        //        MouseKiku.SetBool("onclick", true);
        //        teksBeginner.text = "Tahan";
        //    }
        //    // 2. KETIKA DILEPAS (Dan sebelumnya statusnya masih ditekan)
        //    else if (!Mouse.current.leftButton.isPressed && sedangDitekan)
        //    {
        //        sedangDitekan = false;

        //        MouseKiku.SetBool("onclick", false);
        //        teksBeginner.text = "Klik kiri pada mouse";
        //    }

        //    sliderBulet.value = waktu;
        //    sliderPanjang.value = sliderBulet.value * 50;
        //    if (Mouse.current.leftButton.isPressed )
        //    {

        //        waktu += Time.deltaTime;
        //        if (waktu >= 2.4 & waktu < 5)
        //        {
        //            bolehSad = true;
        //        }


        //        //Debug.Log("sudah di klik kiri sebanyak " + waktu + " detik");
        //        if (waktu >= 5f & !doneKah)
        //        {
        //            doneKah = true;
        //            sliderPanjang.value = 100;

        //            StartCoroutine(login.WakeMeUpInside());
        //            //Debug.Log(this.gameObject);
        //            //this.gameObject.SetActive(false);

        //            //kocak lu tb tb ga bisa (lain kali jangan pake this game object, langusng aja spesifik nanti mana yg mw di false)
        //            sliderBulet.gameObject.SetActive(false);
        //            sliderPanjang.gameObject.SetActive(false);
        //        }
        //    } else if (!Mouse.current.leftButton.isPressed && !doneKah && bolehSad)
        //    {
        //        waktu = Mathf.MoveTowards(waktu, 0f, Time.deltaTime);

        //        //MouseKiku.SetBool("onclick", false);
        //        //MoodletBacksoundSoundEffectController.InstanceMoodlet.ChangeMoodlet(MoodletBacksoundSoundEffectController.MoodletState.sad);
        //        //waktu = Mathf.MoveTowards(sliderBulet.value, 0f, Time.deltaTime * 2f);
        //    }
        //}

    }

}

