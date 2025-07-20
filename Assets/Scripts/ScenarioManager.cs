using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{
    private static ScenarioManager instance = null;
    public static ScenarioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScenarioManager>();

                if (instance == null)
                {
                    Debug.LogError("ScenarioManager instance not available.");
                }
            }
            return instance;
        }
    }

    [SerializeField]
    private TextMeshProUGUI characterName;
    [SerializeField]
    public TextMeshProUGUI characterLine;
    [SerializeField]
    public TextMeshProUGUI mologue;
    [SerializeField]
    public Button dialogueBG;
    bool isDialogue;
    Dialogue[] dialogues;
    bool isNext = false;
    bool isNextBtnClicked = false;
    int dialogueCnt = 0;
    int contextCnt = 0;

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

    public void ShowDialogue(Dialogue[] aDialogues)
    {
        isDialogue = true;
        characterName.text = "";
        characterLine.text = "";
        mologue.text = "";

        dialogues = aDialogues;

        SettingUI(true);
        StartCoroutine(TypeWriter());
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        SettingUI(false);
    }

    void Start()
    {
        dialogueBG.onClick.AddListener(OnDialogueBtnClicked);
    }

    void Update()
    {
        if (isDialogue)
        {
            if (isNext)
            {
                if (Input.GetKeyDown(KeyCode.Space) || isNextBtnClicked)
                {
                    isNext = false;
                    isNextBtnClicked = false;
                    characterLine.text = "";

                    if (++contextCnt < dialogues[dialogueCnt].context.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }

                    else
                    {
                        contextCnt = 0;

                        if (++dialogueCnt < dialogues.Length)
                        {
                            StartCoroutine(TypeWriter());
                        }
                    }
                }
            }
        }
    }

    private void OnDialogueBtnClicked()
    {
        if (!isNextBtnClicked)
            isNextBtnClicked = true;
    }

    IEnumerator TypeWriter()
    {
        isDialogue = true;
        SettingUI(true);
        string replaceText = dialogues[dialogueCnt].context[contextCnt];
        if (dialogues[dialogueCnt].name == "Monologue")
        {
            characterLine.GetComponent<CanvasRenderer>().SetAlpha(0);
            characterName.GetComponent<CanvasRenderer>().SetAlpha(0);
            mologue.GetComponent<CanvasRenderer>().SetAlpha(100);
            mologue.SetText(replaceText);
        }
        else
        {
            characterLine.GetComponent<CanvasRenderer>().SetAlpha(100);
            characterName.GetComponent<CanvasRenderer>().SetAlpha(100);
            mologue.GetComponent<CanvasRenderer>().SetAlpha(0);
            characterName.text = dialogues[dialogueCnt].name;
            characterLine.text = replaceText;
        }
        isNext = true;
        yield return null;
    }
}
