using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.MoveScene("StartScene");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
