using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SimonSaysTarget : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private string highlightSfx;
    public readonly Guid id = Guid.NewGuid();
    private Material originalMaterial;
    private readonly float duration = 0.5f;
    private new Renderer renderer;

    private void Awake() {
        renderer = GetComponent<Renderer>();
        originalMaterial = renderer.material;
    }

    public float Highlight() {
        StartCoroutine(HighlightAnimation());
        return duration;
    }
    
    IEnumerator HighlightAnimation()
    {
        renderer.material = highlightMaterial;
        AudioManager.Instance.PlaySFX(highlightSfx);
        yield return new WaitForSeconds(duration);
        renderer.material = originalMaterial;
    }
}
