using UnityEngine;

public class CreditSceneSceneControl : MonoBehaviour
{
    [SerializeField] private RectTransform creditsText;
    [SerializeField] private GameObject ButtonX;
    [SerializeField] private AudioClip bgm1;
    [SerializeField] private AudioSource SpeakerCredit;

    // Titik koordinat Y tujuan (atas layar, misal: 1500 atau 2000 tergantung panjang teks)
    private float targetY = 0;

    // Durasi jalannya teks dalam detik. Makin besar angkanya, makin pelan jalannya!
    private float duration = 388f;

    void Start()
    {

        Invoke("NyalakanButton", 5f);
        // Pastikan posisi awal teks berada di bawah layar (misal Y = -1000)
        // Anda bisa mengaturnya langsung di Inspector RectTransform game object Anda.

        if (SpeakerCredit != null)
        {
            SpeakerCredit.Play(); // Memerintahkan speaker untuk langsung menyala
        }

        // Perintah LeanTween untuk menggerakkan posisi Y UI secara pelan dan konstan
        LeanTween.moveY(creditsText, targetY, duration)
            .setEase(LeanTweenType.linear); 
    }

    //void OnCreditsFinished()
    //{
    //    //Debug.Log("Kredit sudah selesai bergulir!");

    //    LoadingScreenSceneControl.Instance.LoadScene("LoginScene");

    //}

    private void NyalakanButton()
    {
        ButtonX.SetActive(true);
    }
}
