using System.Collections.Generic;
using UnityEngine;

public class LineDatabase : MonoBehaviour
{
    public static LineDatabase instance;
    private string csvFileName = "script_data";

    Dictionary<int, List<Dialogue>> dialogueDic = new Dictionary<int, List<Dialogue>>();
    public static bool isFinish = false;

    bool isNext = false;
    int dialogueCnt = 0;
    int contextCnt = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DialogueParser parser = GetComponent<DialogueParser>();
            List<Dialogue>[] dialogues = parser.Parse(csvFileName);

            for (int i = 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i + 1, dialogues[i]);
            }
            isFinish = true;
        }
    }

    public Dialogue[] GetDialogue(int scene)
    {
        return dialogueDic[scene].ToArray();
    }
}
