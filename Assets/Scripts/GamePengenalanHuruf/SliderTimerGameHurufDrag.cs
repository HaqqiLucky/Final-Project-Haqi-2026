using UnityEngine;
using UnityEngine.UI;

public class SliderTimerGameHurufDrag : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private bool TimerActive = false;
    [SerializeField] private GamePengenalanHurufSceneControl sceneControl;
    public float DurasiSekarang = 20;
    private float durasiFlat = 20;


    void Start()
    {
        slider.value = DurasiSekarang;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerActive)
        {
            DurasiSekarang -= Time.deltaTime;
            slider.value = DurasiSekarang;
            if (DurasiSekarang <= 0)
            {
                DurasiSekarang = 0;
                TimerActive = false;
                sceneControl.menggantiSesi = true;
                StartCoroutine(sceneControl.PergantianSesi());
                //StartCoroutine(sceneControl.MengurutkanAngkaSelesai());
            }
        }
    }

    public void StartTimer()
    {
        DurasiSekarang = durasiFlat;
        slider.value = DurasiSekarang;
        TimerActive = true;
    }

    public void StopTimer()
    {
        TimerActive = false;
    }
}

