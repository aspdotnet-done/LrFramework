using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoFSM<T>
{
    protected T main;
    protected List<MonoState<T>> mainStates;
    public MonoState<T>  currentState;

    public MonoFSM(T main)
    {
        this.main = main;
        mainStates = new List<MonoState<T>>();
    }

    public void AddState(MonoState<T> state)
    {
        mainStates.Add(state);
    }
    
    public void Start()
    {
        currentState = mainStates[0];
        currentState . OnEnter();
    }

    public void Update()
    {
        currentState?.OnUpdate();
    }

    private bool ChangeState()
    {
        var id = mainStates.FindIndex((s) => s == currentState);
        if (++id < mainStates.Count)
        {
            currentState = mainStates[id];
            return true;
        }

        currentState = null;
        return false;
    }

    public void Next()
    {
        currentState?.OnExit();
        if (ChangeState())
        {
            currentState?.OnEnter();
        }
    }
}



public class MonoState<T>
{
    protected T main;
    protected MonoFSM<T> fsm;

    public virtual void Init(MonoFSM<T> fsm, T main)
    {
        this.fsm  = fsm;
        this.main = main;
    }

    public virtual void OnEnter() { }
    public virtual void OnExit()  { }
    public virtual void OnUpdate() { }
}