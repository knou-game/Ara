using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class DialogueParser : MonoBehaviour
{
    public List<Dialogue>[] Parse(string _CSVFileName)
    {

        List<List<Dialogue>> dialogueList = new List<List<Dialogue>>();
        List<Dialogue> tempDiaogueList = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        string[] data = csvData.text.Split(new char[] { '\n' });
        int current_scene = 1;

        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });
            if (current_scene.ToString() != row[0] && row[0] != "")
            {
                current_scene = int.Parse(row[0]);
                dialogueList.Add(tempDiaogueList);
                tempDiaogueList = new List<Dialogue>();
            }
            Dialogue dialogue = new Dialogue();
            dialogue.name = row[2];
            List<string> contextList = new List<string>();
            do
            {
                contextList.Add(row[3]);
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }
            } while (row[2].ToString() == "");

            dialogue.context = contextList.ToArray();
            tempDiaogueList.Add(dialogue);
        }
        dialogueList.Add(tempDiaogueList);  // NOTE :: Adding last scene's dialogue.

        return dialogueList.ToArray();
    }
}
