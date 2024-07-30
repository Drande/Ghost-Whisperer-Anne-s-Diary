using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] private Button[] optionPlaceholders;

    public void SetOptions(DialogOption[] options, Action<string> onChoiceSelected)
    {
        gameObject.SetActive(true);
        for (int i = 0; i < optionPlaceholders.Length; i++)
        {
            var placeholder = optionPlaceholders[i];
            if(i < options.Length) {
                var option = options[i];
                placeholder.gameObject.SetActive(true);
                placeholder.GetComponentInChildren<TextMeshProUGUI>().text = option.label;
                placeholder.onClick.AddListener(() => {
                    OnChoiceMade();
                    onChoiceSelected?.Invoke(option.value);
                });
            } else {
                placeholder.gameObject.SetActive(false);
            }
        }
    }

    private void OnChoiceMade()
    {
        foreach (var placeholder in optionPlaceholders)
        {
            placeholder.onClick.RemoveAllListeners();
        }
        gameObject.SetActive(false);
    }
}
