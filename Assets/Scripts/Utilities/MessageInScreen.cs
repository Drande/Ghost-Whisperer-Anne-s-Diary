using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Character
{
    public string characterName;
    public Sprite characterSprite;
}

public class Message
{
    public Message(string character, string message, float duration = 3f)
    {
        this.character = character;
        this.message = message;
        this.duration = duration;
    }

    public string character { get; set; } = CharacterNames.Rob;
    public string message { get; set; }
    public float duration { get; set; }
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
    private Coroutine textWriter;
    private Coroutine currentDialog;
    private string currentMessage;
    private const int charactersTimeBackup = 30;
    public event Action onDialogCompleted;

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
            StopCoroutine(textWriter);
            characterText.text = currentMessage;
        }
    }

    public void Stop() {
        StopAllCoroutines();
        messageElement.SetActive(false);
    }

    public void StartDialog(Message[] messages, System.Action onCompleted)
    {
        onDialogCompleted += onCompleted;
        currentDialog = StartCoroutine(DialogCooldown(messages));
    }

    public void SkipCurrentDialog()
    {
        if(currentDialog != null) StopCoroutine(currentDialog);
        if(textWriter != null) StopCoroutine(textWriter);
        if(GameManager.Instance.isPaused) GameManager.Instance.TogglePause();
        NotifyComplete();
    }

    private void NotifyComplete() {
        messageElement.SetActive(false);
        onDialogCompleted?.Invoke();
        onDialogCompleted = null;
    }

    private System.Collections.IEnumerator DialogCooldown(Message[] messages)
    {
        messageElement.SetActive(true);
        foreach (var message in messages)
        {
            currentMessage = message.message;
            UpdateCharacterImage(message.character);
            UpdateCharacterText(message.message, message.duration);
            UpdateCharacterNameTM(message.character); 
            yield return new WaitForSeconds(message.duration);
        }
        NotifyComplete();
    }


    private System.Collections.IEnumerator MessageCooldown()
    {
        yield return new WaitForSeconds(3);
        messageElement.SetActive(false);
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

    private void UpdateCharacterText(string message, float duration)
    {
        textWriter = StartCoroutine(Coroutines.WriteText(characterText, message, Mathf.Clamp(duration / (message.Length + charactersTimeBackup), 0, 0.06f)));
    }
    private void UpdateCharacterNameTM(string characterName)
    {
        characterNameTM.text = CharacterNames.GetRealName(characterName);
    }
}
