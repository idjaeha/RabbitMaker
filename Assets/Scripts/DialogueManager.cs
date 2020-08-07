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
        dialogueBox.text = dialogues[index];
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
}
