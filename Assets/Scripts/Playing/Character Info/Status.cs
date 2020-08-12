using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    //정해진 스탯을 더하기 , 빼기, 설정 ,스탯 한번에 설정, 스탯 반환

    private RabbitCharacter rabbitCharacter;

    private void Awake()
    {
        rabbitCharacter = GetComponent<RabbitCharacter>();
    }
    private void Start()
    {
       // Debug.Log(rabbitCharacter.rabbitStress);
    }


}
