
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public SpriteRenderer background;

    public Vector3 offset = new Vector3(0, 1, -10);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null || background == null) return;

        Camera cam = GetComponent<Camera>();
        Vector3 wanted = target.position + offset;

        Bounds b = background.bounds;

        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        float minX = b.min.x + halfWidth;
        float maxX = b.max.x - halfWidth;
        float minY = b.min.y + halfHeight;
        float maxY = b.max.y - halfHeight;

        wanted.x = minX <= maxX
            ? Mathf.Clamp(wanted.x, minX, maxX)
            : b.center.x;

        wanted.y = minY <= maxY
            ? Mathf.Clamp(wanted.y, minY, maxY)
            : b.center.y;

        wanted.z = -10f;

        transform.position = Vector3.Lerp(
            transform.position,
            wanted,
            smoothSpeed * Time.deltaTime
        );
    }
}


