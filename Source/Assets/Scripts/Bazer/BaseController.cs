using UnityEngine;
using UnityEngine.Events;

using System.Collections;
using System.Collections.Generic;

namespace Bazer
{
  public class BaseControllerComponent: MonoBehaviour
  {
    public UnityEvent onStart = new UnityEvent();
    public UnityEvent onUpdate = new UnityEvent();
    public UnityEvent onDestroy = new UnityEvent();

    public UnityEvent onIntervalUpdate = new UnityEvent();

    public UnityEvent onApplicationClose = new UnityEvent();
    public BoolEvent onApplicationPause = new BoolEvent();

    public void Start()
    {
      onStart.Invoke();
    }

    public void Update()
    {
      onUpdate.Invoke();
    }

    public void Destroy()
    {
      onDestroy.Invoke();
    }

    public void OnApplicationQuit()
    {
      onApplicationClose.Invoke();
    }

    public void OnApplicationPause(bool pause)
    {
      onApplicationPause.Invoke(pause);
    }
  }

  public class BaseController
  {
    public GameObject gameObject;
    public BaseControllerComponent gameComponent;

    private CoroutineObject intervalUpdateRoutine;

    private float intervalUpdateTime;

    public BaseController() { }

    public void AddToScene()
    {
      gameObject = new GameObject(this.ToString());

      gameComponent = gameObject.AddComponent<BaseControllerComponent>();
    }

    public void StartIntervalUpdate(float timeInterval = 0.1f)
    {
      intervalUpdateTime = timeInterval;

      intervalUpdateRoutine = new CoroutineObject(gameComponent, OnUpdate);

      intervalUpdateRoutine.Start();
    }

    public void StopIntervalUpdate()
    {
      intervalUpdateRoutine.Stop();
    }

    private IEnumerator OnUpdate()
    {
      while (true) {
        yield return new WaitForSeconds(intervalUpdateTime);

        gameComponent.onIntervalUpdate.Invoke();
      }
    }
  }
}