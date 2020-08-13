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

    [SerializeField]
    private GameObject cnvDecisionPrefab;
    private List<GameObject> createdUIContents;

    private GameObject _cnvMenu;
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
}
