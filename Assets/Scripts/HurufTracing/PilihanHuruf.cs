using TMPro;
using UnityEngine;

public class PilihanHuruf : MonoBehaviour
{
    private TextMeshProUGUI teksABC;
    private Transform ParentGrid;
    private bool iniHasilClone = false;
    [SerializeField] private TracingHurufSceneControl sceneControl;

    void Start()
    {
        if (iniHasilClone) return;
        ParentGrid = GameObject.Find("GridParent").GetComponent<RectTransform>();
        LoopingABCZ();
    }

    private void LoopingABCZ()
    {
        for (char abc = 'A'; abc <= 'Z'; abc++)
        {
            // 1. Spawn si Outer (karena script nempel di Outer, this.gameObject adalah Outer)
            GameObject clone = Instantiate(this.gameObject, ParentGrid);

            // 2. Ambil script di clone dan kunci agar tidak looping (Tetap sama)
            PilihanHuruf scriptClone = clone.GetComponent<PilihanHuruf>();
            if (scriptClone != null)
            {
                scriptClone.iniHasilClone = true;
            }

            // 3. NAH, INI BEDANYA: Pakai GetComponentInChildren
            // Karena teks ada di dalam (anak) dari objek yang kamu spawn
            TextMeshProUGUI teks = clone.GetComponentInChildren<TextMeshProUGUI>();

            if (teks != null)
            {
                teks.text = abc.ToString();
            }

            // Opsional: Kasih nama di hierarchy biar gampang ngeceknya
            clone.name = "Huruf_" + abc;
        }

        // 4. Matikan master Outer agar tidak dobel
        this.gameObject.SetActive(false);
    }

    public void IniHurufApa()
    {
        string yy = GetComponentInChildren<TextMeshProUGUI>().text;
        //sceneControl.DiKlikDaiButtonHurufPanelAwal(yy);
        StartCoroutine(sceneControl.SkenarioBukaTutupDiawal(yy));
    }

}