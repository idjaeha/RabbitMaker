using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스케줄을 사용할 수 있도록 스케줄을 생성해주는 객체
/// </summary>
public class ScheduleFactory : MonoBehaviour
{
    public GameObject[] schedulePrefabs;
    private Dictionary<string, GameObject> scheduleDictionary;

    private void Awake()
    {
        scheduleDictionary = new Dictionary<string, GameObject>();
        InitDictionary();
    }

    private void InitDictionary()
    {
        foreach (GameObject schedule in schedulePrefabs)
        {
            string name = schedule.GetComponent<ISchedule>().ID;
            scheduleDictionary.Add(name, schedule);
        }
    }

    public GameObject GetInstance(string name)
    {
        if (scheduleDictionary.ContainsKey(name))
        {
            return scheduleDictionary[name];
        }
        else
        {
            Debug.Log($"{name}에 해당하는 스케줄이 존재하지 않습니다.");
            return null;
        }
    }
}
