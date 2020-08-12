using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private List<string> dialogues;
    private int dialogueIndex;
    private Text dialogueBox;

    void Awake()
    {
        dialogues = new List<string>();
        dialogueIndex = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = gameObject.GetComponentInChildren<Text>();
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
    }

    /// <summary>
    /// 메세지 배열에 저장된 메세지를 순서대로 보여줍니다.
    /// </summary>
    public void NextMessage()
    {
        if (dialogues.Count > 0 && dialogueIndex < dialogues.Count)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShowMessage(dialogueIndex++);
            }
        }
    }

    private void ShowMessage(int index)
    {
        string rawMessage = dialogues[index];
        Debug.Log("Show " + rawMessage);
        List<string> messageArray = new List<string>(rawMessage.Split('/'));
        string flag = messageArray[0];
        if (flag.Equals("Message"))
        {
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
            // GameManager.Instance.CreateCharacter(characterName, 0, 0);
            dialogueBox.text = messageArray[2];
        }
        else if (messageArray.Count == 4) // 인물, 위치, 대화 내용 포함
        {
            string characterName = messageArray[1];
            string characterPosition = messageArray[2];
            if (characterPosition.Equals("Left"))
            {
                // GameManager.Instance.CreateCharacter(characterName, -7, 0);
            }
            else if (characterPosition.Equals("Right"))
            {
                // GameManager.Instance.CreateCharacter(characterName, 7, 0);
            }
            else if (characterPosition.Equals("Center"))
            {
                // GameManager.Instance.CreateCharacter(characterName, 0, 0);
            }
            dialogueBox.text = messageArray[3];
        }
    }

    // TODO : 추후 개선 필요, 언제 hidden이 될 것인지
    private void HiddenUI()
    {
        for (int index = 0; index < transform.childCount; index++)
        {
            Transform childTransform = transform.GetChild(index);
            childTransform.gameObject.SetActive(false);
        }
    }

    // TODO : 추후 개선 필요, 언제 show가 될 것인지
    private void ShowUI()
    {
        for (int index = 0; index < transform.childCount; index++)
        {
            Transform childTransform = transform.GetChild(index);
            childTransform.gameObject.SetActive(true);
        }
    }

    // TODO : Typing 효과 구현하기
    // TODO : 대화할 때 캐릭터의 일러스트가 나오는 것 구현
}
