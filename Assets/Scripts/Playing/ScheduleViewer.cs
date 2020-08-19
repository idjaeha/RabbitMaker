using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleViewer : MonoBehaviour
{
    private const float IMAGE_GAP = 190f;
    [SerializeField]
    private GameObject scheduleImagePrefab;
    private GameObject[] scheduleImages;
    private GameObject content;

    private int selectedIndex;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        content = GameObject.FindGameObjectWithTag("ScheduleViewerContent");
        scheduleImages = new GameObject[PlayingManager.TOTAL_SCHEDULE_NUM];
        for (int index = 0; index < PlayingManager.TOTAL_SCHEDULE_NUM; index++)
        {
            GameObject newScheduleImage = Instantiate<GameObject>(scheduleImagePrefab, content.transform);
            newScheduleImage.GetComponent<Toggle>().group = content.GetComponent<ToggleGroup>();
            newScheduleImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(IMAGE_GAP * index, 0);
            newScheduleImage.GetComponentInChildren<Text>().text = $"{index}일차";
            scheduleImages[index] = newScheduleImage;
        }
    }

    public void AddSchedule(GameObject scheduleImage)
    {
    }

    // 스크롤 뷰는 scheduler안에 있는 배열을 통해서 초기화된다. ( 서로 다른 값이 되는 경우를 방지하기 위해서 )
    // 어떤 값이 선택되었는지는 어떻게 알 것인가?
    // 지금은 스크롤 뷰가 콘텐츠를 벗어나는데 이것을 어떻게 해결할 것인가?
    // 선택된 객체를 알기 위해서 Image가 아닌 새로운 방법을 채택해야한다.
}
