using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RabbitCharacter : MonoBehaviour
{
    private InputRabbitName inputrabbitname;

    //소지금, 이름, 착용 장비, Status 등 캐릭터와 관련된 정보 저장, 제공

    private int cash;
    public string characterName;
    private int equipment;       //장비를 번호로 저장한다 
    
    public float rabbitStress;   //스트레스

    public float rabbitStamina;  //체력

    public float rabbitIntelligence;  //지력

    public float rabbitPatience;   //끈기

    public float rabbitPhysical;  //근력

    private void Awake()
    {
        inputrabbitname = GetComponent<InputRabbitName>();
    }
    private void Update()
    {
        //Debug.Log(inputrabbitname.rabbitname);
    }

}
