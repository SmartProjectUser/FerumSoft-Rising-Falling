using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using System.Collections;

namespace Rectum
{
  public class RectElement : RectComponent
  {
    public UnityEvent onElementUpdate = new UnityEvent();

    private object _elementData;
    public object elementData
    {
      get => _elementData;
      set
      {
        _elementData = value;

        onElementUpdate.Invoke();
      }
    }

    private LayoutElement layoutElementComponent;

    public RectElement()
    {
      onInit.AddListener(OnInit);
      onSizeChange.AddListener(OnSizeChange);
    }

    private void OnInit()
    {
      layoutElementComponent = gameObject.AddComponent<LayoutElement>();
    }

    private void OnSizeChange()
    {
      SetPrefferedSize(size * RectUI.scaleFactor);
    }

    public void SetPrefferedSize(Vector2 size)
    {
      layoutElementComponent.preferredWidth = size.x;
      layoutElementComponent.preferredHeight = size.y;
    }

    public void SetMinimalSize(Vector2 size)
    {
      layoutElementComponent.minWidth = size.x;
      layoutElementComponent.minHeight = size.y;
    }

    public void SetFlexibleSize(Vector2 size)
    {
      layoutElementComponent.flexibleWidth = size.x;
      layoutElementComponent.flexibleHeight = size.y;
    }
  }
}