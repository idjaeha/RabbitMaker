using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MessageManager messageManager;

    // Start is called before the first frame update
    void Start()
    {
        ExecuteMessage("Intro");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ExecuteMessage(string fileNickname)
    {
        string filePath = string.Format("Assets\\3_Sources\\2_Dialogue\\{0}.txt", fileNickname);
        TextLoader textLoader = new TextLoader();
        List<string> newMessages = textLoader.LoadText(fileNickname);
        messageManager.ReceiveMessages(newMessages);
    }


}
