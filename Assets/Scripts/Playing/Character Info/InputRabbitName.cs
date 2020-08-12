using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputRabbitName : MonoBehaviour
{
    public InputField inputName;
    public string rabbitname;

    public void Input()
    {
        rabbitname = inputName.text;

        //Debug.Log(rabbitname);
    }
}
