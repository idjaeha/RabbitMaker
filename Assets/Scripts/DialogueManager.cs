using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 대화와 캐릭터 생성 및 삭제를 담당하는 객체입니다.
/// 그 이외의 명령은 CommandHandler에게 맡깁니다.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> characterPrefabs; // 대화에 필요한 content들을 저장한 배열입니다. 각종 캐릭터나 버튼까지 모두 저장합니다.
    private Dictionary<string, GameObject> characterDictionary; // contentPrefabs에 있는 value값을 key값을 통해 쉽게 찾을 수 있게 도와줍니다.

    [SerializeField]
    private GameObject cnvDialoguePrefab;
    private GameObject cnvDialogue;

    private List<GameObject> createdCharacters; // index[0] : Left, index[1]: Center, index[2] : Right
    private const int CHARACTER_POSITION_X = 7;
    private const KeyCode NEXT_COMMAND = KeyCode.Space;
    private readonly float typingMessageIdleTime = 0.05f;
    private bool canNextCommand; // 다음 커맨드로 넘어갈 수 있는 상태인지 나타냅니다.
    private string targetMessage;
    private TextLoader textLoader;
    private List<string> commands;
    private int commandIndex;
    private Text dialogueBox;
    private static DialogueManager instance;
    public static DialogueManager Instance
    {
        get
        {
            return instance;
        }
    }

    private CommandHandler commandHandler;

    private IEnumerator typingEffectCoroutine;
    private string printedMessageInUI;
    private bool isTypingMessage; // 현재 타이핑하고 있는 상태인지 나타냅니다.
    private IEnumerator canNextCommandCoroutine;
    private readonly float canNextCommandIdleTime = 1.0f;

    void Awake()
    {
        InitDialogueManager();
        commands = new List<string>();
        createdCharacters = new List<GameObject>();
        characterDictionary = new Dictionary<string, GameObject>();
        commandHandler = new CommandHandler();
        commandIndex = 0;
        canNextCommand = true;
        isTypingMessage = false;
        InitCharacterDictionary();
        InitDialogueBox();
    }

    private void InitDialogueManager()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void InitCharacterDictionary()
    {
        foreach (GameObject prefab in characterPrefabs)
        {
            characterDictionary.Add(prefab.name, prefab);
        }
    }

    private void InitDialogueBox()
    {
        cnvDialogue = Instantiate<GameObject>(cnvDialoguePrefab);
        cnvDialogue.SetActive(false);
        cnvDialogue.transform.SetParent(transform, true);
        dialogueBox = cnvDialogue.GetComponentInChildren<Text>();
    }

    /// <summary>
    /// 화면에 캐릭터를 생성합니다.
    /// </summary>
    /// <param name="name">캐릭터 이름</param>
    /// <param name="x">x 좌표</param>
    /// <param name="y">y 좌표</param>
    /// <returns>캐릭터 정보</returns>
    private GameObject CreateCharacter(string name, int x, int y)
    {
        GameObject character = null;
        if (characterDictionary.ContainsKey(name))
        {
            Vector3 position = new Vector3(x, y, 0);
            character = Instantiate<GameObject>(characterDictionary[name], position, Quaternion.identity);
        }
        return character;
    }

    // Update is called once per frame
    void Update()
    {
        NextCommand();
    }

    public void ReceiveCommands(List<string> newMessages)
    {
        if (newMessages == null)
        {
            Debug.Log("빈 메세지입니다.");
            commands = new List<string>();
        }
        else
        {
            commands = newMessages;
        }
        commandIndex = 0;
        ExecuteCommand(commandIndex++); // 최초 1회 실행
    }

    public void ReceiveCommandsFile(string messageFileName)
    {
        textLoader = new TextLoader();
        Instance.ReceiveCommands(textLoader.LoadText(messageFileName));
    }

    /// <summary>
    /// 저장된 commands에서 command를 불러옵니다.
    /// </summary>
    private void NextCommand()
    {
        if (commands.Count > 0)
        {
            if (canNextCommand && Input.GetKeyDown(NEXT_COMMAND))
            {
                if (isTypingMessage)
                {
                    PrintMessageInUI();
                    return;
                }

                if (commandIndex < commands.Count)
                {
                    ExecuteCommand(commandIndex++);
                }
            }
        }
    }

    private void ExecuteCommand(int index)
    {
        string rawCommand = commands[index];
        List<string> splitedMessages = new List<string>(rawCommand.Split('/'));
        foreach (string command in splitedMessages)
        {
            if (command.StartsWith("Message"))
            {
                string message = commandHandler.PutStringBracket(command);
                TypeMessage(message);
                ShowDialogueUI();
            }
            else if (command.StartsWith("Character"))
            {
                string[] arguments = commandHandler.PutStringBracket(command).Split(',');
                string name = arguments[0].Trim();
                string stringPositionFlag = arguments[1].Trim();
                CreateCharacter(name, stringPositionFlag);
            }
            else if (command.StartsWith("DeleteDialogueAndCharacter"))
            {
                HiddenDialogueUI();
                DeleteCharacters();
            }
            else if (command.StartsWith("DeleteCharacters"))
            {
                DeleteCharacters();
            }
            else
            {
                commandHandler.Handle(command);
            }
        }
    }

    private void CreateCharacter(string name, string stringPositionFlag)
    {
        int position = 0;
        if (stringPositionFlag.Equals("Left"))
        {
            position = -CHARACTER_POSITION_X;
        }
        else if (stringPositionFlag.Equals("Center"))
        {
            position = 0;
        }
        else if (stringPositionFlag.Equals("Right"))
        {
            position = CHARACTER_POSITION_X;
        }
        GameObject charater = CreateCharacter(name, position, 0);
        createdCharacters.Add(charater);
    }

    /// <summary>
    /// 메세지를 UI에 타이핑 효과를 넣어 출력합니다.
    /// </summary>
    /// <param name="message">출력될 메세지</param>
    private void TypeMessage(string message)
    {
        // 필요한 기능
        // 1. 지정된 시간 뒤에는 스페이스 바를 누르면 모든 텍스트를 출력한다.
        // 2. 일정한 간격으로 글자를 출력한다.
        // 3. 모든 텍스트가 출력된 이후에는 스페이스바를 누르면 다음 명령으로 넘어간다.
        canNextCommand = false;
        targetMessage = message;

        // 1번의 구현
        // 일정 시간이 지난 뒤에 typing을 건너뛰고 모든 text를 보이게 할 수 있는 상태로 변경합니다.
        if (canNextCommandCoroutine != null)
        {
            StopCoroutine(canNextCommandCoroutine);
            canNextCommandCoroutine = null;
        }
        canNextCommandCoroutine = ChangeCanNextCommand();
        StartCoroutine(canNextCommandCoroutine);

        // 2번의 구현
        // 코루틴을 이용하여 반복문을 통해 message를 출력하게 한다.
        if (typingEffectCoroutine != null)
        {
            StopCoroutine(typingEffectCoroutine);
            typingEffectCoroutine = null;
        }
        typingEffectCoroutine = TypingEffect();
        StartCoroutine(typingEffectCoroutine);
    }

    private IEnumerator TypingEffect()
    {
        printedMessageInUI = "";
        isTypingMessage = true;
        for (int index = 0; printedMessageInUI.Length < targetMessage.Length; index++)
        {
            printedMessageInUI += targetMessage[index];
            dialogueBox.text = printedMessageInUI;
            yield return new WaitForSeconds(typingMessageIdleTime);
        }
        isTypingMessage = false;
        canNextCommand = true;
    }

    private IEnumerator ChangeCanNextCommand()
    {
        yield return new WaitForSeconds(canNextCommandIdleTime);
        if (!canNextCommand)
        {
            canNextCommand = true;
        }
    }

    /// <summary>
    /// target 메세지를 UI에 출력합니다.
    /// </summary>
    private void PrintMessageInUI()
    {
        printedMessageInUI = targetMessage;
        dialogueBox.text = printedMessageInUI;
        canNextCommand = true;
        isTypingMessage = false;
    }

    private void HiddenDialogueUI()
    {
        cnvDialogue.SetActive(false);
    }

    private void ShowDialogueUI()
    {
        cnvDialogue.SetActive(true);
    }

    /// <summary>
    /// 생성된 모든 캐릭터들을 지웁니다.
    /// </summary>
    private void DeleteCharacters()
    {
        foreach (GameObject character in createdCharacters)
        {
            Destroy(character);
        }
        createdCharacters.Clear();
    }

    /// <summary>
    /// 지정한 위치에 있는 캐릭터를 지웁니다.
    /// </summary>
    /// <param name="position">Left, Right, Center 중 하나</param>
    private void DeleteCharacter(string position)
    {
        float deletePosision = 0;
        if (position.Equals("Left"))
        {
            deletePosision = -CHARACTER_POSITION_X;
        }
        else if (position.Equals("Right"))
        {
            deletePosision = CHARACTER_POSITION_X;
        }
        else if (position.EndsWith("Center"))
        {
            deletePosision = 0;
        }
        for (int characterIndex = 0; characterIndex < createdCharacters.Count; characterIndex++)
        {
            if (createdCharacters[characterIndex].transform.position.x == deletePosision)
            {
                Destroy(createdCharacters[characterIndex]);
                createdCharacters.RemoveAt(characterIndex);
            }
        }
    }
}
