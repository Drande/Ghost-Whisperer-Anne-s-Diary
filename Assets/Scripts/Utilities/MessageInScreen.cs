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

public class MessageInScreen : MonoBehaviour
{
    [SerializeField] private List<Character> characterList = new List<Character>();
    [SerializeField] private Image characterImage;
    [SerializeField] private TMP_Text characterText;

    [SerializeField] private string selectedCharacterName;
    [SerializeField] private string dialogueText;

    private void OnValidate()
    {
        // Update the character image and text when properties are changed in the Inspector
        UpdateCharacterImage();
        UpdateCharacterText();
    }

    private void UpdateCharacterImage()
    {
        Character selectedCharacter = characterList.Find(character => character.characterName == selectedCharacterName);
        if (selectedCharacter != null)
        {
            characterImage.sprite = selectedCharacter.characterSprite;
        }
        else
        {
            Debug.LogWarning("Character not found in the list!");
        }
    }

    private void UpdateCharacterText()
    {
        characterText.text = dialogueText;
    }
}
