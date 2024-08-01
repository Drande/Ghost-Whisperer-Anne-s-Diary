using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "Dialog/DialogData")]
public class DialogData : ScriptableObject
{
    public Message[] messages;
}