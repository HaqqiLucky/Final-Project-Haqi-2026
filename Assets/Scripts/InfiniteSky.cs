using System.Globalization;
using UnityEngine;

public class InfiniteSky : MonoBehaviour
{
    public float speed;
    [SerializeField] private Renderer bgRenderer;
    [SerializeField] private int urutanSortingOrder;
    private LoginSceneScenario sceneControl;

    void Start()
    {
        bgRenderer.sortingLayerName = "SkyClouds";
        bgRenderer.sortingOrder = urutanSortingOrder;
        sceneControl = Object.FindAnyObjectByType<LoginSceneScenario>(); 
        
    }

    // Update is called once per frame
    void Update()
    {

        float currentSpeed = speed;

        if (sceneControl.keMainMenu == true)
        {
            currentSpeed = speed * 40;
        }

        bgRenderer.material.mainTextureOffset += new Vector2(currentSpeed * Time.deltaTime, 0);

        //bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }




}
