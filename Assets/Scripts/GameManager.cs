using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueManagerPrefab;
    [SerializeField]
    private GameObject backgroundPrefab;

    private DialogueManager dialogueManager;
    private Background background;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        InitGameManager();
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

    /// <summary>
    /// 각종 매니저가 있는지 확인한 뒤, 없다면 새로 생성하여 초기화해줍니다.
    /// </summary>
    private void InitManagers()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager == null)
        {
            dialogueManager = Instantiate(dialogueManagerPrefab, Vector3.zero, Quaternion.identity).GetComponent<DialogueManager>();
        }
        background = FindObjectOfType<Background>();
        if (background == null)
        {
            background = Instantiate(backgroundPrefab, Vector3.zero, Quaternion.identity).GetComponent<Background>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitManagers();
    }

    /// <summary>
    /// fileNickName에 해당하는 메세지들을 불러와 messageManager에게 보냅니다.
    /// </summary>
    /// <param name="fileNickname">텍스트 파일의 확장자를 제외한 이름</param>
    void PostMessagesFromTextFile(string fileNickname)
    {
        TextLoader textLoader = new TextLoader();
        List<string> newMessages = textLoader.LoadText(fileNickname);
        dialogueManager.GetComponent<DialogueManager>().ReceiveMessages(newMessages);
    }


}
