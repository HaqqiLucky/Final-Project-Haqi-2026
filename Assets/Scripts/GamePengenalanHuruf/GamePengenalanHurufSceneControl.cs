using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class GamePengenalanHurufSceneControl : MonoBehaviour
{
    public GameObject linePrefab;
    public RectTransform canvasRect;

    private UILineRenderer currentLine;
    private Vector2 startPoint;
    private GameObject startObject;
    [SerializeField] private Image Bukatutup;
    [SerializeField] private SliderTimerGameHurufDrag timerlama;

    [SerializeField] private int yangUdahBener = 0;
    //private bool isFull = false;    

    // sesi
    private int sesiSekarang = 0;
    private int totalSesi = 5;
    [SerializeField] private TextMeshProUGUI teksSesi;
    public bool menggantiSesi = false;
    [SerializeField] SceneControl sceneControlIsiGame;
    [SerializeField] float[] arrayWaktuPerSesi = new float[5];
    //private int totalSkorHaha = 0;
    [SerializeField] GameObject Star1, Star2, Star3;
    //[SerializeField] Animator AnimEmo1, AnimEmo2, AnimEmo3;
    [SerializeField] CanvasGroup EmojiSkorParent;


    [SerializeField] private TextMeshProUGUI teksSkorHuha;

    [SerializeField] private SliderTimerGameHurufDrag slider;
    [SerializeField] private GameObject Buttons;
    [SerializeField] private GameObject Stars;



    [Header("GameHurufMoozik")]
    [SerializeField] private AudioSource asorSceneControl;
    [SerializeField] private AudioClip[] musiks;
    private int indexMusik = 0;
    [Range(0f, 1f)]
    [SerializeField] private float volume = 0.362f;


    [SerializeField] private AudioSource asorCanvas;
    [SerializeField] private AudioClip correctsekali, correcttigakali, salah;

    [SerializeField] private AudioClip starasors1, starasors2, starasors3;


    //[SerializeField] private GameObject FireworksParent;
    [SerializeField] private ParticleSystem Fireworks;
    [SerializeField] private RectTransform garisDiatasSkor;

    private void Awake()
    {
        for (int i = 0; i < arrayWaktuPerSesi.Length; i++)
        {
            arrayWaktuPerSesi[i] = -1f;
        }
        Fireworks.Stop();
    }

    private void Start()
    {
        BukaSceme();
        Buttons.SetActive(false);
        //EmojiSkorParent.gameObject.SetActive(false);


        musiks = musiks.OrderBy(x => Random.value).ToArray();
        PlayNext();
    }
    private void PlayNext()
    {
        asorSceneControl.clip = musiks[indexMusik];
        asorSceneControl.Play();
        indexMusik = (indexMusik + 1) % musiks.Length;
    }
    void Update()
    {
        // buat volume
        asorSceneControl.volume = volume;
        if (!asorSceneControl.isPlaying) PlayNext();

        var pointer = Pointer.current;
        if (pointer == null) return;

        if (pointer.press.wasPressedThisFrame)
        {
            StartDrawing(pointer.position.ReadValue());
        }

        if (currentLine != null && pointer.press.isPressed)
        {
            UpdateLine(pointer.position.ReadValue());
        }

        if (pointer.press.wasReleasedThisFrame && currentLine != null)
        {
            StopDrawing(pointer.position.ReadValue());
        }

        if (Bukatutup.transform.GetSiblingIndex() != Bukatutup.transform.parent.childCount - 1)
        {
            Bukatutup.transform.SetAsLastSibling();
        } 
    }

    void StartDrawing(Vector2 screenPos)
    {
        GameObject hitObj = GetUIObjectAtPosition(screenPos);
        if (hitObj != null)
        {
            GameObject target = FindParentWithTag(hitObj, "Gambar");

            if (target != null)
            {
                // --- TAMBAHKAN PENGECEKAN INI ---
                // Jika objek ini sudah pernah sukses, abaikan!
                // Kita bisa memanfaatkan komponen CanvasGroup atau mendeteksi apakah objek aktif untuk raycast
                var canvasGroup = target.GetComponent<CanvasGroup>();
                if (canvasGroup != null && !canvasGroup.blocksRaycasts) return;
                // --------------------------------

                startObject = target;

                // 1. Spawn garis
                GameObject newLineObj = Instantiate(linePrefab, canvasRect);

                // 2. RESET POSISI
                RectTransform rect = newLineObj.GetComponent<RectTransform>();
                rect.localPosition = Vector3.zero;
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.offsetMin = Vector2.zero;
                rect.offsetMax = Vector2.zero;

                newLineObj.transform.SetAsLastSibling();
                currentLine = newLineObj.GetComponent<UILineRenderer>();

                // 3. Ambil titik awal
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out startPoint);
                currentLine.Points = new Vector2[2] { startPoint, startPoint };
            }
        }
    }

    // Fungsi tambahan untuk ngecek parent sampai ketemu Tag yang dicari
    GameObject FindParentWithTag(GameObject child, string tag)
    {
        if (child == null) return null;

        // Cek objek itu sendiri dulu
        if (child.CompareTag(tag)) return child;

        // Cek semua bapaknya ke atas
        Transform curr = child.transform.parent;
        while (curr != null)
        {
            if (curr.CompareTag(tag)) return curr.gameObject;
            curr = curr.parent;
        }
        return null;
    }

    void UpdateLine(Vector2 screenPos)
    {
        Vector2 currentPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out currentPos);
        currentLine.Points = new Vector2[2] { startPoint, currentPos };
        currentLine.SetAllDirty();
    }

    void StopDrawing(Vector2 screenPos)
    {
        GameObject hitObj = GetUIObjectAtPosition(screenPos);
        GameObject target = hitObj != null ? FindParentWithTag(hitObj, "Tulisan") : null;

        if (target != null && startObject != null)
        {
            if (startObject.name == target.name)
            {
                Vector2 endPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out endPoint);
                currentLine.Points = new Vector2[2] { startPoint, endPoint };
                currentLine.SetAllDirty();
                yangUdahBener += 1;

                // --- TAMBAHKAN KODE INI UNTUK MENGUNCI ---
                // Matikan deteksi Raycast di objek Gambar (Asal) dan Tulisan (Target)
                // Opsi A: Menggunakan CanvasGroup (Disarankan jika objek punya anak UI lain)
                KunciRaycast(startObject);
                KunciRaycast(target);
                // -----------------------------------------

                asorCanvas.PlayOneShot(correctsekali);

                if (yangUdahBener == 3)
                {
                    menggantiSesi = true;
                    asorCanvas.PlayOneShot(correcttigakali);
                    StartCoroutine(PergantianSesi());
                }
            }
            else
            {
                Destroy(currentLine.gameObject);
                slider.DurasiSekarang -= 5;
                asorSceneControl.PlayOneShot(salah);
            }
        }
        else
        {
            Destroy(currentLine.gameObject);
        }
        currentLine = null;
    }
    void KunciRaycast(GameObject obj)
    {
        // Tambahkan CanvasGroup secara runtime jika belum ada, lalu matikan blocksRaycasts
        CanvasGroup cg = obj.GetComponent<CanvasGroup>();
        if (cg == null)
        {
            cg = obj.AddComponent<CanvasGroup>();
        }
        cg.blocksRaycasts = false;

        // Catatan: Jika objek target/asal kamu adalah Image biasa tanpa anak, 
        // kamu juga bisa pakai alternatif simpel ini:
        // var img = obj.GetComponent<UnityEngine.UI.Image>();
        // if(img != null) img.raycastTarget = false;
    }

    GameObject GetUIObjectAtPosition(Vector2 screenPos)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = screenPos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        if (results.Count > 0)
        {
            // LOG INI AKAN MEMBERITAHU SIAPA YANG TERKENA KLIK DULUAN
            //Debug.Log("Raycast menyentuh objek: " + results[0].gameObject.name);
            return results[0].gameObject;
        }
        return null;
    }

    private void TeksKeUiSesi()
    {
        sesiSekarang++;
        string sesi = $"{sesiSekarang}/{totalSesi}";
        teksSesi.text = sesi;
    }

    private void Sesi()
    {
 

        // masukin ke array tiap waktu
        switch (sesiSekarang)
        {
            case 1:
                arrayWaktuPerSesi[0] = timerlama.DurasiSekarang;
                break;
            case 2:
                arrayWaktuPerSesi[1] = timerlama.DurasiSekarang;
                break;
            case 3:
                arrayWaktuPerSesi[2] = timerlama.DurasiSekarang;
                break;
            case 4:
                arrayWaktuPerSesi[3] = timerlama.DurasiSekarang;
                break;
            case 5:
                arrayWaktuPerSesi[4] = timerlama.DurasiSekarang;
                break;
        }

    }


    private void BukaSceme()
    {
        sceneControlIsiGame.Bersatu();
        TeksKeUiSesi();
        LeanTween.value(Bukatutup.gameObject, 1f, 0f, 2f)
           .setEaseInOutBack()
           .setOnUpdate((float val) => {
               Bukatutup.fillAmount = val;

           })
           .setOnComplete(() =>
           {

               yangUdahBener = 0;
               Bukatutup.gameObject.SetActive(false);
               timerlama.StartTimer();
           });
    }

    private void TutupSceme()
    {
        Bukatutup.gameObject.SetActive(true);
        LeanTween.value(Bukatutup.gameObject, 0f, 1f, 2f)
           .setEaseInOutBack()
           .setOnUpdate((float val) => {
               Bukatutup.fillAmount = val;
           })
           .setOnComplete(() =>
           {
               HancurkanSemuaPrefabYangMengangguPergantianScene();
               timerlama.StopTimer();
           });
    }


    public IEnumerator PergantianSesi()
    {
        if (menggantiSesi)
        {
            yield return new WaitForSeconds(0.5f);
            TutupSceme();
            Sesi();
            if (CekPenuh())
            {
                EmojiSkorParent.gameObject.SetActive(true);
                LeanTween.value(EmojiSkorParent.gameObject, 0f, 1f, 1f)
                    .setOnUpdate((float val) => {
                        EmojiSkorParent.alpha = val;
                    });
                //Stars.SetActive(true);


                int skorFinal = HitungTotalWaktu();

                //EmojiSkor(skorFinal);

                // naikin bintang
                //NaikinBintang(skorFinal);
                //Invoke("NaikinBintang(skorFinal)", 2f);
                StarSoundScenario(skorFinal);
                GarisLeantween();
                LeanTween.delayedCall(1f, () => NaikinBintang(skorFinal));
                
                Fireworks.Play();
                yield return new WaitForSeconds(2);
                //Fireworks.SetActive(true);
                //FireworksParent.SetActive(true);
                
                Buttons.SetActive(true);
            }
            else
            {
                yield return new WaitForSeconds(2f);
                BukaSceme();
                menggantiSesi = false;
            }
        }
    }

    private void GarisLeantween()
    {
        LeanTween.value(0, 550, 3f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnUpdate((float val) =>
            {
                garisDiatasSkor.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, val);
            });
    }

    private void NaikinBintang(int skorYuhu)
    {

        if (skorYuhu >= 1000)
        {
            LeanTween.moveLocalY(Star1, -56, 1f)
                .setEaseInOutBack();
        }
        if (skorYuhu >= 7000)
        {
            LeanTween.moveLocalY(Star3, -56, 1f)
                .setEaseInOutBack() 
                .setDelay(0.5f);
        }
        if (skorYuhu >= 9600)
        {
            LeanTween.moveLocalY(Star2, 0, 1f)
                .setEaseInOutBack()
                .setDelay(1);
        }
    }

    private void StarSoundScenario(int skorYuhuu)
    {
        switch (skorYuhuu)
        {
            case >= 9100:
                asorCanvas.PlayOneShot(starasors3);
                break;
            case >= 4100:
                asorCanvas.PlayOneShot(starasors2);
                break;
            case >= 1000:
                asorCanvas.PlayOneShot(starasors1);
                break;
        }
    }

    private bool CekPenuh()
    {
        for (int i = 0; i < arrayWaktuPerSesi.Length; i++)
        {
            if (arrayWaktuPerSesi[i] == -1) return false;
        }
        return true;
    }

    private void HancurkanSemuaPrefabYangMengangguPergantianScene()
    {
        string[] daftarTagYangMwDiHancurin = { "Gambar", "Tulisan", "LineIn" };

        foreach (string tag in daftarTagYangMwDiHancurin)
        {
            GameObject[] thisObject = GameObject.FindGameObjectsWithTag(tag);
            
            foreach (GameObject obj in thisObject)
            {
                Destroy(obj);
            }
        }
    }


    private int HitungTotalWaktu()
    {
        // total
        float total = 0;

        foreach (float waktu in arrayWaktuPerSesi)
        {
            total += waktu;
        }


        int titil = Mathf.CeilToInt(total);
        titil *= 90;
        titil += 1000;
        //Debug.Log(titil);

        LeanTween.value(gameObject, 0f, titil, 5f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnUpdate((float val) => {
                teksSkorHuha.text = Mathf.RoundToInt(val).ToString();
            });

        return titil;
    }



    // Gunakan Coroutine agar lebih gampang ngatur jeda waktunya
    IEnumerator JedaEmot(CanvasGroup cg, Animator anim, string clipName, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (cg != null && anim != null)
        {
            // Munculkan Alpha
            LeanTween.value(cg.gameObject, 0f, 1f, 0.5f)
                .setOnUpdate((float val) => { cg.alpha = val; });

            // Mainkan Animasi
            anim.Play(clipName);
        }
    }

    public void GoReplay()
    {
        //LoadingScreenSceneControl.TargetSceneName = "MengurutkanAngka";
        LoadingScreenSceneControl.Instance.LoadScene("GamePengenalanHuruf");

        //SceneManager.LoadScene(6);
    }
    public void GoHome()
    {
        //LoadingScreenSceneControl.TargetSceneName = "HomeScene";
        LoadingScreenSceneControl.Instance.LoadScene("LoginScene");

        //SceneManager.LoadScene(6);
    }



}