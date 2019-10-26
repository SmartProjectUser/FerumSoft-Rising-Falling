using System;
using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rectum
{
  public class RectContainer : RectComponent
  {
    public List<RectElement> elements = new List<RectElement>();

    public RectContainer() {
      onInit.AddListener(OnInit);
    }

    private void OnInit()
    {
      AddVerticalLayout();
      AddSizeFitter();
    }

    public T AddElement<T>() where T: RectElement
    {
      RectElement rectElement = AddChild<T>();

      elements.Add(rectElement);

      return (T)rectElement;
    }

    public List<RectElement> GetLayoutElements()
    {
      return elements;
    }

    public void AddVerticalLayout()
    {
      VerticalLayoutGroup verticalLayoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
      verticalLayoutGroup.spacing = 2;
      verticalLayoutGroup.childAlignment = TextAnchor.UpperCenter;
      verticalLayoutGroup.childForceExpandHeight = false;
      verticalLayoutGroup.childForceExpandWidth = false;
    }

    public void AddHorizontalLayout()
    {

    }

    public void AddGridLayout()
    {

    }

    public void AddSizeFitter()
    {
      ContentSizeFitter contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();
      contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
      contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }
  }
}