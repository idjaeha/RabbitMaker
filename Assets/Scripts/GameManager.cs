using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueManagerPrefab;
    [SerializeField]
    private GameObject backgroundPrefab;
    [SerializeField]
    private GameObject sceneLoaderPrefab;

    private DialogueManager dialogueManager;
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

    public void MoveScene(string sceneName)
    {
        if (sceneLoader == null)
        {
            sceneLoader = Instantiate<GameObject>(sceneLoaderPrefab).GetComponent<SceneLoader>();
        }
        sceneLoader.LoadSceneWithFadeIn(LoadSceneMode.Single, sceneName);
    }
}
