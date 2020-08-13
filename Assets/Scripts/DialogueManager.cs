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

    private CommandHandler commandHandler;

    void Awake()
    {
        InitDialogueManager();
        dialogues = new List<string>();
        createdCharacters = new List<GameObject>();
        characterDictionary = new Dictionary<string, GameObject>();
        commandHandler = new CommandHandler();
        commandIndex = 0;
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
        textLoader = new TextLoader();
        Instance.ReceiveCommands(textLoader.LoadText(messageFileName));
    }

    /// <summary>
    /// 메세지 배열에 저장된 메세지를 순서대로 보여줍니다.
    /// </summary>
    private void NextCommand()
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
            if (command.StartsWith("Message"))
            {
                string message = commandHandler.PutStringBracket(command);
                EnterMessageIntoDialogue(message);
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

    private void EnterMessageIntoDialogue(string message)
    {
        dialogueBox.text = message;
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

    // TODO : Typing 효과 구현하기
}
