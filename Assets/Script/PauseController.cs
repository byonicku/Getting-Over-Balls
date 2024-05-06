using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool isPaused;

    void Start()
    {
        Time.timeScale = 1.0f;
        this.isPaused = false;
    }

    public void TogglePause()
    {
        this.isPaused = !isPaused;
        if (this.isPaused )
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public bool GetIsPaused()
    { return this.isPaused; }
}
