using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject messageManagerPrefab;
    [SerializeField]
    private GameObject backGroundManagerPrefab;

    private MessageManager messageManager;
    private BackgroundManager backgroundManager;

    /// <summary>
    /// 각종 매니저가 있는지 확인한 뒤, 없다면 새로 생성하여 초기화해줍니다.
    /// </summary>
    private void InitManagers()
    {
        messageManager = FindObjectOfType<MessageManager>();
        if (messageManager == null)
        {
            messageManager = Instantiate(messageManagerPrefab, Vector3.zero, Quaternion.identity).GetComponent<MessageManager>();
        }

        backgroundManager = FindObjectOfType<BackgroundManager>();
        if (backgroundManager == null)
        {
            backgroundManager = Instantiate(backGroundManagerPrefab, Vector3.zero, Quaternion.identity).GetComponent<BackgroundManager>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitManagers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// fileNickName에 해당하는 메세지들을 불러와 messageManager에게 보냅니다.
    /// </summary>
    /// <param name="fileNickname">텍스트 파일의 확장자를 제외한 이름</param>
    void PostMessagesFromTextFile(string fileNickname)
    {
        TextLoader textLoader = new TextLoader();
        List<string> newMessages = textLoader.LoadText(fileNickname);
        messageManager.GetComponent<MessageManager>().ReceiveMessages(newMessages);
    }


}
