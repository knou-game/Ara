using UnityEngine;

public class DialogueModel : CommandBase
{
    public override CommandType type => CommandType.DIALOGUE;
    [Tooltip("Script order")]
    public int order;
    [Tooltip("Character name")]
    public string name;

    [Tooltip("Line")]
    public string context;
}
