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
            Debug.Log(name);
            scheduleDictionary.Add(name, schedule);
        }
    }

    public GameObject GetInstance(string name)
    {
        return scheduleDictionary[name];
    }
}
