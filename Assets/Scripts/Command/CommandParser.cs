using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandParser : MonoBehaviour
{
    public CommandClass[] Parse(string _CSVFileName)
    {
        SceneModel sceneModel;
        BackgroundModel backgroundModel;
        ShowcharModel showcharModel;
        HidecharModel hidecharModel;
        DialogueModel dialogueModel;
        CharacterModel characterModel;
        QuestModel questModel;

        List<CommandClass> commandList = new List<CommandClass>();
        CommandClass tempCommandList = new CommandClass();
        List<CommandBase> commandOrder = tempCommandList.commandList.ToList();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        string[] data = csvData.text.Split(new char[] { '\n' });    
        int current_scene = 1;
        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });
            if (current_scene.ToString() != row[0] && row[0] != "")
            {
                current_scene = int.Parse(row[0]);
                tempCommandList.commandList = commandOrder.ToArray();
                commandList.Add(tempCommandList);
                tempCommandList = new CommandClass();
                commandOrder = tempCommandList.commandList.ToList();
            }

            if (row[1] == "SCENE")
            {
                sceneModel = new SceneModel();
                sceneModel.sceneName = row[2];
                commandOrder.Add(sceneModel);
            }
            else if (row[1] == "BACKGROUND")
            {
                backgroundModel = new BackgroundModel();
                backgroundModel.command = row[2];
                backgroundModel.argument = row[3];
                commandOrder.Add(backgroundModel);
            }
            else if (row[1] == "SHOW_CHAR")
            {
                showcharModel = new ShowcharModel();
                commandOrder.Add(showcharModel);
            }
            else if (row[1] == "HIDE_CHAR")
            {
                hidecharModel = new HidecharModel();
                commandOrder.Add(hidecharModel);
            }
            else if (row[1] == "DIALOGUE")
            {
                dialogueModel = new DialogueModel();
                dialogueModel.name = row[2];
                dialogueModel.context = row[3];
                commandOrder.Add(dialogueModel);
            }
            else if (row[1] == "CHARACTER")
            {
                characterModel = new CharacterModel();
                characterModel.name = row[2];
                characterModel.command = row[3];
                characterModel.argument = row[4];
                commandOrder.Add(characterModel);
            }
            else if (row[1] == "QUEST")
            {
                questModel = new QuestModel();
                questModel.title = row[2];
                questModel.line = row[3];
                questModel.questName = row[4];
                commandOrder.Add(questModel);
            }
        }
        commandList.Add(tempCommandList);  // NOTE :: Adding last scene's dialogue.

        return commandList.ToArray();
    }
}
