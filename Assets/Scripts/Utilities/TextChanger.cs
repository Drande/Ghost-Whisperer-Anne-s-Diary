using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextChanger : MonoBehaviour
{
    private TextMeshProUGUI uiText;

    private void Awake() {
        uiText = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeText(string newText)
    {
        StartCoroutine(Coroutines.WriteText(uiText, newText));
    }
}