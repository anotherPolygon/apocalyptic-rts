using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class QuaziTimer : MonoBehaviour
{
    private string command; 

    private Dictionary<string, TimedAction> timedActions = new Dictionary<string, TimedAction>();
    private List<string> timedActionsKeys = new List<string>();
    // FORMER SOLUTION: //
    //private List<TimedAction> timedActions = new List<TimedAction>();
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       for (int i=0; i<timedActionsKeys.Count; i++)
        {
            command = timedActions[timedActionsKeys[i]].Tick();
            if (command == "Remove")
                RemoveTimedAction(timedActions[timedActionsKeys[i]].Label);
        }
        // ####### FORMER SOLUTION: #######  //
        // foreach(var item in timedActions)
        // {
        //     command = item.Value.Tick();
        //     if (command == "Remove")
        //         RemoveTimedAction(item.Value.Label);
        //     //Debug.Log(timedAction.counter);
        // }
        //// Debug.Log(counter);

    }

    public void RegisterTimedAction(string label, Action a, int frameRate, int allowedDeviation, bool playOnceAfterDelay=false)
    {
        TimedAction ta = new TimedAction(a, frameRate, allowedDeviation, label, playOnceAfterDelay);
        if (!playOnceAfterDelay)
            ta.Func(); // Strat with calling the function
        timedActions.Add(label, ta);
        timedActionsKeys.Add(label);
    }

    public void RemoveTimedAction(string label)
    {
        TimedAction a;
        if (timedActions.TryGetValue(label, out a))
        {
            timedActions.Remove(label);
            timedActionsKeys.Remove(label);
        }
    }
}

public class TimedAction
{
    public Action Func { get; set; }
    public int FrameRate { get; set; }
    public int AllowedDeviation { get; set; }
    public bool PlayOnceAfterDelay { get; set; }
    public string Label { get; set; }
    System.Random rnd = new System.Random();

    public TimedAction(Action func, int frameRate, int allowedDeviation, string label, bool playOnceAfterDelay =false)
    {
        Func = func;
        FrameRate = frameRate;
        AllowedDeviation = allowedDeviation;
        PlayOnceAfterDelay = playOnceAfterDelay;
        Label = label;
    }

    public int counter = 0;
    public int noise = 0;
    public int FrameRateNoise = 0;

    public string Tick()
    {
        counter = counter + 1;
        noise = rnd.Next(-AllowedDeviation, AllowedDeviation);
        FrameRateNoise = noise + FrameRate;
        if (counter >= FrameRateNoise)
        {
            counter = 0;
            Func();
            if (PlayOnceAfterDelay)
                return "Remove";// order to remove
            else
                return "Remain";
        }
        return "Remain";
    }
}
