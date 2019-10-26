using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections;
using System.Collections.Generic;

namespace Rectum
{
  public class RectScroll: RectComponent
  {
    public RectContainer contentContainer;

    private ScrollRect scrollRectComponent;

    public RectScroll()
    {
      onInit.AddListener(OnInit);
      onSizeChange.AddListener(OnSizeChange);
    }

    private void OnInit()
    {
      RectMask2D mask = gameObject.AddComponent<RectMask2D>();

      AddScrollRect();
    }

    private void OnSizeChange()
    {
      AddContentContainer();
    }

    private void AddScrollRect()
    {
      scrollRectComponent = gameObject.AddComponent<ScrollRect>();
      scrollRectComponent.horizontal = false;
      scrollRectComponent.vertical = true;
      scrollRectComponent.movementType = ScrollRect.MovementType.Elastic;
      scrollRectComponent.elasticity = 0.1f;
      scrollRectComponent.inertia = true;
      scrollRectComponent.decelerationRate = 0.135f;
      scrollRectComponent.scrollSensitivity = 24;
    }

    private void  AddContentContainer()
    {
      contentContainer = AddChild<RectContainer>();
			contentContainer.size = size;
			contentContainer.position = new Vector2(0, 0);

			scrollRectComponent.content = contentContainer.rectTransform;
    }
  }
}