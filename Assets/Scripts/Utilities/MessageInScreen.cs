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
    [SerializeField] private TMP_Text characterText;
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

    public void Stop() {
        StopAllCoroutines();
        messageElement.SetActive(false);
    }

    public void SendMessage(Message message)
    {
        SendMessage(message.character, message.message);
    }

    public void SendMessage(string character, string message)
    {
        // Update the character image and text when properties are changed in the Inspector
        UpdateCharacterImage(character);
        UpdateCharacterText(message);
        messageElement.SetActive(true);
        StartCoroutine(MessageCooldown());
    }

    public void StartDialog(Message[] messages, System.Action onComplete)
    {
        StartCoroutine(DialogCooldown(messages, onComplete));
    }

    private System.Collections.IEnumerator DialogCooldown(Message[] messages, System.Action onComplete)
    {
        messageElement.SetActive(true);
        foreach (var message in messages)
        {
            UpdateCharacterImage(message.character);
            UpdateCharacterText(message.message);
            yield return new WaitForSeconds(message.duration);
        }
        messageElement.SetActive(false);
        onComplete.Invoke();
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

    private void UpdateCharacterText(string message)
    {
        characterText.text = message;
    }
}
