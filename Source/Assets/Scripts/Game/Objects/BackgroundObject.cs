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
  public class BackgroundObject : BaseObject
  {
    public BackgroundObject() { }

    public void AssignBackgroundObject()
    {
      BaseObjectParams objectParams = new BaseObjectParams
      {
        name = GameController.backgroundObjectName,
        component = typeof(BackgroundObjectComponent)
      };

      AssignObject(objectParams);

      gameComponent.objectParams.Add("isDragged", false);
      gameComponent.objectParams.Add("mouseDownPosition", new Vector3());

      gameComponent.onMouseDown.AddListener(() => {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);

        gameComponent.objectParams["mouseDownPosition"] = castPoint.origin;
      });

      gameComponent.onMouseDrag.AddListener(() => {
        gameComponent.objectParams["isDragged"] = true;
      });

      gameComponent.onMouseUp.AddListener(() => {
        Vector3 mouseDownPosition = (Vector3)gameComponent.objectParams["mouseDownPosition"];

        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);

        Vector3 currentMousePosition = castPoint.origin;

        Vector3 dragVectorDirection = (currentMousePosition - mouseDownPosition).normalized;

        var draggedDirection = DragHelper.GetDragDirection(dragVectorDirection);

        GameController.SwipeObject(draggedDirection);

        gameComponent.objectParams["isDragged"] = false;
      });
    }
  }
}
