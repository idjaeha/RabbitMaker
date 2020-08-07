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
        PostMessages("Assets\\3_Sources\\2_Dialogue\\Intro.txt");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PostMessages(string filePath)
    {
        TextLoader textLoader = new TextLoader();
        List<string> newMessages = textLoader.LoadText(filePath);
        messageManager.ReceiveMessages(newMessages);
    }


}
