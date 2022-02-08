using System;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private Action action;
    private string name;
    private float _time;
    private GameObject gameObject;
    private bool _isDestroyed = false;
    private static GameObject _initGameObj;
    private static List<Timer> _activeTimers;
    private class MonoBehaviourHook : MonoBehaviour
    {
        public Action OnUpdate;
        public void Update()
        {
            OnUpdate?.Invoke();
        }

    }
    private Timer(Action action, float time, string name, GameObject gameObject)
    {
        this.action = action;
        this._time = time;
        this.name = name;
        this.gameObject = gameObject;
    }
    private static void InitIfNeeded()
    {
        if (_initGameObj == null)
        {
            _initGameObj = new GameObject("Timer_InitGameObj");
            _activeTimers = new List<Timer>();
        }
    }
    public static Timer Create(Action action, float time, string name = null)
    {
        InitIfNeeded();
        GameObject gameObject = new GameObject("Timer", typeof(MonoBehaviourHook));
        Timer timer = new Timer(action, time, name, gameObject);
        gameObject.GetComponent<MonoBehaviourHook>().OnUpdate = timer.Update;
        _activeTimers.Add(timer);
        return timer;
    }
    private static void RemoveTimer(Timer timer)
    {
        InitIfNeeded();
        _activeTimers.Remove(timer);
    }
    public static void Stop(string name)
    {
        for (int i = 0; i < _activeTimers.Count; i++)
        {
            if (_activeTimers[i].name == name)
            {
                _activeTimers[i].DestroySelf();
                i--;
            }
        }
    }
    public void Update()
    {
        if (!_isDestroyed)
        {
            _time -= Time.deltaTime;
            if (_time < 0)
            {
                action();
                DestroySelf();
            }
        }
    }
    public void DestroySelf()
    {
        _isDestroyed = true;
        RemoveTimer(this);
        UnityEngine.Object.Destroy(gameObject);
    }
}
