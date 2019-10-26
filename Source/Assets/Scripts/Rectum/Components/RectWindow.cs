using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using System;
using System.Collections;

using Project;

namespace Rectum
{
  public class RectWindow : RectComponent
  {
    public string windowColor = "#000000";
    public float windowAlpha = 0.5f;

    private RectImage windowBackground;

    public Sprite sprite
    {
      get => windowBackground.sprite;
      set => windowBackground.sprite = value;
    }

    public RectWindow()
    {
      onCreate.AddListener(OnCreate);
      onSizeChange.AddListener(OnSizeChange);
    }

    private void OnCreate()
    {
    }

    private void OnSizeChange()
    {
      AddWindowBackground();
    }

    private void AddWindowBackground()
    {
      windowBackground = AddChild<RectImage>("WindowBackground");
      windowBackground.size = size;
      windowBackground.position = new Vector2(0, 0);

      if (windowBackground.sprite == null)
      {
        windowBackground.SetColor(windowColor, windowAlpha);
      }
    }
  }
}