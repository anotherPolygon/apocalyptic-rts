using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class QuaziTimer : MonoBehaviour
{
    private int counter;
    private int maxCounter; // An arbitrrary number that resets the ounter
    private string command; 

    //private List<TimedAction> timedActions = new List<TimedAction>();
    private Dictionary<string, TimedAction> timedActions = new Dictionary<string, TimedAction>();

    // Start is called before the first frame update
    void Start()
    {
        //RegisterTimedAction(Shoot, 50);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        foreach(var item in timedActions)
        {
            command = item.Value.Tick();
            if (command == "Remove")
                RemoveTimedAction(item.Value.Label);
            //Debug.Log(timedAction.counter);
        }
       // Debug.Log(counter);

    }

    public void RegisterTimedAction(string label, Action a, int frameRate, int allowedDeviation, bool playOnceAfterDelay=false)
    {
        TimedAction ta = new TimedAction(a, frameRate, allowedDeviation, label, playOnceAfterDelay);
        if (!playOnceAfterDelay)
            ta.Func(); // Strat with calling the function
        timedActions.Add(label, ta);
    }

    public void RemoveTimedAction(string label)
    {
        TimedAction a;
        if (timedActions.TryGetValue(label, out a))
            timedActions.Remove(label);
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
    int noise = 0;
    int FrameRateNoise = 0;

    public string Tick()
    {
        counter = counter + 1;
        noise = rnd.Next(-AllowedDeviation, AllowedDeviation);
        FrameRateNoise = noise + FrameRate;
        if (counter >= FrameRateNoise)
        {
            counter = 0;
            Func();
        }
        if (PlayOnceAfterDelay)
        {
            return "Remove";// order to remove
        }
        else
        {
            return "Remain";
        }
    }
}
