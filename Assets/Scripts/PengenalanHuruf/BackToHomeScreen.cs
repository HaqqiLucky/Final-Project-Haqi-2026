using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackToHomeScreen : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Slider slider;
    private bool isHolding = false;

    private void Awake()
    {
        if (slider == null) slider = GetComponent<Slider>();
    }

    void Update()
    {
        if (isHolding)
        {
            slider.value += 60 * Time.deltaTime;

            if (slider.value >= 100)
            {
                isHolding = false; // Stop agar tidak load scene berkali-kali
                slider.value = 100;
                LeanTween.scale(this.gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.5f);
                LoadingScreenSceneControl.Instance.LoadScene("LoginScene");
            }
        }
        else
        {
            
            if (slider.value > 0)
            {
                slider.value -= 20 * Time.deltaTime;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        //Debug.Log("diklik");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        //Debug.Log("release");
    }
}