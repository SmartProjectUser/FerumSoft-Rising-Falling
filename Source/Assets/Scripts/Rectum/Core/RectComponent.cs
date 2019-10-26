using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using System;
using System.Collections;

namespace Rectum
{
  public class RectComponent : RectObject
  {
    public RectTransform rectTransform;

    public UnityEvent onSizeChange = new UnityEvent();
    public UnityEvent onPositionChange = new UnityEvent();

    public Vector2 size
    {
      get {
        float currWidth = rectTransform.GetWidth() / RectUI.scaleFactor;
        float currHeight = rectTransform.GetHeight() / RectUI.scaleFactor;

        return new Vector2(currWidth, currHeight);
      }
      set
      {
        float newWidth = value.x * RectUI.scaleFactor;
        float newHeight = value.y * RectUI.scaleFactor;

        rectTransform.SetWidth(newWidth);
        rectTransform.SetHeight(newHeight);

        onSizeChange.Invoke();
      }
    }

    public Vector2 position
    {
      get {
        float currPositionX = rectTransform.localPosition.x / RectUI.scaleFactor;
        float currPositionY = rectTransform.localPosition.y / RectUI.scaleFactor * -1;

        return new Vector2(currPositionX, currPositionY);
      }
      set {
        float newPositionX = value.x * RectUI.scaleFactor;
        float newPositionY =  value.y * RectUI.scaleFactor * -1;

        rectTransform.SetLeftTopPosition(new Vector2(newPositionX, newPositionY));

        onPositionChange.Invoke();
      }
    }

    public Vector2 positionOnCanvas
    {
      get
      {
        GameObject canvasObject = RectCanvas.GetCanvasObject();

        Vector3 component3DPosition = canvasObject.transform.TransformPoint(rectTransform.position.x, rectTransform.position.y, 0);
        Vector2 component2DPosition = RеctTransformExtension.PosOnScreen(new Vector2(component3DPosition.x, component3DPosition.y - Screen.height));

        return component2DPosition;
      }
      set {
        rectTransform.SetLeftTopPosition(RеctTransformExtension.PosOnScreen(value));

        onPositionChange.Invoke();
      }
    }

    public RectComponent()
    {
      onInit.AddListener(OnInit);
    }

    private void OnInit()
    {
      AddRectTransform();
    }

    public T AddChild<T>(string name = "") where T: RectComponent
    {
      GameObject childObject = new GameObject(name == string.Empty ? typeof(T).ToString() : name);

      childObject.transform.SetParent(gameObject.transform);

      var childComponent = childObject.AddComponent(typeof(T));

      return (T)childComponent;
    }

    public T AddChildAt<T>(int index, string name = "") where T: RectComponent
    {
      var childComponent = AddChild<T>(name);

      childComponent.transform.SetSiblingIndex(index);

      return childComponent;
    }

    public void RemoveChild(RectComponent rectComponent)
    {
      GameObject.Destroy(rectComponent.gameObject);
    }

    private void AddRectTransform()
    {
      rectTransform = gameObject.AddComponent<RectTransform>();
      rectTransform.SetLeftTopPivot();
    }

    public void SetFirst()
    {
      gameObject.transform.SetAsFirstSibling();
    }

    public void SetLast()
    {
      gameObject.transform.SetAsLastSibling();
    }

    public void SetDephth(int index)
    {
      gameObject.transform.SetSiblingIndex(index);
    }
  }
}