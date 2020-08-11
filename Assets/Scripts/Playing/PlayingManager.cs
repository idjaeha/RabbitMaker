using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingManager : MonoBehaviour
{
    private const int TOTAL_SCHEDULE_NUM = 24 * 8;
    private static int currentScheduleIndex;

    [SerializeField]
    private GameObject schedulerPrefab;
    private Scheduler scheduler;

    private void Awake()
    {
        currentScheduleIndex = 0;
        scheduler = Instantiate<GameObject>(schedulerPrefab).GetComponent<Scheduler>();

    }

    private void Start()
    {
        scheduler.Add(currentScheduleIndex, "School");
        StartSchedule();
    }

    public void StartSchedule()
    {
        scheduler.BeginSchedule(currentScheduleIndex);
    }

    public static void PlusCurrentScheduleIndex()
    {
        if (currentScheduleIndex < TOTAL_SCHEDULE_NUM)
        {
            currentScheduleIndex++;
        }
    }

    public void Show()
    {
        Debug.Log(scheduler.GetSchedule(0));
    }
}
