using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;

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
    private TextMeshProUGUI characterName;
    private TextMeshProUGUI characterLine;
    private TextMeshProUGUI mologue;
    private Button dialogueBG;


    // variables about Commands
    public CommandClass commands;  // NOTE :: Current scene's scenario command.
    public int currentCommandIndex;
    private CommandBase currentCommand;

    // variables about Dialogue 
    bool isNextBtnClicked = false;

    // variable for fadeController
    public FadeController fadeController;

    // variable for quest ui
    private TextMeshProUGUI questTitle;
    private TextMeshProUGUI questDetail;
    private Image questBaseImage;
    private Image questCheckboxImage;

    // variable for quest process
    private GameObject currentQuestObj;
    private QuestBase currentQuest;
    private Sprite questCompleteBaseImageSource;
    private Sprite questUncompleteBaseImageSource;
    private Sprite questUnCheckboxImageSource;
    private Sprite questCheckboxImageSource;
    private AsyncOperationHandle<IList<Sprite>> questImageLoadHandle;

    [SerializeField]
    public List<GameObject> questList;

    public void StartScenario()
    {
        StartCoroutine(RunScenario());
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
        questTitle = GameObject.Find("QuestTitle").GetComponent<TextMeshProUGUI>();
        questDetail = GameObject.Find("QuestDetail").GetComponent<TextMeshProUGUI>();
        questCheckboxImage = GameObject.Find("CheckboxImage").GetComponent<Image>();
        questBaseImage = GameObject.Find("BaseImage").GetComponent<Image>();
    }

    private IEnumerator LoadImageFromAddress()
    {
        var assetKeys = new System.Collections.Generic.List<string>
        {
            "Assets/UI/quest_uncompletly_base.png",
            "Assets/UI/quest_completly_base.png",
            "Assets/UI/unchecked.png",
            "Assets/UI/check-box-checked.png"
        };

        questImageLoadHandle = Addressables.LoadAssetsAsync<Sprite>(assetKeys, item => { }, Addressables.MergeMode.Union);
        yield return questImageLoadHandle;

        if (questImageLoadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            System.Collections.Generic.IList<Sprite> sprites = questImageLoadHandle.Result;
            questUncompleteBaseImageSource = sprites[0];
            questCompleteBaseImageSource = sprites[1];
            questUnCheckboxImageSource = sprites[2];
            questCheckboxImageSource = sprites[3];
        }
        else
        {
            Debug.LogError("Quest UI Sprites load failed.");
        }
    }

    void OnDestroy()
    {
        if (questImageLoadHandle.IsValid())
        {
            Addressables.Release(questImageLoadHandle);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        fadeController = FindFirstObjectByType<FadeController>();
    }

    void Start()
    {
        StartCoroutine(LoadImageFromAddress());
        FindUIElements();
        SettingDiagUI(false);
        SettingQuestUI(false);
        dialogueBG.onClick.AddListener(OnDialogueBtnClicked);
    }

    void Update()
    {

    }

    public void SettingDiagUI(bool trig)
    {
        if (trig)
        {
            dialogueBG.enabled = true;
            dialogueBG.GetComponent<CanvasRenderer>().SetAlpha(100);
            dialogueBG.GetComponent<Graphic>().raycastTarget = true;
        }
        else
        {
            dialogueBG.enabled = false;
            dialogueBG.GetComponent<CanvasRenderer>().SetAlpha(0);
            dialogueBG.GetComponent<Graphic>().raycastTarget = false;
            characterLine.GetComponent<CanvasRenderer>().SetAlpha(0);
            characterName.GetComponent<CanvasRenderer>().SetAlpha(0);
            mologue.GetComponent<CanvasRenderer>().SetAlpha(0);
        }
    }

    public void SettingQuestUI(bool trig)
    {
        if (trig)
        {
            questBaseImage.sprite = questUncompleteBaseImageSource;
            questCheckboxImage.sprite = questUnCheckboxImageSource;
            questBaseImage.GetComponent<CanvasRenderer>().SetAlpha(100);
            questCheckboxImage.GetComponent<CanvasRenderer>().SetAlpha(100);
            questTitle.GetComponent<CanvasRenderer>().SetAlpha(100);
            questDetail.GetComponent<CanvasRenderer>().SetAlpha(100);
        }
        else
        {
            questBaseImage.GetComponent<CanvasRenderer>().SetAlpha(0);
            questCheckboxImage.GetComponent<CanvasRenderer>().SetAlpha(0);
            questTitle.GetComponent<CanvasRenderer>().SetAlpha(0);
            questDetail.GetComponent<CanvasRenderer>().SetAlpha(0);
        }
    }

    private void OnDialogueBtnClicked()
    {
        if (!isNextBtnClicked)
            isNextBtnClicked = true;
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
                    yield return StartCoroutine(RunBackgroundChange());
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
                    yield return StartCoroutine(RunQuest());
                    break;
            }

            currentCommandIndex++;
            yield return null;
        }
    }

    IEnumerator RunBackgroundChange()
    {
        BackgroundModel currentBackground = (BackgroundModel)currentCommand;
        if (currentBackground.command == "Light")
        {
            yield return StartCoroutine(fadeController.Fade(1f - float.Parse(currentBackground.argument)));
        }
        yield return null;
    }

    IEnumerator RunShowChar()
    {
        SettingDiagUI(true);
        yield return null;
    }

    IEnumerator RunHideChar()
    {
        SettingDiagUI(false);
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
        isNextBtnClicked = false;
        yield return new WaitUntil(() => isNextBtnClicked);
    }

    IEnumerator RunQuest()
    {
        QuestModel currentQuestModel = (QuestModel)currentCommand;
        string questClassName = currentQuestModel.questName.Trim();
        currentQuestObj = GameObject.Find(questClassName); 
        currentQuest = currentQuestObj.GetComponent<QuestBase>();
        questTitle.SetText(currentQuestModel.title);
        questDetail.SetText(currentQuestModel.line);
        SettingQuestUI(true);
        yield return StartCoroutine(currentQuest.WaitQuestTrigger());
        questBaseImage.sprite = questCompleteBaseImageSource;
        questCheckboxImage.sprite = questCheckboxImageSource;
        yield return new WaitForSeconds(3.0f);
        SettingQuestUI(false);
    }
}
