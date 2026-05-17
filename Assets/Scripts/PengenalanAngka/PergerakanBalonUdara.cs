using UnityEngine;

public class PergerakanBalonUdara : MonoBehaviour
{
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        // Target posisi X = 0, Y = -42f
        BalonUdaraTerbangStart(new Vector2(0f, -42f), 3f);
    }

    private void BalonUdaraTerbangStart(Vector2 position, float time)
    {
        LeanTween.cancel(rectTransform);


        LeanTween.move(rectTransform, position, time)
            .setEaseOutBack()
            .setIgnoreTimeScale(true)
            .setDelay(0.25f);
    }

    public void BalonUdaraTerbangHabisSelesai(Vector2 positionTerbangKeatas, float time)
    {
        LeanTween.cancel(rectTransform);

        // PERUBAHAN DI SINI JUGA
        LeanTween.move(rectTransform, positionTerbangKeatas, time)
            .setEaseInBack()
            .setIgnoreTimeScale(true)
            .setDelay(0.25f);
    }
}