using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextLoader : MonoBehaviour
{
    public List<string> LoadText(string filePath)
    {
        List<string> texts = new List<string>();
        foreach (string text in File.ReadLines(filePath))
        {
            texts.Add(text);
        }
        return texts;
    }
}
