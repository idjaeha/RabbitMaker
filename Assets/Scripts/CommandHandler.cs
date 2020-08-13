using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler
{
    private PlayingManager _playingManager;
    private PlayingManager playingManager
    {
        get
        {
            if (_playingManager == null)
            {
                _playingManager = GameObject.FindGameObjectWithTag("PlayingManager").GetComponent<PlayingManager>();
            }
            return _playingManager;
        }
    }

    public CommandHandler()
    {
        InitActionDictionary();
    }

    private Dictionary<string, Action> actionDictionary;

    private void InitActionDictionary()
    {
        actionDictionary = new Dictionary<string, Action>();
        actionDictionary.Add("ShowMenuUI", playingManager.ShowMenuUI);
        actionDictionary.Add("HiddenMenuUI", playingManager.HiddenMenuUI);
        actionDictionary.Add("CreateDecisionUI", playingManager.CreateDecisionUI);
        actionDictionary.Add("DeleteDecisionUI", playingManager.DeleteDecisionUI);
        actionDictionary.Add("StartSchedule", playingManager.StartSchedule);
    }

    // TODO : 인자가 있을 경우 어떻게 해결할 것인가?
    // public void Handle(string command)
    // {
    //     if (command.StartsWith("ShowMenuUI"))
    //     {
    //         playingManager.ShowMenuUI();
    //     }
    //     else if (command.StartsWith("HiddenMenuUI"))
    //     {
    //         playingManager.HiddenMenuUI();
    //     }
    //     else if (command.StartsWith("CreateDecisionUI"))
    //     {
    //         playingManager.CreateDecisionUI();
    //     }
    //     else if (command.StartsWith("DeleteDecisionUI"))
    //     {
    //         playingManager.DeleteDecisionUI();
    //     }
    //     else if (command.StartsWith("StartSchedule"))
    //     {
    //         playingManager.StartSchedule();
    //     }
    // }

    public void Handle(string command)
    {
        actionDictionary[command]();
    }

    /// <summary>
    /// [] 괄호 안에서 문자열을 뽑아냅니다.
    /// 괄호가 제대로 형성되어 있지 않다면 빈 문자열을 반환한다.
    /// </summary>
    /// <param name="rawString">[] 가 포함된 문자열</param>
    /// <returns>괄호 안에 문자열</returns>
    public string PutStringBracket(string rawString)
    {
        string newString = "";
        bool canAdd = false;

        foreach (char character in rawString)
        {
            if (character.Equals('['))
            {
                canAdd = true;
            }
            else if (character.Equals(']'))
            {
                canAdd = false;
                break;
            }
            else if (canAdd)
            {
                newString += character;
            }
        }

        return newString;
    }
}
