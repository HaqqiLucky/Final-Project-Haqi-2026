using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenSceneControl : MonoBehaviour
{
    public static LoadingScreenSceneControl Instance;

    [Header("UI References")]
    public GameObject m_LoadingScreenObject;
    public Slider ProgressBar;
    public CanvasGroup canvasGroup;
    private bool _isLoading = false;

    [Header("LoadingAudio")]
    [SerializeField] private AudioSource LoadingAudioSource;
    [SerializeField] private AudioClip Sound;

    private void Awake()
    {
        // Cukup set instance ke diri sendiri setiap kali scene baru di-load
        Instance = this;

        // Pastikan loading screen tertutup saat awal
        if (m_LoadingScreenObject != null)
        {
            m_LoadingScreenObject.SetActive(false);
        }
    }


    // Panggil fungsi ini dari button atau script lain
    // Contoh: LoadingScreenSceneControl.Instance.LoadScene("PuzzleScene");
    public void LoadScene(string sceneName)
    {
        StopAllCoroutines(); // Menghindari tumpang tindih coroutine
        StartCoroutine(SwitchToSceneAsync(sceneName));
    }

    IEnumerator SwitchToSceneAsync(string nameSceneId)
    {
        _isLoading = true;
        LoadingAudioSource.PlayOneShot(Sound);
        // 1. Munculkan Overlay
        m_LoadingScreenObject.SetActive(true);
        if (canvasGroup != null) canvasGroup.alpha = 1f;
        ProgressBar.value = 0;

        // 2. Mulai Load Scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nameSceneId);
        asyncLoad.allowSceneActivation = false;

        // 3. Animasi Progress
        while (asyncLoad.progress < 0.9f)
        {
            float targetProgress = asyncLoad.progress / 0.9f;
            ProgressBar.value = Mathf.MoveTowards(ProgressBar.value, targetProgress, Time.deltaTime * 0.5f);
            yield return null;
        }

        // 4. Fake Loading sampai 100%
        while (ProgressBar.value < 1f)
        {
            ProgressBar.value = Mathf.MoveTowards(ProgressBar.value, 1f, Time.deltaTime * 0.8f);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // 5. Aktivasi Scene
        asyncLoad.allowSceneActivation = true;

        // Tunggu sampai scene baru benar-benar aktif
        while (!asyncLoad.isDone) yield return null;

        // 6. Fade Out
        if (canvasGroup != null)
        {
            float fadeDuration = 0.5f;
            float startAlpha = canvasGroup.alpha;
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, t / fadeDuration);
                yield return null;
            }
            canvasGroup.alpha = 0;
        }

        m_LoadingScreenObject.SetActive(false);
        _isLoading = false;
    }
    public void SwitchToScene(string scenaName)
    {
        if (_isLoading) return; // Jika lagi loading, abaikan perintah baru

        m_LoadingScreenObject.SetActive(true);
        StartCoroutine(SwitchToSceneAsync(scenaName));
    }
}