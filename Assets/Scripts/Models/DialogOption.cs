public class DialogOption {
    public string label { get; private set; }
    public string value { get; private set; }

    public DialogOption(string label, string value) {
        this.label = label;
        this.value = value;
    }
}