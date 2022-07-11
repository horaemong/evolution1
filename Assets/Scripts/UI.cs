using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject prefab;
    public static int generation = 1;
    public static int timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        Timer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Timer()
    {
        if (timer == 0)
        {
            timer +=1;
            Invoke("Timer", 10);
        }
        else
        {
            generation +=1;
            Debug.Log("제네"+generation);
            Invoke("Timer", 7);
        }

    }


}
