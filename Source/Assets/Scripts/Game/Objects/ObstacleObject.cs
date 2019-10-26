using UnityEngine;

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
  public class ObstacleObject : BaseObject
  {
    public int respawnIndex;
    public int typeIndex;

    public ObstacleObject(int respawnIndex, int typeIndex)
    {
      this.respawnIndex = respawnIndex;
      this.typeIndex = typeIndex;
    }

    public void CreateObstacleObject()
    {
      var objectPosition = GameController.inst.respawnObjectPlaces[respawnIndex].gameObject.transform.position;

      BaseObjectParams objectParams = new BaseObjectParams
      {
        path = Const.MODELS_PATH + "Obstacle_objects/",
        file = "Obstacle_" + typeIndex,
        name = "Obstacle_" + typeIndex,
        position = objectPosition,
        rotation = Quaternion.Euler(0f, 0f, 0f),
        scale = new Vector3(1f, 1f, 1f),
        component = typeof(ObstacleObjectComponent)
      };

      CreateObject(objectParams);

      gameComponent.onUpdate.AddListener(OnComponentUpdate);
      gameComponent.onStart.AddListener(OnStart);
    }

    public void OnStart()
    {
      var spriteRendererComponent = gameComponent.GetComponent<SpriteRenderer>();
      spriteRendererComponent.sortingOrder = 1;
    }

    public void OnComponentUpdate()
    {
      gameObject.transform.position += Vector3.up * Time.deltaTime * GameController.fallingSpeed;
    }
  }
}
