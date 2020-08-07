using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    private List<string> messages;
    private int messageIndex;
    private Text messageBox;

    void Awake()
    {
        messages = new List<string>();
        messageIndex = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        messageBox = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        NextMessage();
    }

    internal void ReceiveMessages(List<string> newMessage)
    {
        messageIndex = 0;
        messages = newMessage;
    }

    /// <summary>
    /// 메세지 배열에 저장된 메세지를 순서대로 보여줍니다.
    /// </summary>
    internal void NextMessage()
    {
        if (messages.Count > 0 && messageIndex < messages.Count)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShowMessage(messageIndex++);
            }
        }
    }

    private void ShowMessage(int index)
    {
        messageBox.text = messages[index];
    }
}
