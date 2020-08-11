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

    [SerializeField]
    private GameObject scheduleHandlerPrefab;
    private ScheduleHandler scheduleHandler;


    private void Awake()
    {
        currentScheduleIndex = 0;
        scheduler = Instantiate<GameObject>(schedulerPrefab).GetComponent<Scheduler>();
        scheduleHandler = Instantiate<GameObject>(scheduleHandlerPrefab).GetComponent<ScheduleHandler>();
    }

    private void Start()
    {
        scheduler.Add(0, "School");
        scheduler.Add(1, "School");
    }

    public void StartSchedule()
    {
        GameObject scheduleObject = scheduler.GetSchedule(currentScheduleIndex);
        if (scheduleObject != null)
        {
            StartCoroutine(scheduleHandler.Handle(scheduleObject));
        }
        else
        {
            Debug.Log("스케줄이 비었습니다.");
        }
    }

    public static void PlusCurrentScheduleIndex()
    {
        if (currentScheduleIndex < TOTAL_SCHEDULE_NUM)
        {
            currentScheduleIndex++;
        }
    }
}
