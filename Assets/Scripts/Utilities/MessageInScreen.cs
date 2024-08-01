using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using System.Linq;

[System.Serializable]
public class Character
{
    public GameCharacters characterName;
    public Sprite characterSprite;
}

public class MessageInScreen : MonoBehaviour
{
    public static MessageInScreen Instance { get; private set; }
    [SerializeField] private List<Character> characterList = new();
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
    private Message skipToMessage;
    private Message[] currentDialogMessages;
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

    public void StartDialog(DialogData dialog)
    {
        Instance.StartDialog(dialog.messages);
    }

    public void StartDialog(Message[] messages)
    {
        currentDialog = StartCoroutine(WriteDialog(messages));
    }

    public void StartDialog(Message[] messages, Action<string> onCompleted)
    {
        StopAllCoroutines();
        onDialogCompleted += onCompleted;
        currentDialogMessages = messages;
        currentDialog = StartCoroutine(WriteDialog(messages));
    }

    public void SkipCurrentDialog()
    {
        var nextChoice = currentDialogMessages.FirstOrDefault(m => m.options.Any());
        if(nextChoice != null) {
            skipToMessage = nextChoice;
            StopCoroutine(textWriter);
        } else {
            if(currentDialog != null) StopCoroutine(currentDialog);
            if(textWriter != null) StopCoroutine(textWriter);
            if(GameManager.Instance.isPaused) GameManager.Instance.TogglePause();
            NotifyComplete(null);
        }
    }

    private void NotifyComplete(string result) {
        messageElement.SetActive(false);
        skipToMessage = null;
        var temp = onDialogCompleted; // Allow nested dialogs
        onDialogCompleted = null;
        temp?.Invoke(result);
    }

    private System.Collections.IEnumerator WriteDialog(Message[] messages)
    {
        messageElement.SetActive(true);
        foreach (var message in messages)
        {
            if(skipToMessage != null && message.message != skipToMessage.message) {
                continue;
            } else if(skipToMessage != null) {
                skipToMessage = null;
            }
            currentMessage = LocalizationSettings.StringDatabase.GetTable("Dialogs").GetEntry(message.message).GetLocalizedString(LocalizationSettings.SelectedLocale);
            UpdateCharacterImage(message.character);
            UpdateCharacterText(currentMessage);
            UpdateCharacterNameTM(message.character);
            yield return new WaitForSeconds(currentMessage.Length * writeSpeed);
            if(message.options != null && message.options.Any()) {
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

    private void UpdateCharacterImage(GameCharacters characterName)
    {
        Character selectedCharacter = characterList.Find(character => character.characterName == characterName);
        if (selectedCharacter != null)
        {
            characterImage.sprite = selectedCharacter.characterSprite;
        }
        else
        {
            Debug.LogWarning("Character not found in the list! " + characterName.ToString());
        }
    }

    private void UpdateCharacterText(string message)
    {
        textWriter = StartCoroutine(Coroutines.WriteText(characterText, message, writeSpeed));
    }
    private void UpdateCharacterNameTM(GameCharacters characterName)
    {
        characterNameTM.text = CharacterNames.GetRealName(characterName);
    }
}
