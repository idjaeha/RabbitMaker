using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 진행을 담당하며, 게임을 진행을 위한 각종 객체들에게 명령을 내립니다.
/// </summary>
public class PlayingManager : MonoBehaviour
{
    public const int TOTAL_SCHEDULE_NUM = 24 * 8;
    private static int currentScheduleIndex;

    [SerializeField]
    private GameObject schedulerPrefab;
    private Scheduler scheduler;

    [SerializeField]
    private GameObject scheduleHandlerPrefab;
    private ScheduleHandler scheduleHandler;

    [SerializeField]
    private GameObject cnvDecisionPrefab;
    private List<GameObject> createdUIContents;

    [SerializeField]
    private GameObject scheduleViewer;

    private GameObject _cnvMenu;
    private int selectedScheduleIndex;

    private GameObject cnvMenu
    {
        get
        {
            if (_cnvMenu == null)
            {
                _cnvMenu = GameObject.Find("Cnv_Menu");
            }
            return _cnvMenu;
        }
    }


    private void Awake()
    {
        createdUIContents = new List<GameObject>();
        currentScheduleIndex = 0;
        scheduler = Instantiate<GameObject>(schedulerPrefab).GetComponent<Scheduler>();
        scheduleHandler = Instantiate<GameObject>(scheduleHandlerPrefab).GetComponent<ScheduleHandler>();
    }

    public void AddSchedule(string scheduleName)
    {
        scheduler.Add(selectedScheduleIndex, scheduleName);
        selectedScheduleIndex++;
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
            DialogueManager.Instance.ReceiveCommandsFile("EmptySchedule");
        }
    }

    public static void PlusCurrentScheduleIndex()
    {
        if (currentScheduleIndex < TOTAL_SCHEDULE_NUM)
        {
            currentScheduleIndex++;
        }
    }

    public void CreateDecisionUI()
    {
        GameObject decision = Instantiate(cnvDecisionPrefab);
        createdUIContents.Add(decision);
    }

    public void DeleteDecisionUI()
    {
        foreach (GameObject uiContent in createdUIContents)
        {
            Destroy(uiContent);
        }
        createdUIContents.Clear();
    }

    public void HiddenMenuUI()
    {
        cnvMenu.SetActive(false);
    }

    public void ShowMenuUI()
    {
        cnvMenu.SetActive(true);
    }

    public void AddScheduleViewer()
    {

    }
}
