[System.Serializable]
public class SceneModel : CommandBase
{
    public override CommandType type => CommandType.SCENE;
    public string sceneName; 
}