using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스케줄을 관리, 실행해주는 객체
/// </summary>
public class Scheduler : MonoBehaviour
{
    private GameObject[] scheduleObjects;
    private readonly int TOTAL_SCHEDULE_NUM = 24 * 8;

    [SerializeField]
    private GameObject scheduleFactoryPrefab;
    private ScheduleFactory scheduleFactory;


    public GameObject[] ScheduleObjects
    {
        get
        {
            return scheduleObjects;
        }
    }

    public void Awake()
    {
        scheduleFactory = Instantiate<GameObject>(scheduleFactoryPrefab).GetComponent<ScheduleFactory>();
        scheduleObjects = new GameObject[TOTAL_SCHEDULE_NUM];
    }

    public void Add(int index, string scheduleName)
    {
        if (index < 0 || index > PlayingManager.TOTAL_SCHEDULE_NUM)
        {
            return;
        }

        GameObject schedulePrefab = scheduleFactory.GetInstance(scheduleName);
        GameObject scheduleObject = Instantiate<GameObject>(schedulePrefab);
        scheduleObject.transform.parent = transform;
        scheduleObject.SetActive(false);
        scheduleObjects[index] = scheduleObject;
    }

    public void Remove(int index)
    {
        scheduleObjects[index] = null;
    }

    public GameObject GetSchedule(int index)
    {
        return scheduleObjects[index];
    }
}
