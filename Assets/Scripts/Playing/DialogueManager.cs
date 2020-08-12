using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterPosition
{
    Left = 0,
    Center = 1,
    Right = 0
}

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> characterPrefabs;
    private Dictionary<string, GameObject> characterDictionary;

    [SerializeField]
    private GameObject cnvDialoguePrefab;
    private GameObject cnvDialogue;

    private GameObject[] createdCharacters; // index[0] : Left, index[1]: Center, index[2] : Right
    private const int CREATED_CHARACTERS_SIZE = 3;

    private TextLoader textLoader;
    private List<string> dialogues;
    private int dialogueIndex;
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
        characterDictionary = new Dictionary<string, GameObject>();
        dialogueIndex = 0;
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
    public GameObject CreateCharacter(string name, int x, int y)
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
        NextMessage();
    }

    public void ReceiveMessages(List<string> newMessages)
    {
        if (newMessages == null)
        {
            Debug.Log("빈 메세지입니다.");
            dialogues = new List<string>();
        }
        else
        {
            foreach (var message in newMessages)
            {
                Debug.Log(message);
            }
            dialogues = newMessages;
        }
        dialogueIndex = 0;
        ExecuteMessage(dialogueIndex++); // 최초 1회 실행
    }

    public void ReceiveMessagesFile(string messageFileName)
    {
        ReceiveMessages(textLoader.LoadText(messageFileName));
    }

    /// <summary>
    /// 메세지 배열에 저장된 메세지를 순서대로 보여줍니다.
    /// </summary>
    public void NextMessage()
    {
        if (dialogues.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dialogueIndex < dialogues.Count)
                {
                    ExecuteMessage(dialogueIndex++);
                }
                else
                {
                    DeleteCharacters();
                    HiddenDialogueUI();
                }
            }
        }
    }

    private void ExecuteMessage(int index)
    {
        string rawMessage = dialogues[index];
        Debug.Log("Show " + rawMessage);
        List<string> messageArray = new List<string>(rawMessage.Split('/'));
        string flag = messageArray[0];
        if (flag.Equals("Message"))
        {
            DeleteCharacters();
            ShowDialogueUI();
            MessageHandler(messageArray);
        }
    }

    private void MessageHandler(List<string> messageArray)
    {
        if (messageArray.Count == 2) // 대화 내용만 포함
        {
            dialogueBox.text = messageArray[1];
        }
        else if (messageArray.Count == 3) // 인물, 대화 내용 포함
        {
            string characterName = messageArray[1];
            CreateCharacter(characterName, 0, 0);
            dialogueBox.text = messageArray[2];
        }
        else if (messageArray.Count == 4) // 인물, 위치, 대화 내용 포함
        {
            string characterName = messageArray[1];
            string characterPositionFlag = messageArray[2];
            int x = 0;
            CharacterPosition characterPosition = 0;
            if (characterPositionFlag.Equals("Left"))
            {
                x = -7;
                characterPosition = CharacterPosition.Left;
            }
            else if (characterPositionFlag.Equals("Right"))
            {
                x = 7;
                characterPosition = CharacterPosition.Right;
            }
            else if (characterPositionFlag.Equals("Center"))
            {
                x = 0;
                characterPosition = CharacterPosition.Center;
            }
            GameObject createdCharacter = CreateCharacter(characterName, x, 0);
            createdCharacters[(int)characterPosition] = createdCharacter;
            dialogueBox.text = messageArray[3];
        }
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
