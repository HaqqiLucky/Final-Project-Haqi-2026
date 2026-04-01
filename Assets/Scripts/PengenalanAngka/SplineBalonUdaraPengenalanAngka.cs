using UnityEngine;
using UnityEngine.Splines;

public class SplineBalonUdaraPengenalanAngka : MonoBehaviour
{
    public float speed;
    float t = 0;
    public SplineContainer splineContainer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spline spline = splineContainer.Spline;

        t += Time.deltaTime * speed;
        t %= 1f;
        var position = spline.EvaluatePosition(t);
        var rotation = spline.EvaluateTangent(t);
        transform.position = position;
        float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
