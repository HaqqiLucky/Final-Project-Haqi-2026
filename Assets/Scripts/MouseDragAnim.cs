using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MouseDragAnim : MonoBehaviour
{
    private TrailRenderer tr;
    private Camera mainCamera;
    private AudioSource klikSound;
    [SerializeField] private AudioClip audioClick;
    [SerializeField] private ParticleSystem psClick;

    void Start()
    {
        tr = GetComponent<TrailRenderer>();
        psClick = GetComponent<ParticleSystem>();
        mainCamera = Camera.main;
        tr.emitting = false;
        klikSound = GetComponent<AudioSource>();
    }

    void Update()
    {

        //if (mainCamera == null)
        //{
        //    mainCamera = Camera.main;

        //    if (mainCamera == null) return;
        //}

        if (Mouse.current.leftButton.isPressed)
        {
            // Ambil posisi mouse
            Vector2 mousePos = Mouse.current.position.ReadValue();

            Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));

            transform.position = worldPos;
            tr.emitting = true;
        }
        else
        {
            tr.emitting = false;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            tr.Clear();
        }
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            klikSound.PlayOneShot(audioClick);
            psClick.Play();

        }
    }

    void OnEnable()
    {
        // Daftarkan fungsi ke event pindah scene
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Hapus pendaftaran saat script mati agar tidak error (memory leak)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cari kamera baru setiap kali scene berganti
        mainCamera = Camera.main;
    }
}
