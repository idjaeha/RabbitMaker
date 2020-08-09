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

    // Update is called once per frame
    void Update()
    {

    }

    // TODO : Text를 불러오는 것이 DialogueManager에서 이뤄지는 것이 더 옳은 방향일 수 있다. 설계가 조금 더 필요하다.
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
