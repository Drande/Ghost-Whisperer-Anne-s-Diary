
public class Message
{
    public Message(string character, string message)
    {
        this.character = character;
        this.message = message;
    }

    public Message(string character, string message, DialogOption[] options): this(character, message)
    {
        this.options = options;
    }

    public string character { get; set; } = CharacterNames.Rob;
    public string message { get; set; }
    public DialogOption[] options { get; set; }
}