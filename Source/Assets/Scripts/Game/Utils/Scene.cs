using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

namespace Project
{
  public class Scene
  {
    public static GameObject CreateObject(
      string path, string file, Vector3 position, Quaternion rotation, GameObject parent = null, bool proj = false
    )
    {
      GameObject gameObject = null;

      try
      {
        gameObject = GameObject.Instantiate(Resources.Load<GameObject>(path + file), position, rotation) as GameObject;
        gameObject.name = file;

        if (parent)
        {
          gameObject.transform.SetParent(parent.transform);

          if (proj)
          {
            gameObject.transform.localPosition = parent.transform.InverseTransformPoint(position);
          }
          else
          {
            gameObject.transform.localPosition = position;
          }
        }
        else
        {
          gameObject.transform.position = position;
        }
      }
      catch (NullReferenceException e)
      {
        Funcs.Throw("Prefab not found");
      }

      return gameObject;
    }
  }
}