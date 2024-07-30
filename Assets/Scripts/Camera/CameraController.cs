using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    private float xOffset = 0.23f;
    private float yOffset = 1.83f;
    private float zOffset = -2.11f;

    private void Start()
    {
        offset.x = xOffset;
        offset.y = yOffset;
        offset.z = zOffset;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
