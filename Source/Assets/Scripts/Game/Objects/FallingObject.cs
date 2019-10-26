using UnityEngine;
using UnityEngine.EventSystems;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Bazer;
using Animatix;
using UnityEngine.Events;

using Rectum;

namespace Project
{
  public class FallingObject : BaseObject
  {
    public FallingObject() { }

    public void CreateFallingObject()
    {
      var unitPosition = GameController.inst.fallingObjectPlace.gameObject.transform.position;

      BaseObjectParams objectParams = new BaseObjectParams
      {
        path = Const.MODELS_PATH + "Falling_object/",
        file = "Falling_object",
        name = "Falling_object",
        position = unitPosition,
        rotation = Quaternion.Euler(0f, 0f, 0f),
        scale = new Vector3(1f, 1f, 1f),
        component = typeof(FallingObjectComponent)
      };

      CreateObject(objectParams);

      gameComponent.onStart.AddListener(OnStart);

      gameComponent.onCollision2DEnter.AddListener((collision) =>
      {
        GameController.ContactObject(collision);
      });
    }

    public void OnStart()
    {
      var spriteRendererComponent = gameComponent.GetComponent<SpriteRenderer>();
      spriteRendererComponent.sortingOrder = 1;
    }
  }
}
