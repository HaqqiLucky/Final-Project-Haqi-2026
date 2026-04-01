using System.Collections;
using UnityEngine;

public class FadeInOutCanvas : MonoBehaviour
{
    private Animator anim;
    [SerializeField] GameObject puzzle;
    [SerializeField] GameObject balonUdara;
    [SerializeField] GameObject awanOut;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(SkenarioFadeInOutCircle(anim));

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SkenarioFadeInOutCircle(Animator anim)
    {
        anim.SetBool("isDone", true);
        yield return new WaitForSeconds(3f);
        puzzle.SetActive(false);
        balonUdara.SetActive(true);
        anim.SetBool("isDone", false);

    }
}
