[System.Serializable]
public class CharacterModel : CommandBase
{
    public override CommandType type => CommandType.CHARACTER;
    public string name;
    public string command;
    public string argument;
}