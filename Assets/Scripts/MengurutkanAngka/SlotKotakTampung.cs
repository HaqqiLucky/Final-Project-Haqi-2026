using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotKotakTampung : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    MengurutkanAngkaSceneControl mengurutkanAngkaSceneControl;
    //[SerializeField] private ParticleSystem confentiiOnDestroy;
    private ParticleSystem partikelOnDestroy;
    private bool UdahMiring = false;
    protected void Awake()
    {
        mengurutkanAngkaSceneControl = FindFirstObjectByType<MengurutkanAngkaSceneControl>(); // yang dari ai dan aku blm paham

        // ini buat partikel sistem
        GameObject partikelObj = GameObject.Find("OnDestroyPrefabParticleSystem");
        partikelOnDestroy = partikelObj.GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        
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
                LeanTween.moveLocalY(gameObject, transform.localPosition.y + 100f, 0.2f)
                    .setLoopPingPong(2);
                //mengurutkanAngkaSceneControl.PenghitungAmalKebenaran++;
            } else
            {
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (mengurutkanAngkaSceneControl.SelesaiThisStage == true && UdahMiring == false)
        {
            LeanTween.rotateAround(this.gameObject, Vector3.forward, -33.115f, 0.1f)
                .setEaseInOutBack();
            rb.simulated = false;
            UdahMiring = true;
        }

        else if (UdahMiring == true)
        {
            rb.simulated = true;
            //rb.AddForce(Vector2.down * 50f, ForceMode2D.Impulse);
            //LeanTween.moveY(this.gameObject, 50f, 0.3f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PenghancurPrefab"))
        {
            
            mengurutkanAngkaSceneControl.TotalYangSudahDiHancurkan += 1;
            partikelOnDestroy.transform.position = this.transform.position;
            partikelOnDestroy.Play();
            Destroy(this.gameObject);
        } else if (collision.gameObject.CompareTag("PrefabGoneNow"))
        {
            Destroy(this.gameObject);
        }
    }
}
