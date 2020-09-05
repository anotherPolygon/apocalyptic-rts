using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    private string command;

    private Dictionary<string, TimedActiona> timedActions = new Dictionary<string, TimedActiona>();
    private List<string> timedActionsKeys = new List<string>();

    public float t;
    // FORMER SOLUTION: //
    //private List<TimedAction> timedActions = new List<TimedAction>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < timedActionsKeys.Count; i++)
        {
            t = Time.time;
            command = timedActions[timedActionsKeys[i]].Tick(t);
            if (command == "Remove")
                RemoveTimedAction(timedActions[timedActionsKeys[i]].Label);
        }

    }
    public void After(float delayTime, Action a, string label, float allowedDeviation = 0)
    {
        bool playOnce = true;
        RegisterTimedAction(label, a, delayTime, allowedDeviation, playOnce);
    }

    public void RegisterTimedAction(string label, Action a, float timeRate, float allowedDeviation, bool playOnceAfterDelay = false)
    {
        TimedActiona ta = new TimedActiona(t, a, timeRate, allowedDeviation, label, playOnceAfterDelay);
        if(playOnceAfterDelay==false)
            ta.Func(); // Strat with calling the function
            
        timedActions.Add(label, ta);
        timedActionsKeys.Add(label);
    }

    public void RemoveTimedAction(string label)
    {
        TimedActiona a;
        if (timedActions.TryGetValue(label, out a))
        {
            timedActions.Remove(label);
            timedActionsKeys.Remove(label);
        }
    }
}

public class TimedActiona
{
    public float ReffTime { get; set; }
    public Action Func { get; set; }
    public float TimeRate { get; set; }
    public float AllowedDeviation { get; set; }
    public bool PlayOnceAfterDelay { get; set; }
    public string Label { get; set; }
    System.Random rnd = new System.Random();

    public TimedActiona(float reffTime, Action func, float timeRate, float allowedDeviation, string label, bool playOnceAfterDelay = false)
    {
        Func = func;
        TimeRate = timeRate;
        AllowedDeviation = allowedDeviation;
        PlayOnceAfterDelay = playOnceAfterDelay;
        Label = label;
        ReffTime = reffTime;
    }

    public float noise = 0f;
    public float TimeRateNoise = 0;
   

    public string Tick(float timeNow)
    {
       
        //noise = rnd.Next(-AllowedDeviation, AllowedDeviation); // works for int only
        noise = 0f;
        TimeRateNoise = noise + TimeRate;
        if (timeNow >= TimeRateNoise + ReffTime)
        {
            ReffTime = timeNow;
            Func();
            if (PlayOnceAfterDelay)
                return "Remove";// order to remove
            else
                return "Remain";
        }
        return "Remain";
    }
}
