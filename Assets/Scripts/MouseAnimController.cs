using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseAnimController : MonoBehaviour
{
    [SerializeField] private GameObject MouseGO;
    private Animator mouseAnim;
    RectTransform parentAnimMouse;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseAnim = MouseGO.GetComponent<Animator>();
        //animMouse = GetComponent<Animator>();

        parentAnimMouse = GetComponent<RectTransform>();
        StartCoroutine(MouseAlive());
    }

    IEnumerator MouseAlive()
    {
        StartMelebar();
        yield return new WaitForSeconds(1f);
        MouseGO.SetActive(true);
    }

    private void StartMelebar()
    {
        LeanTween.value(this.gameObject, parentAnimMouse.sizeDelta.x, 315f, 1f)
            .setEaseOutBack()
            .setOnUpdate((float val) =>
            {
                parentAnimMouse.sizeDelta = new Vector2(val, parentAnimMouse.sizeDelta.y);
            });
    }

    void Update()
    {
        // bawa mouse nya ke gameobject, akses anim nya

        if (mouseAnim == null || !MouseGO.activeInHierarchy)
        {
            return;
        }

        if (!Mouse.current.leftButton.isPressed)
        {
            mouseAnim.SetBool("IsClicked", false);
        }
        else
        {
            mouseAnim.SetBool("IsClicked", true);
        }
    }
}
