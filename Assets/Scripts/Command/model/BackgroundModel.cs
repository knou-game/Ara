[System.Serializable]
public class BackgroundModel : CommandBase
{
    public override CommandType type => CommandType.BACKGROUND;
    public string command;
    public string argument;
}