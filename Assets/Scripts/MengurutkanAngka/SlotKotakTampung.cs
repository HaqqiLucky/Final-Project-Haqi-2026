using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotKotakTampung : MonoBehaviour, IDropHandler
{
    MengurutkanAngkaSceneControl mengurutkanAngkaSceneControl;
    //[SerializeField] private ParticleSystem confentiiOnDestroy;
    private ParticleSystem partikelOnDestroy;
    private bool UdahJalan = false;
    private Rigidbody2D rb;
    private AudioSource asors;
    [SerializeField] private AudioClip correct, wrong, hancur;
    protected void Awake()
    {
        mengurutkanAngkaSceneControl = FindFirstObjectByType<MengurutkanAngkaSceneControl>(); // yang dari ai dan aku blm paham

        // ini buat partikel sistem
        GameObject partikelObj = GameObject.Find("OnDestroyPrefabParticleSystem");
        partikelOnDestroy = partikelObj.GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        asors = mengurutkanAngkaSceneControl.GetComponent<AudioSource>();
    }

    public int slotIndex;

    //[SerializeField] MengurutkanAngkaSceneControl mengurutkanAngkaSceneControl;
    public void OnDrop(PointerEventData eventData)
    {

        //mengurutkanAngkaSceneControl.PengecekanRutinKotaks();

        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItemAngkas draggableItemAngkas = dropped.GetComponent<DraggableItemAngkas>();
            
            int AngkaYangDiDrag = int.Parse(draggableItemAngkas.GetComponent<TMP_Text>().text);
         
            Transform parentSebelumnyaKaloSalah = draggableItemAngkas.parentAfterDrag;
            //Debug.Log("Angka " + AngkaYangDiDrag + " masuk ke slot index ke-" + slotIndex);
            //BagianAtasAngkaSudahDimasukanTapiBelumDiCek[0] += AngkaYangDiDrag;

            if (slotIndex + 1 == AngkaYangDiDrag)
            {
                //Debug.Log("slot ke " + slotIndex + "sudah bener yaitu " + AngkaYangDiDrag);
                draggableItemAngkas.parentAfterDrag = transform;
                draggableItemAngkas.GetComponent<CanvasGroup>().blocksRaycasts = false;
                LeanTween.moveLocalY(gameObject, transform.localPosition.y + 100f, 0.2f)
                    .setLoopPingPong(2);
                asors.PlayOneShot(correct);
                //mengurutkanAngkaSceneControl.PenghitungAmalKebenaran++;
            } else
            {
                asors.PlayOneShot(wrong);
                //Debug.Log("slot ke " + slotIndex + "sudah salah yaitu " + AngkaYangDiDrag);
                //LeanTween.rotateZ(this.gameObject, 30f, 0.5f)
                //    .setEase(LeanTweenType.easeInOutSine)  // di cek nanti sine itu apa
                //    .setLoopPingPong(1);
                draggableItemAngkas.parentAfterDrag = parentSebelumnyaKaloSalah;
                //LeanTween.rotateAround(this.gameObject, Vector3.forward, 30f, 0.3f)
                //    .setEaseShake();
                //mengurutkanAngkaSceneControl.PenghitungAmalKebenaran -= 1;

            }

        }

    }



    IEnumerator SelesaiNaik()
    {
        //Debug.Log("smpe sini");
        rb.simulated = false;
        LeanTween.rotateAround(this.gameObject, Vector3.forward, -33.115f, 0.1f)
                    .setEaseInOutBack();
        yield return new WaitForSeconds(2f);
        float delay = Random.Range(2.0f, 10.0f);
        yield return new WaitForSeconds(delay);
        rb.simulated = true;

    }

    private void Update()
    {
        if (mengurutkanAngkaSceneControl.SudahNaik && !UdahJalan)
        {
            StartCoroutine(SelesaiNaik());
            UdahJalan = true;
        }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PenghancurPrefab")) // kalo kena yg di drag
        {
       
            asors.PlayOneShot(hancur);
            mengurutkanAngkaSceneControl.TotalYangSudahDiHancurkan += 1;
            partikelOnDestroy.transform.position = this.transform.position;
            partikelOnDestroy.Play();
            //Destroy(this.gameObject);
            this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        } else if (collision.gameObject.CompareTag("PrefabGoneNow")) // ini di luar scene
        {
            //Destroy(this.gameObject);
            this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }
    }
}
