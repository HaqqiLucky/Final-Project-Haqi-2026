using UnityEngine;

public class PergerakanBalonUdara : MonoBehaviour
{
    [SerializeField] private GameObject Balud;
    void Start()
    {
        BalonUdaraTerbangStart( new Vector2(-6.13f, -0.36f), 3f);
    }

    void Update()
    {
        
    }

    private void BalonUdaraTerbangStart(Vector2 position, float time)
    {
        Balud.LeanMove(position, time)
            .setEaseOutBack()
            .setIgnoreTimeScale(true)
            .setDelay(0.25f);
    }


    public void BalonUdaraTerbangHabisSelesai(Vector2 positionTerbangKeatas, float time)
    {
        Balud.LeanMove(positionTerbangKeatas, time)
            .setEaseInBack()
            .setIgnoreTimeScale(true)
            .setDelay(0.25f);
    }
}
