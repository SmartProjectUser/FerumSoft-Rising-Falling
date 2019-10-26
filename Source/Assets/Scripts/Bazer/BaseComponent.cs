using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

namespace Bazer
{
  public class BaseComponent : MonoBehaviour
  {
    public Hashtable objectParams = new Hashtable();

    public UnityEvent onStart = new UnityEvent();
    public UnityEvent onUpdate = new UnityEvent();
    public UnityEvent onDestroy = new UnityEvent();

    public UnityEvent onClick = new UnityEvent();

    public UnityEvent onMouseDown = new UnityEvent();
    public UnityEvent onMouseDrag = new UnityEvent();
    public UnityEvent onMouseEnter = new UnityEvent();
    public UnityEvent onMouseExit = new UnityEvent();
    public UnityEvent onMouseOver = new UnityEvent();
    public UnityEvent onMouseUp = new UnityEvent();

    public CollisionEvent onCollisionEnter = new CollisionEvent();
    public Collision2DEvent onCollision2DEnter = new Collision2DEvent();
    public TriggerEvent onTriggerEnter = new TriggerEvent();
    public Trigger2DEvent onTrigger2DEnter = new Trigger2DEvent();
    public DragEndEvent onDragEnd = new DragEndEvent();

    public void Start()
    {
      onStart.Invoke();
    }

    public void Update()
    {
      onUpdate.Invoke();

      ClickHandle();
    }

    public void Destroy()
    {
      onDestroy.Invoke();
    }

    public Coroutine RunCoroutine(IEnumerator coroutine)
    {
      return StartCoroutine(coroutine);
    }

    public void EndCoroutine(Coroutine coroutine)
    {
      StopCoroutine(coroutine);
    }

    private void OnDragEnd(PointerEventData eventData)
    {
      Debug.Log("HELLO");
      onDragEnd.Invoke(eventData);
    }

    private void OnMouseDown()
    {
      onMouseDown.Invoke();
    }

    private void OnMouseDrag()
    {
      onMouseDrag.Invoke();
    }

    private void OnMouseEnter()
    {
      onMouseEnter.Invoke();
    }

    private void OnMouseExit()
    {
      onMouseExit.Invoke();
    }

    private void OnMouseOver()
    {
      onMouseOver.Invoke();
    }

    private void OnMouseUp()
    {
      onMouseUp.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
      onCollisionEnter.Invoke(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      onCollision2DEnter.Invoke(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
      onTriggerEnter.Invoke(other);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      onTrigger2DEnter.Invoke(other);
    }

    private void ClickHandle()
    {
      RaycastHit hit = new RaycastHit();

      for (int i = 0; i < Input.touchCount; ++i)
      {
        if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
        {
          Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

          if (Physics.Raycast(ray, out hit))
          {
            onClick.Invoke();
          }
        }
      }

      if (Input.GetMouseButtonDown(0))
      {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
          if (hit.collider.gameObject == gameObject)
          {
            onClick.Invoke();
          }
        }
      }
    }
  }
}