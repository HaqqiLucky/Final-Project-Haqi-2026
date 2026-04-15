using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Train : MonoBehaviour
{
    //[SerializeField] private AnimationCurve GerakGerikMencurigakan;
    //[SerializeField] private float durasi = 5f;
    [SerializeField] private float Speed;
    private Vector3 StartPos = new Vector3(-3266f, -320.16f, 0);
    //private Vector3 EndPos = new Vector3(, -320.16f, 0);
    //public bool DoneKah = false;

    private void Start()
    {
        //this.gameObject.SetActive(false);
        //transform.position = new Vector3(-6013f, -320.16f, 0);
    }

    private void OnEnable() // ini fungsi tiap di set active true
    {
        //Debug.Log("speed skrnh = " + Speed);
        //timer = 0f;
        LeanTween.cancel(this.gameObject);
        //DoneKah = false;
        transform.localPosition = StartPos;
        LeanTween.moveLocalX(this.gameObject, 510451f, Speed)
            .setOnComplete(() => {

                this.gameObject.SetActive(false);
            });
            //.setSpeed(Speed);
      
    }

    void Update()
    {
        //if (timer < durasi && this.gameObject.activeSelf == true)
        //{
        //    timer += Time.deltaTime;
        //    float speedMultiplier = GerakGerikMencurigakan.Evaluate(timer);
        //    transform.Translate(Vector3.right * speedMultiplier * Time.deltaTime);
        //} else if (!DoneKah)
        //{

        //    DoneKah = true;
        //    this.gameObject.SetActive(false);
        //}

        //transform.position = Vector3.MoveTowards(transform.position, TargetKereta, Kecepatan * Time.deltaTime);
    }
}
