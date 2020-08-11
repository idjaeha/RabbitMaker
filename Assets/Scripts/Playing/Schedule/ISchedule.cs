using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISchedule
{
    ScheduleState State
    {
        get;
    }
    string ID
    {
        get;
    }
    void Begin();
    void Progress();
    void Pause();
    void End();
}
