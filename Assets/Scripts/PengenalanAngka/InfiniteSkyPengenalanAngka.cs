using UnityEngine;
using UnityEngine.UIElements;

public class InfiniteSkyPengenalanAngka : MonoBehaviour
{
    public float speed;
    [SerializeField] private Renderer bgRenderer;
    [SerializeField] PengenalanAngkaSceneControl check;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = speed;

        if (check.AnimasiJalankah == true)
        {
            currentSpeed = speed * 40;
        }

        bgRenderer.material.mainTextureOffset += new Vector2(currentSpeed * Time.deltaTime, 0);
    }
}
