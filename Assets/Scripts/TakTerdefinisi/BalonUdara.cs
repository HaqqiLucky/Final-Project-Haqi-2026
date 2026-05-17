using UnityEngine;

public class BalonUdara : MonoBehaviour
{
    private LoginSceneScenario sceneControl;
    private bool sedangGerakKanan = false;

    void Start()
    {
        sceneControl = Object.FindAnyObjectByType<LoginSceneScenario>();

        // 1. Mulai game: posisi tegak (0,0,0) dan langsung gerak naik turun
        transform.rotation = Quaternion.identity;
        GerakAtasBawah();
    }

    void Update()
    {
        if (sceneControl == null) return;

        if (sceneControl.keMainMenu == true && !sedangGerakKanan)
        {
            sedangGerakKanan = true;
            GerakKeKanan();
        }
    }

    private void GerakAtasBawah()
    {
        // FOKUS HANYA SUMBU Y: Biarkan ini nge-loop ping-pong selamanya
        LeanTween.moveY(this.gameObject, transform.position.y + 0.2f, 1.5f)
            .setEase(LeanTweenType.easeInOutQuad)
            .setLoopPingPong();
    }

    private void GerakKeKanan()
    {
        // 2. Saat maju, balon miring ke -15.48 derajat dalam waktu 0.4 detik
        LeanTween.rotate(this.gameObject, new Vector3(0, 0, -15.48f), 0.4f);

        // 3. Balon maju ke kanan di sumbu X
        LeanTween.moveX(this.gameObject, transform.position.x - 4f, 2f)
            .setEase(LeanTweenType.easeInQuad)
            .setOnComplete(() => {
                 LeanTween.rotate(this.gameObject, Vector3.zero, 0.5f);
            });
    }
}