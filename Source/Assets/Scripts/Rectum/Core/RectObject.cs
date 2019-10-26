using UnityEngine;
using UnityEngine.Events;

namespace Rectum
{
  public class RectObject: MonoBehaviour
  {
    public UnityEvent onInit = new UnityEvent();
    public UnityEvent onCreate = new UnityEvent();
    public UnityEvent onUpdate = new UnityEvent();
    public UnityEvent onDestroy = new UnityEvent();

    public bool active
    {
      get => gameObject.activeSelf;
      set => gameObject.SetActive(value);
    }

    private void Awake()
    {
      onInit.Invoke();
    }

    private void Start()
    {
      onCreate.Invoke();
    }

    private void Update()
    {
      onUpdate.Invoke();
    }

    private void OnDestroy()
    {
      onDestroy.Invoke();
    }
  }
}