using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

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
    private bool isFull = false;    

    // sesi
    private int sesiSekarang = 0;
    private int totalSesi = 5;
    [SerializeField] private TextMeshProUGUI teksSesi;
    private bool menggantiSesi = false;
    [SerializeField] SceneControl sceneControlIsiGame;
    [SerializeField] float[] arrayWaktuPerSesi = new float[5];
    [SerializeField] int totalSkorHaha = 0;
    [SerializeField] CanvasGroup Emo1, Emo2, Emo3;
    [SerializeField] Animator AnimEmo1, AnimEmo2, AnimEmo3;
    [SerializeField] CanvasGroup EmojiSkorParent;


    [SerializeField] private TextMeshProUGUI teksSkorHuha;


    private void Awake()
    {
        for (int i = 0; i < arrayWaktuPerSesi.Length; i++)
        {
            arrayWaktuPerSesi[i] = -1f;
        }
    }

    private void Start()
    {
        BukaSceme();
        //EmojiSkorParent.gameObject.SetActive(false);
    }

    void Update()
    {
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
            // Cari induk yang punya tag "Gambar"
            GameObject target = FindParentWithTag(hitObj, "Gambar");

            // Debug untuk tahu apa yang kena klik
            Debug.Log("Raycast Start menyentuh: " + hitObj.name + " | Parent Tag Gambar: " + (target != null ? target.name : "KOSONG"));

            if (target != null)
            {
                startObject = target;
                Debug.Log("Mulai narik dari: " + startObject.name);

                // 1. Spawn garis
                GameObject newLineObj = Instantiate(linePrefab, canvasRect);

                // 2. RESET POSISI (Ini obat buat error tadi)
                RectTransform rect = newLineObj.GetComponent<RectTransform>();
                rect.localPosition = Vector3.zero; // Harus (0,0,0)
                rect.anchorMin = Vector2.zero;    // Anchor kiri bawah
                rect.anchorMax = Vector2.one;     // Anchor kanan atas
                rect.offsetMin = Vector2.zero;    // Reset margin
                rect.offsetMax = Vector2.zero;    // Reset margin

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

        // Cari objek ber-tag "Tulisan" (bisa objek itu sendiri atau parent-nya)
        GameObject target = hitObj != null ? FindParentWithTag(hitObj, "Tulisan") : null;

        if (target != null && startObject != null)
        {
            //Debug.Log($"Membandingkan: {startObject.name} vs {target.name}");
            // PENTING: Kita bandingkan nama TARGET (Bapaknya), bukan nama hitObj (Anaknya)
            string namaAsal = startObject.name;
            string namaTarget = target.name;

            //Debug.Log($"Membandingkan Asal: [{namaAsal}] dengan Target: [{namaTarget}]");

            if (startObject.name == target.name)
            {
                //Debug.Log("KONEKSI BENAR!");
                // Kunci posisi akhir ke tengah target
                Vector2 endPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out endPoint);
                currentLine.Points = new Vector2[2] { startPoint, endPoint };
                currentLine.SetAllDirty();
                yangUdahBener += 1; 

                if (yangUdahBener == 3)
                {
                    menggantiSesi = true;
                    StartCoroutine(PergantianSesi());
                }

            }
            else
            {
                Debug.Log("NAMA TIDAK COCOK, HAPUS GARIS");
                Destroy(currentLine.gameObject);
            }
        }
        else
        {
            Debug.Log("MELESET / TIDAK KENA TAG TULISAN");
            Destroy(currentLine.gameObject);
        }
        currentLine = null;
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


    IEnumerator PergantianSesi()
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

                int skorFinal = HitungTotalWaktu();

                EmojiSkor(skorFinal);
            }
            else
            {
                yield return new WaitForSeconds(2f);
                BukaSceme();
                menggantiSesi = false;
            }
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
        float total = 0;

        foreach (float waktu in arrayWaktuPerSesi)
        {
            if (waktu != -1f)
            {
                total += waktu / 3;
            }
        }


        int titil = Mathf.CeilToInt(total);
        Debug.Log(titil);
        LeanTween.value(gameObject, 0f, titil, 5f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnUpdate((float val) => {
                teksSkorHuha.text = Mathf.RoundToInt(val).ToString();
            });

        return titil;
    }

    private void EmojiSkor(int skor)
    {
        // Durasi animasi skor kamu kan 5 detik.
        // Kita bagi kemunculan emot berdasarkan proporsi skornya.

        float durasiTotal = 5f;

        // Emot 1 muncul hampir instan
        StartCoroutine(JedaEmot(Emo1, AnimEmo1, "1Ngangguk", 0.1f));

        // Emot 2 muncul di tengah-tengah animasi (misal detik ke-2.5)
        if (skor >= 50)
        {
            StartCoroutine(JedaEmot(Emo2, AnimEmo2, "2Horrmat", durasiTotal * 0.5f));
        }

        // Emot 3 muncul di akhir animasi (misal detik ke-4.5)
        if (skor >= 80)
        {
            StartCoroutine(JedaEmot(Emo3, AnimEmo3, "3mindblowing", durasiTotal * 0.9f));
        }
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


}