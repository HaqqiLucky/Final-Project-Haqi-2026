using UnityEngine;

public class TrainSpeed : MonoBehaviour
{
    public bool IsInScreen()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        // Return true jika gerbong ini ada di dalam layar
        return (viewPos.x > 0.1f && viewPos.x < 0.9f && viewPos.z > 0);
    }
}