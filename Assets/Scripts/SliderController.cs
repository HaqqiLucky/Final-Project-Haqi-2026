using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderController : MonoBehaviour,
    IDragHandler, IEndDragHandler
{
    [SerializeField] private Slider slider;
    public float playingPingPong = 0f;

    [SerializeField] private LoginSceneScenario login;


    public void OnDrag(PointerEventData data)
    {


        float movingDirectionandSpeedDelta = data.delta.x / 40;
        

        playingPingPong += movingDirectionandSpeedDelta;
        slider.value = Mathf.PingPong(playingPingPong, slider.maxValue);
        
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (slider.value > 95)
        {
            slider.value =  Mathf.Lerp(slider.value, slider.maxValue, 1);
            StartCoroutine(login.WakeMeUpInside());
        }
    }

}

