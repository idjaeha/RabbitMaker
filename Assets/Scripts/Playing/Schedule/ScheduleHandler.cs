using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// schedule을 실행시켜주는 객체
/// </summary>
public class ScheduleHandler : MonoBehaviour
{
    public IEnumerator Handle(GameObject scheduleObject)
    {
        scheduleObject.SetActive(true);
        ISchedule schedule = scheduleObject.GetComponent<ISchedule>();

        schedule.Begin();
        yield return null;
    }
}
