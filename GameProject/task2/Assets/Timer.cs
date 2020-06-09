using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public Text timerT;
	private float sTime;
    void Start()
    {
        sTime = Time.time;
    }
    void Update()
    {
        float t = Time.time - sTime;
	
	string hours = ((int) t/3600).ToString();
	string min = ((int) t/60).ToString();
	string sec = (t % 60).ToString("f0");
	timerT.text = hours + ":" + min + ":" + sec;
    }
}
