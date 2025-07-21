[System.Serializable]
public class QuestModel : CommandBase
{
    public override CommandType type => CommandType.QUEST;
    public string title;
    public string line;
    public string questName;
}