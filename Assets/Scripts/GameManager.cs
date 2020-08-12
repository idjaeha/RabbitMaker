using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject backgroundPrefab;
    [SerializeField]
    private GameObject sceneLoaderPrefab;

    private Background background;
    private SceneLoader sceneLoader;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private Background Background
    {
        get
        {
            // Background를 hierarchy에서 찾습니다.
            if (background == null)
            {
                background = GameObject.FindGameObjectWithTag("Background")?.GetComponent<Background>();
            }

            // hierarchy에 존재하지 않는다면 새로 만듭니다.
            if (background == null)
            {
                background = Instantiate<GameObject>(backgroundPrefab)?.GetComponent<Background>();
            }

            return background;
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

    // /// <summary>
    // /// fileNickName에 해당하는 메세지들을 불러와 dialogueManager에게 보냅니다.
    // /// </summary>
    // /// <param name="fileNickname">텍스트 파일의 확장자를 제외한 이름</param>
    // public void PostMessagesFromTextFile(string fileNickname)
    // {
    //     TextLoader textLoader = new TextLoader();
    //     List<string> newMessages = textLoader.LoadText(fileNickname);
    //     Instance.dialogueManager.GetComponent<DialogueManager>().ReceiveMessages(newMessages);
    // }

    // /// <summary>
    // /// messages에 해당하는 메세지들을 dialogueManager에게 보냅니다.
    // /// </summary>
    // /// <param name="messages">List<string> 형태로 메세지를 저장한 배열</param>
    // void PostMessagesFromList(List<string> messages)
    // {
    //     dialogueManager.GetComponent<DialogueManager>().ReceiveMessages(messages);
    // }

    public void MoveScene(string sceneName)
    {
        if (sceneLoader == null)
        {
            sceneLoader = Instantiate<GameObject>(sceneLoaderPrefab).GetComponent<SceneLoader>();
        }
        sceneLoader.LoadSceneWithFadeIn(LoadSceneMode.Single, sceneName);
    }

    public void ChangeBackground(string backgroundName)
    {
        Background.ChangeBackground(backgroundName);
    }
}
