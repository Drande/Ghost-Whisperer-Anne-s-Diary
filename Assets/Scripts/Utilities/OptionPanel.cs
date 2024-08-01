using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
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
                var label = LocalizationSettings.StringDatabase.GetTable("Dialogs").GetEntry(option.label).GetLocalizedString(LocalizationSettings.SelectedLocale);
                placeholder.gameObject.SetActive(true);
                placeholder.GetComponentInChildren<TextMeshProUGUI>().text = label;
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
