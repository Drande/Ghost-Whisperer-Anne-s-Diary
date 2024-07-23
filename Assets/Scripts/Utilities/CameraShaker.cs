using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float magnitude = 0.1f;
    [SerializeField] private float duration = 0.25f;

    public void Shake() {
        StartCoroutine(ShakeEffect());
    }

    private IEnumerator ShakeEffect()
    {
        Vector3 originalPosition = transform.position;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;
    }
}
