using System;

[Serializable]
public class Message
{
    public Message(GameCharacters character, string message)
    {
        this.character = character;
        this.message = message;
    }

    public Message(GameCharacters character, string message, DialogOption[] options): this(character, message)
    {
        this.options = options;
    }

    public GameCharacters character;
    public string message;
    public DialogOption[] options;
}