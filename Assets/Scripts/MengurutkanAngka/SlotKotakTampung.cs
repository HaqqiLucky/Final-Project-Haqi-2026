using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotKotakTampung : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    MengurutkanAngkaSceneControl mengurutkanAngkaSceneControl;

    private bool UdahMiring = false;
    private void Awake()
    {
        mengurutkanAngkaSceneControl = FindFirstObjectByType<MengurutkanAngkaSceneControl>(); // yang dari ai dan aku blm paham
    }
    private void Start()
    {
        //GetComponent<Image>().raycastTarget = false;
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
            draggableItemAngkas.parentAfterDrag = transform;
            int AngkaYangDiDrag = int.Parse(draggableItemAngkas.GetComponent<TMP_Text>().text);
            //Debug.Log("Angka " + AngkaYangDiDrag + " masuk ke slot index ke-" + slotIndex);
            //BagianAtasAngkaSudahDimasukanTapiBelumDiCek[0] += AngkaYangDiDrag;

            if (slotIndex + 1 == AngkaYangDiDrag)
            {
                //Debug.Log("slot ke " + slotIndex + "sudah bener yaitu " + AngkaYangDiDrag);
                LeanTween.moveLocalY(gameObject, transform.localPosition.y + 100f, 0.2f)
                    .setLoopPingPong(2);
                //mengurutkanAngkaSceneControl.PenghitungAmalKebenaran++;
            } else
            {
                Debug.Log("slot ke " + slotIndex + "sudah salah yaitu " + AngkaYangDiDrag);
                //LeanTween.rotateZ(this.gameObject, 30f, 0.5f)
                //    .setEase(LeanTweenType.easeInOutSine)  // di cek nanti sine itu apa
                //    .setLoopPingPong(1);

                LeanTween.rotateAround(this.gameObject, Vector3.forward, 30f, 0.3f)
                    .setEaseShake();
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
            Debug.Log("Hancur haha");
            Destroy(this.gameObject);
        }
    }
}
