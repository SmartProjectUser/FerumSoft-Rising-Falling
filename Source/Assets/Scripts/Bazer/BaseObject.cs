using UnityEngine;
using UnityEngine.Events;

using System;
using System.Collections;
using System.Collections.Generic;

using Animatix;

namespace Bazer
{
  public struct BaseObjectParams
  {
    public string name;
    public string path;
    public string file;
    public Type component;
    public Nullable<Vector3> position;
    public Nullable<Quaternion> rotation;
    public Nullable<Vector3> scale;
    public GameObject parent;
    public bool projection;
  }

  public class BaseObject
  {
    public GameObject gameObject;
    public BaseComponent gameComponent;

    public BaseObject() { }

    public BaseObject(BaseObjectParams objectParams)
    {
      CreateObject(objectParams);
    }

    public BaseObject CreateObject(BaseObjectParams objectParams)
    {
      if (!string.IsNullOrEmpty(objectParams.path) && !string.IsNullOrEmpty(objectParams.file))
      {
        try
        {
          gameObject = GameObject.Instantiate(Resources.Load<GameObject>(objectParams.path + objectParams.file));

          if (objectParams.position.HasValue)
          {
            gameObject.transform.position = objectParams.position.Value;
          }

          if (objectParams.rotation.HasValue)
          {
            gameObject.transform.rotation = objectParams.rotation.Value;
          }

          if (objectParams.scale.HasValue)
          {
            gameObject.transform.localScale = objectParams.scale.Value;
          }

          gameObject.name = objectParams.file;
        }
        catch (NullReferenceException e)
        {
          throw new Exception(e.ToString());
        }
      }
      else
      {
        gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (objectParams.position.HasValue)
        {
          gameObject.transform.position = objectParams.position.Value;
        }

        if (!string.IsNullOrEmpty(objectParams.name))
        {
          gameObject.name = objectParams.name;
        }
      }

      if (objectParams.parent)
      {
        gameObject.transform.SetParent(objectParams.parent.transform);

        if (objectParams.position.HasValue)
        {
          if (objectParams.projection)
          {
            gameObject.transform.localPosition = objectParams.parent.transform.InverseTransformPoint(objectParams.position.Value);
          }
          else
          {
            gameObject.transform.localPosition = objectParams.position.Value;
          }
        }
      }

      if (objectParams.component != null)
      {
        gameComponent = gameObject.AddComponent(objectParams.component) as BaseComponent;
      }
      else
      {
        gameComponent = gameObject.AddComponent(typeof(BaseComponent)) as BaseComponent;
      }

      return this;
    }

    public BaseObject AssignObject(BaseObjectParams objectParams)
    {
      if (!string.IsNullOrEmpty(objectParams.name))
      {
        gameObject = GameObject.Find(objectParams.name);
      }

      if (objectParams.component != null)
      {
        gameComponent = gameObject.AddComponent(objectParams.component) as BaseComponent;
      }
      else
      {
        gameComponent = gameObject.AddComponent(typeof(BaseComponent)) as BaseComponent;
      }

      return this;
    }

    public void ApplyParams(BaseObjectParams objectParams)
    {

    }

    public void DestroyObject()
    {
      GameObject.Destroy(gameObject);
    }
  }
}