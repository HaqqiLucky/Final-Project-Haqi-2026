using System.Globalization;
using UnityEngine;

public class InfiniteSky : MonoBehaviour
{
    public float speed;
    [SerializeField] private Renderer bgRenderer;

    void Start()
    {
        bgRenderer.sortingLayerName = "SkyClouds";
        bgRenderer.sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {


        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }




}
