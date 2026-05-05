using UnityEngine;

public class BirdAnimationController : MonoBehaviour
{
    //[SerializeField] private float speedKaliKe = 1.0f;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = Random.Range(1.0f, 3.2f);
    }

}
