using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

[System.Serializable]
public class Character
{
    public string characterName;
    public Sprite characterSprite;
}

public class MessageInScreen : MonoBehaviour
{
    public static MessageInScreen Instance { get; private set; }
    [SerializeField] private List<Character> characterList = new List<Character>();
    [SerializeField] private GameObject messageElement;
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI characterText;
    [SerializeField] private TextMeshProUGUI characterNameTM;
    [SerializeField] private string skipButton = "Fire1";
    [SerializeField] private float writeSpeed = 0.06f;
    [SerializeField] private float fixedSecondsDelay = 1f;
    [SerializeField] private OptionPanel optionPanel;
    private string lastOptionSelected;
    private Coroutine textWriter;
    private Coroutine currentDialog;
    private string currentMessage;
    public event Action<string> onDialogCompleted;

    [HideInInspector] public bool isActive => messageElement.activeInHierarchy;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(!isActive) return;
        if(Input.GetButtonDown(skipButton)) {
            if(textWriter != null) {
                StopCoroutine(textWriter);
                characterText.text = currentMessage;
            }
        }
    }

    public void Stop() {
        StopAllCoroutines();
        messageElement.SetActive(false);
    }

    public void StartDialog(Message[] messages)
    {
        currentDialog = StartCoroutine(WriteDialog(messages));
    }

    public void StartDialog(Message[] messages, Action<string> onCompleted)
    {
        onDialogCompleted += onCompleted;
        currentDialog = StartCoroutine(WriteDialog(messages));
    }

    public void SkipCurrentDialog()
    {
        if(currentDialog != null) StopCoroutine(currentDialog);
        if(textWriter != null) StopCoroutine(textWriter);
        if(GameManager.Instance.isPaused) GameManager.Instance.TogglePause();
        NotifyComplete(null);
    }

    private void NotifyComplete(string result) {
        messageElement.SetActive(false);
        onDialogCompleted?.Invoke(result);
        onDialogCompleted = null;
    }

    private System.Collections.IEnumerator WriteDialog(Message[] messages)
    {
        messageElement.SetActive(true);
        foreach (var message in messages)
        {
            currentMessage = LocalizationSettings.StringDatabase.GetTable("Dialogs").GetEntry(message.message).GetLocalizedString(LocalizationSettings.SelectedLocale);
            UpdateCharacterImage(message.character);
            UpdateCharacterText(currentMessage);
            UpdateCharacterNameTM(message.character);
            yield return new WaitForSeconds(currentMessage.Length * writeSpeed);
            if(message.options != null) {
                lastOptionSelected = null;
                DisplayDialogOptions(message.options);
                while(string.IsNullOrEmpty(lastOptionSelected)) {
                    yield return null;
                }
            } else {
                yield return new WaitForSeconds(fixedSecondsDelay);
            }
        }
        NotifyComplete(lastOptionSelected);
    }

    private void DisplayDialogOptions(DialogOption[] options)
    {
        optionPanel.SetOptions(options, (selectedOptionValue) => {
            lastOptionSelected = selectedOptionValue;
        });
    }

    private void UpdateCharacterImage(string characterName)
    {
        Character selectedCharacter = characterList.Find(character => character.characterName == characterName);
        if (selectedCharacter != null)
        {
            characterImage.sprite = selectedCharacter.characterSprite;
        }
        else
        {
            Debug.LogWarning("Character not found in the list! " + characterName);
        }
    }

    private void UpdateCharacterText(string message)
    {
        textWriter = StartCoroutine(Coroutines.WriteText(characterText, message, writeSpeed));
    }
    private void UpdateCharacterNameTM(string characterName)
    {
        characterNameTM.text = CharacterNames.GetRealName(characterName);
    }
}
