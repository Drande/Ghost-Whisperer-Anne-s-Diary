using System;

[Serializable]
public class DialogOption {
    public string label;
    public string value;
    
    public DialogOption(string label, string value) {
        this.label = label;
        this.value = value;
    }
}