using UnityEngine;
using UnityEngine.UI;

public class SliderTimer : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private bool TimerActive = false;
    [SerializeField] private MengurutkanAngkaSceneControl sceneControl;
    public float Durasi = 60;
    

    void Start()
    {

        slider.value = Durasi;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerActive)
        {
            Durasi -= Time.deltaTime;
            slider.value = Durasi;
            if (Durasi <= 0)
            {
                Durasi = 0;
                TimerActive = false;
                StartCoroutine(sceneControl.MengurutkanAngkaSelesai());
            }
        }
    }

    public void StartTimer()
    {
        TimerActive = true;
    }

    public void StopTimer()
    {
        TimerActive = false;
    }
}
