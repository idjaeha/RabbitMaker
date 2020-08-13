using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterPosition
{
    Left = 0,
    Center = 1,
    Right = 2
}

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> contentPrefabs; // 대화에 필요한 content들을 저장한 배열입니다. 각종 캐릭터나 버튼까지 모두 저장합니다.
    private Dictionary<string, GameObject> contentDictionary; // contentPrefabs에 있는 value값을 key값을 통해 쉽게 찾을 수 있게 도와줍니다.

    [SerializeField]
    private GameObject cnvDialoguePrefab;
    private GameObject cnvDialogue;

    private GameObject[] createdCharacters; // index[0] : Left, index[1]: Center, index[2] : Right
    private const int CREATED_CHARACTERS_SIZE = 3;

    private TextLoader textLoader;
    private List<string> dialogues;
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

    void Awake()
    {
        InitGameManager();
        dialogues = new List<string>();
        textLoader = new TextLoader();
        createdCharacters = new GameObject[3];
        contentDictionary = new Dictionary<string, GameObject>();
        commandIndex = 0;
        InitCharacterDictionary();
        InitDialogueBox();
    }

    private void InitGameManager()
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
        foreach (GameObject prefab in contentPrefabs)
        {
            contentDictionary.Add(prefab.name, prefab);
        }
    }

    private void InitDialogueBox()
    {
        cnvDialogue = Instantiate<GameObject>(cnvDialoguePrefab);
        cnvDialogue.SetActive(false);
        cnvDialogue.transform.parent = transform;
        dialogueBox = cnvDialogue.GetComponentInChildren<Text>();
    }

    /// <summary>
    /// 화면에 캐릭터를 생성합니다.
    /// </summary>
    /// <param name="name">캐릭터 이름</param>
    /// <param name="x">x 좌표</param>
    /// <param name="y">y 좌표</param>
    /// <returns>캐릭터 정보</returns>
    public GameObject CreateContent(string name, int x, int y)
    {
        GameObject character = null;
        if (contentDictionary.ContainsKey(name))
        {
            Vector3 position = new Vector3(x, y, 0);
            character = Instantiate<GameObject>(contentDictionary[name], position, Quaternion.identity);
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
            dialogues = new List<string>();
        }
        else
        {
            dialogues = newMessages;
        }
        commandIndex = 0;
        ExecuteCommand(commandIndex++); // 최초 1회 실행
    }

    public void ReceiveCommandsFile(string messageFileName)
    {
        ReceiveCommands(textLoader.LoadText(messageFileName));
    }

    /// <summary>
    /// 메세지 배열에 저장된 메세지를 순서대로 보여줍니다.
    /// </summary>
    public void NextCommand()
    {
        if (dialogues.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (commandIndex < dialogues.Count)
                {
                    ExecuteCommand(commandIndex++);
                }
            }
        }
    }

    private void ExecuteCommand(int index)
    {
        string rawCommand = dialogues[index];
        List<string> commands = new List<string>(rawCommand.Split('/'));
        foreach (string command in commands)
        {
            CommandHandler(command);
        }

    }

    private void CommandHandler(string command)
    {
        if (command.StartsWith("Message"))
        {
            string message = PutStringBracket(command);
            dialogueBox.text = message;
            cnvDialogue.SetActive(true);
        }
        else if (command.StartsWith("Character"))
        {
            string[] arguments = PutStringBracket(command).Split(',');
            string name = arguments[0].Trim();
            string stringPositionFlag = arguments[1].Trim();
            CharacterPosition positionFlag = (CharacterPosition)Enum.Parse(typeof(CharacterPosition), stringPositionFlag);
            int position = 0;
            if (positionFlag == CharacterPosition.Left)
            {
                position = -7;
            }
            else if (positionFlag == CharacterPosition.Center)
            {
                position = 0;
            }
            else if (positionFlag == CharacterPosition.Right)
            {
                position = 7;
            }
            GameObject charater = CreateContent(name, position, 0);
            createdCharacters[(int)positionFlag] = charater;
        }
        else if (command.StartsWith("DeleteAll"))
        {
            HiddenDialogueUI();
            DeleteCharacters();
        }
    }

    /// <summary>
    /// [] 괄호 안에서 문자열을 뽑아냅니다.
    /// 괄호가 제대로 형성되어 있지 않다면 빈 문자열을 반환한다.
    /// </summary>
    /// <param name="rawString">[] 가 포함된 문자열</param>
    /// <returns></returns>
    private string PutStringBracket(string rawString)
    {
        string newString = "";
        bool canAdd = false;

        foreach (char character in rawString)
        {
            if (character.Equals('['))
            {
                canAdd = true;
            }
            else if (character.Equals(']'))
            {
                canAdd = false;
                break;
            }
            else if (canAdd)
            {
                newString += character;
            }
        }

        return newString;
    }

    // TODO : 추후 개선 필요, 언제 hidden이 될 것인지
    private void HiddenDialogueUI()
    {
        cnvDialogue.SetActive(false);
    }

    // TODO : 추후 개선 필요, 언제 show가 될 것인지
    private void ShowDialogueUI()
    {
        cnvDialogue.SetActive(true);
    }

    /// <summary>
    /// 생성된 모든 캐릭터들을 지웁니다.
    /// </summary>
    private void DeleteCharacters()
    {
        for (int index = 0; index < CREATED_CHARACTERS_SIZE; index++)
        {
            if (createdCharacters[index] != null)
            {
                Destroy(createdCharacters[index]);
            }
            createdCharacters[index] = null;
        }
    }

    /// <summary>
    /// 해당하는 위치에 있는 생성된 캐릭터를 지웁니다.
    /// </summary>
    /// <param name="index">지울 위치</param>
    private void DeleteCharacter(int index)
    {
        if (createdCharacters[index] != null)
        {
            Destroy(createdCharacters[index]);
        }
        createdCharacters[index] = null;
    }

    // TODO : Typing 효과 구현하기
}
