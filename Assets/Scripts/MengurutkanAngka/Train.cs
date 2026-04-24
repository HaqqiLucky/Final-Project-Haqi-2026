using UnityEngine;

public class Train : MonoBehaviour
{
    public static float Speed;
    private Vector3 StartPos = new Vector3(-3266f, -320.16f, 0);
    private TrainSpeed[] allGerbong;
    //public bool semuanyaSelesai;

    private void Awake()
    {
        // Cari semua script TrainSpeed yang ada di anak-anaknya
        allGerbong = GetComponentsInChildren<TrainSpeed>();
    }

    private void OnEnable()
    {
        transform.localPosition = StartPos;
        Speed = 100f;
        //semuanyaSelesai = false;
    }

    void Update()
    {
        // STEP 1: Asumsikan default speed adalah 100
        float targetSpeed = 80f;

        // STEP 2: Cek satu per satu gerbong
        foreach (TrainSpeed gerbong in allGerbong)
        {
            if (gerbong.IsInScreen())
            {
                // Kalau ada satu saja yang di layar, target jadi 20
                targetSpeed = 50f;
                break; // Keluar dari loop, gak usah cek gerbong lain lagi
            }
        }

        // STEP 3: Terapkan speed
        Speed = targetSpeed;
        transform.Translate(Vector3.right * Speed * Time.deltaTime);

        // Debug untuk liat polanya sesuai analogi kamu
        //Debug.Log("Current Speed: " + Speed);

        if (transform.localPosition.x > 374386f)
        {
            this.gameObject.SetActive(false);
            //semuanyaSelesai = true;
        }
    }
}