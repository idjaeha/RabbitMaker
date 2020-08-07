using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextLoader : MonoBehaviour
{
    private readonly string textFilePath = "Assets\\3_Sources\\2_Dialogue\\{0}.txt";
    public List<string> LoadText(string fileNickname)
    {
        string filePath = string.Format(textFilePath, fileNickname);
        List<string> texts = new List<string>();
        foreach (string text in File.ReadLines(filePath))
        {
            texts.Add(text);
        }
        return texts;
    }
}
