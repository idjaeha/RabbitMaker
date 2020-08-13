using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class School : MonoBehaviour, ISchedule
{
    private string id = "School";
    private int maxCount = 5;
    private int count;
    private ScheduleState state;
    private float workTime;
    private readonly float workFinishTime = 1.0f;

    public ScheduleState State
    {
        get { return state; }
    }

    public string ID
    {
        get { return id; }
    }

    private void Update()
    {
        if (state == ScheduleState.Progressing)
        {
            Progressing();
        }
    }

    public void Awake()
    {
        count = 0;
        workTime = 0.0f;
        state = ScheduleState.Waiting;
    }

    public void Begin()
    {
        Debug.Log("Begin School");
        GameManager.Instance.ChangeBackground("School");
        state = ScheduleState.Progressing;
    }

    public void End()
    {
        Debug.Log("End School");
        GameManager.Instance.ChangeBackground("RabbitRoom");
        state = ScheduleState.Finish;
        PlayingManager.PlusCurrentScheduleIndex();
        gameObject.SetActive(false);
        DialogueManager.Instance.ReceiveCommandsFile("EndSchedule");
    }

    public void Pause()
    {
        throw new System.NotImplementedException();
    }

    public void Progress()
    {
        state = ScheduleState.Progressing;
    }

    public void Progressing()
    {
        workTime += Time.deltaTime;
        if (count > maxCount)
        {
            End();
            return;
        }

        if (workTime > workFinishTime)
        {
            Debug.Log($"수업을 배웠다. {count}/{maxCount}"); // 스탯이나 변동 사항 생기는 부분
            workTime = 0;
            count++;
        }
    }


}
