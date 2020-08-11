using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [Header("스트레스")]
    public float rabbitStress;
   
    [Header("체력")]
    public float rabbitStamina;
    
    [Header("지력")]
    public float rabbitIntelligence;
   
    [Header("끈기")]
    public float rabbitPatience;
    
    [Header("근력")]
    public float rabbitPhysical;


    static Stat instance = null;
    public static Stat Instance {
        get {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }

}
