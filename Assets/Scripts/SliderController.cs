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
    private bool doneKah = false;
    private bool bolehSad = false;

    [Header("Perubahan rencana")]
    private float waktu = 0f;

    private void Start()
    {
        //MoodletBacksoundSoundEffectController.InstanceMoodlet.ChangeMoodlet(MoodletBacksoundSoundEffectController.MoodletState.wave);
    }

    private void Update()
    {
        sliderBulet.value = waktu;
        sliderPanjang.value = sliderBulet.value * 20;
        if (Mouse.current.leftButton.isPressed )
        {
            
            waktu += Time.deltaTime;
            if (waktu >= 2.4 & waktu < 5)
            {
                //MouseAnimController.InstanceMoodlet.ChangeMoodlet(MouseAnimController.MoodletState.kaget);
                //MoodletBacksoundSoundEffectController.InstanceMoodlet.ChangeMoodlet(MoodletBacksoundSoundEffectController.MoodletState.ok);
                bolehSad = true;
            }
            //if (Mouse.current.leftButton.wasReleasedThisFrame )
            //{
            //    if (waktu <5f)
            //    {
            //        MouseAnimController.InstanceMoodlet.ChangeMoodlet(MouseAnimController.MoodletState.sad);
            //    }
                
            //}

            //Debug.Log("sudah di klik kiri sebanyak " + waktu + " detik");
            if (waktu >= 5f & !doneKah)
            {
                doneKah = true;
                sliderPanjang.value = 100;
             
                StartCoroutine(login.WakeMeUpInside());
                //Debug.Log(this.gameObject);
                //this.gameObject.SetActive(false);

                //kocak lu tb tb ga bisa (lain kali jangan pake this game object, langusng aja spesifik nanti mana yg mw di false)
                sliderBulet.gameObject.SetActive(false);
                sliderPanjang.gameObject.SetActive(false);
            }
        } else if (!Mouse.current.leftButton.isPressed && !doneKah && bolehSad)
        {
            waktu = Mathf.MoveTowards(waktu, 0f, Time.deltaTime);
            //MoodletBacksoundSoundEffectController.InstanceMoodlet.ChangeMoodlet(MoodletBacksoundSoundEffectController.MoodletState.sad);
            //waktu = Mathf.MoveTowards(sliderBulet.value, 0f, Time.deltaTime * 2f);
        }
    }


    //public void OnDrag(PointerEventData data)
    //{


    //    float movingDirectionandSpeedDelta = data.delta.x / 40;


    //    playingPingPong += movingDirectionandSpeedDelta;
    //    sliderPanjang.value = Mathf.PingPong(playingPingPong, sliderPanjang.maxValue);

    //}

    //public void OnEndDrag(PointerEventData data)
    //{
    //    if (sliderPanjang.value > 95)
    //    {
    //        sliderPanjang.value = Mathf.Lerp(sliderPanjang.value, sliderPanjang.maxValue, 1);
    //        StartCoroutine(login.WakeMeUpInside());
    //    }
    //}

}

