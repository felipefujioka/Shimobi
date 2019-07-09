using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using Game.Field;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;

public class QAToolController : MonoBehaviour
{
    public InputHandler InputHandler;
    public Text StateText;
    public Text GestureCounterText;

    private int counter;
    
    private int gestureCounter
    {
        get { return counter; }
        set
        {
            counter = value;
            GestureCounterText.text = counter.ToString();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (!Debug.isDebugBuild)
        {
            Destroy(gameObject);
        }

        InputHandler.StateChanged += OnStateChange;
        InputHandler.OnFlick += OnFlick;
        
        StateText.text = InputHandler.State.ToString();
    }

    public void ResetCounter()
    {
        gestureCounter = 0;
    }
    
    private void OnFlick(object sender, Direction e)
    {
        gestureCounter++;
    }

    private void OnStateChange(object sender, GestureStateChangeEventArgs e)
    {
        StateText.text = e.State.ToString();
    }
}
