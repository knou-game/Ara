using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CommandType
{
    SCENE,
    BACKGROUND,
    SHOW_CHAR,
    HIDE_CHAR,
    DIALOGUE,
    CHARACTER,
    QUEST
}

public class ScenarioManager : SingletonPersistent<ScenarioManager>
{
    [SerializeField]
    private TextMeshProUGUI characterName;
    [SerializeField]
    public TextMeshProUGUI characterLine;
    [SerializeField]
    public TextMeshProUGUI mologue;
    [SerializeField]
    public Button dialogueBG;

    // variables about Commands
    public CommandClass commands;  // NOTE :: Current scene's scenario command.
    public int currentCommandIndex;
    private CommandBase currentCommand;

    // Is command running
    private bool isScenraioRunning = false;

    // variables about Dialogue 
    bool isNextBtnClicked = false;

    public void SettingUI(bool trig)
    {
        if (trig)
        {
            dialogueBG.GetComponent<CanvasRenderer>().SetAlpha(100);
            dialogueBG.GetComponent<Graphic>().raycastTarget = true;
        }
        else
        {
            dialogueBG.GetComponent<CanvasRenderer>().SetAlpha(0);
            dialogueBG.GetComponent<Graphic>().raycastTarget = false;
            characterLine.GetComponent<CanvasRenderer>().SetAlpha(0);
            characterName.GetComponent<CanvasRenderer>().SetAlpha(0);
            mologue.GetComponent<CanvasRenderer>().SetAlpha(0);
        }
    }

    public void StartScenario()
    {
        StartCoroutine(RunScenario());
    }

    private IEnumerator RunScenario()
    {
        while (currentCommandIndex < commands.commandList.Length)
        {
            currentCommand = commands.commandList[currentCommandIndex];

            switch (currentCommand.type)
            {
                case CommandType.SCENE:
                    Debug.Log("SCENE changed");
                    break;
                case CommandType.BACKGROUND:
                    Debug.Log("Background change");
                    break;
                case CommandType.SHOW_CHAR:
                    yield return StartCoroutine(RunShowChar());
                    break;
                case CommandType.HIDE_CHAR:
                    yield return StartCoroutine(RunHideChar());
                    break;
                case CommandType.DIALOGUE:
                    yield return StartCoroutine(RunDialogue());
                    break;
                case CommandType.CHARACTER:
                    Debug.Log("Character changed");
                    break;
                case CommandType.QUEST:
                    Debug.Log("Quest changed");
                    break;
            }

            currentCommandIndex++;
            yield return null;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        SettingUI(false);
    }

    protected override void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        base.OnSceneLoaded(scene, mode);
        FindUIElements();
    }

    private void FindUIElements()
    {
        characterName = GameObject.Find("CharacterName").GetComponent<TextMeshProUGUI>();
        characterLine = GameObject.Find("CharacterLine").GetComponent<TextMeshProUGUI>();
        mologue = GameObject.Find("Mologue").GetComponent<TextMeshProUGUI>();
        dialogueBG = GameObject.Find("DialogueBG").GetComponent<Button>();
    }

    void Start()
    {
        FindUIElements();
        dialogueBG.onClick.AddListener(OnDialogueBtnClicked);
    }

    void Update()
    {

    }

    private void OnDialogueBtnClicked()
    {
        if (!isNextBtnClicked)
            isNextBtnClicked = true;
    }

    IEnumerator RunShowChar()
    {
        SettingUI(true);
        yield return null;
    }

    IEnumerator RunHideChar()
    {
        SettingUI(false);
        yield return null;
    }

    IEnumerator RunDialogue()
    {
        characterName.text = "";
        characterLine.text = "";
        mologue.text = "";
        DialogueModel currentDialogue = (DialogueModel)currentCommand;
        string replaceText = currentDialogue.context;
        isNextBtnClicked = false;
        if (currentDialogue.name == "Monologue")
        {
            characterLine.GetComponent<CanvasRenderer>().SetAlpha(0);
            characterName.GetComponent<CanvasRenderer>().SetAlpha(0);
            mologue.GetComponent<CanvasRenderer>().SetAlpha(100);
            foreach (char letter in replaceText.ToCharArray())
            {
                if (isNextBtnClicked)
                {
                    mologue.text = replaceText;
                    break;
                }
                mologue.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            characterLine.GetComponent<CanvasRenderer>().SetAlpha(100);
            characterName.GetComponent<CanvasRenderer>().SetAlpha(100);
            mologue.GetComponent<CanvasRenderer>().SetAlpha(0);
            characterName.text = currentDialogue.name;
            foreach (char letter in replaceText.ToCharArray())
            {
                if (isNextBtnClicked)
                {
                    characterLine.text = replaceText;
                    break;
                }
                characterLine.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
        }
        isNextBtnClicked = true;
        yield return new WaitUntil(() => !isNextBtnClicked);
    }
}
