using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoreLoopGame : MonoBehaviour
{
    public Action OnStart;
    public Action OnStartWave;
    public Action OnEndWave;
    public Action OnEndGame;
    public Action OnWin;
    public Action OnLose;
    public Action OnRestart;
    public Action OnAddPerk;


    public static CoreLoopGame Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    public void EndWave()
    {
        OnEndWave.Invoke();
    }
    public void StartWave()
    {
        OnStartWave.Invoke();
    }
    public void GameCompletedWin()
    {
        OnWin.Invoke();
    }

    public void GameCompletedLose()
    {
        OnLose.Invoke();
    }

    public void AddNewPerk()
    {
        OnAddPerk.Invoke();
    }
}
