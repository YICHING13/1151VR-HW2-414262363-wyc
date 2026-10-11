using UnityEngine;

public class ButterAnimation : MonoBehaviour
{
    public float breatheSpeed = 2f;
    public float breatheAmount = 0.03f;

    private Vector3 originalScale;
    private Rigidbody2D rb;

    void Start()
    {
        originalScale = transform.localScale;
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb == null) return;

        float x = 1f;
        float y = 1f;

        if (Mathf.Abs(rb.linearVelocity.y) > 0.3f)
        {
            // Jump：跳躍時拉長
            x = 0.85f;
            y = 1.15f;
        }
        else if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            // Run：跑步時快速伸縮
            float bounce = Mathf.Sin(Time.time * 12f) * 0.08f;
            x = 1f + bounce;
            y = 1f - bounce;
        }
        else
        {
            // Idle：站著時輕微呼吸
            float breathe = Mathf.Sin(Time.time * breatheSpeed)
                            * breatheAmount;
            x = 1f + breathe;
            y = 1f - breathe;
        }

        Vector3 targetScale = new Vector3(
            originalScale.x * x,
            originalScale.y * y,
            originalScale.z
        );

        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            10f * Time.deltaTime
        );
    }
}

