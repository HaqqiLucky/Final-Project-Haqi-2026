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
    public bool udaLogin = false;

    [Header("LoadingAudio")]
    [SerializeField] private AudioSource LoadingAudioSource;
    [SerializeField] private AudioClip Sound;


    private void Awake()
    {
        // SISTEM SINGLETON (Agar objek bisa dibawa kemana-mana tanpa duplikat)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // KUNCI UTAMA: Agar objek tidak hancur saat pindah scene
        }
        else
        {
            Destroy(gameObject); // Jika di scene baru ada objek serupa, hancurkan duplikatnya
            return;
        }

        // Pastikan loading screen tertutup saat awal game
        if (m_LoadingScreenObject != null)
        {
            m_LoadingScreenObject.SetActive(false);
        }
    }

    // Panggil fungsi ini dari button atau script lain
    // Contoh: LoadingScreenSceneControl.Instance.LoadScene("PuzzleScene");
    public void LoadScene(string sceneName)
    {
        if (_isLoading) return; // Menghindari tumpang tindih jika user spam klik tombol
        StartCoroutine(SwitchToSceneAsync(sceneName));
    }

    IEnumerator SwitchToSceneAsync(string nameSceneId)
    {
        _isLoading = true;

        if (LoadingAudioSource != null && Sound != null)
        {
            LoadingAudioSource.PlayOneShot(Sound);
        }

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

        // 5. Aktivasi Scene Baru
        asyncLoad.allowSceneActivation = true;

        // Tunggu sampai scene baru benar-benar aktif selesai di-load
        while (!asyncLoad.isDone) yield return null;

        // 6. Fade Out (Sekarang aman berjalan karena objek tidak dihancurkan)
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

    // Fungsi alternatif yang Anda buat, disesuaikan agar seragam
    public void SwitchToScene(string scenaName)
    {
        LoadScene(scenaName);
    }
}