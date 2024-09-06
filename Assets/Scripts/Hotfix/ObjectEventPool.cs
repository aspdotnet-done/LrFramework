using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeEvent
{
    protected float delayTime;
    protected bool isStart;
    protected System.Action action;

    public virtual void Init(float delayTime)
    {
        this.delayTime = delayTime;
    }

    public virtual void Start(System.Action action)
    {
        isStart     = true;
        this.action = action;
    }

    public virtual void Stop()
    {
        isStart = false;
        action  = null;
    }

    public virtual void Update()
    {
        if (isStart)
        {
            delayTime -= Time.deltaTime;
            if (delayTime <= 0)
            {
                try
                {
                    action?.Invoke();
                }
                catch(System.Exception ex)
                { 
                    Debug.LogError(ex);
                }
                Stop();
            }
        }
    }
}

public class PoolEvent : TimeEvent
{ 
    public GameObject go;

    public virtual void Init(float delayTime, GameObject go)
    {
        base.Init(delayTime);
        this.go = go;
        Start(Finish);
    }

    void Finish()
    {
        PoolMgr.Delete(go);
        go = null;
    }
}


public class ObjectEventPool : MonoBehaviour
{
    static List<PoolEvent> poolEvents = new List<PoolEvent>();
    static List<PoolEvent> poolEventWaits = new List<PoolEvent>();

    static object lockObj = new object();


    public static void Release(TimeEvent @event)  
    { 
        if (@event is PoolEvent item)
        {
            if (poolEvents.Contains(item))
            { 
                poolEvents.Remove(item);
            }
            poolEventWaits.Add(item);
        }
    }



    public static void CreatePool(float delayTime, GameObject go) 
    {
        lock (lockObj)
        {
            PoolEvent tempItem = null;
            if (poolEventWaits.Count > 0)
            {
                tempItem = poolEventWaits[0];
                poolEventWaits.Remove(tempItem);
            }
            else
            {
                tempItem = new PoolEvent();
            }
            tempItem.Init(delayTime, go);
            poolEvents.Add(tempItem);
        }
    }

    void Update()
    {
        if (poolEvents.Count > 0)
        {
           for (int i = poolEvents.Count - 1; i >= 0 ; i--)
           {
                poolEvents[i].Update();
            }
        }
    }
}
