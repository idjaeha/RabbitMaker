using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextLoader
{
    private readonly string TEXT_FILE_PATH = "Assets\\Resources\\Dialogue\\{0}.txt";

    public List<string> LoadText(string fileNickname)
    {
        string filePath = string.Format(TEXT_FILE_PATH, fileNickname);
        List<string> texts = new List<string>();

        if (File.Exists(filePath))
        {
            foreach (string text in File.ReadLines(filePath))
            {
                texts.Add(text);
            }
        }
        else
        {
            Debug.Log($"{fileNickname} 파일이 존재하지 않습니다.");
        }

        return texts;
    }
}
