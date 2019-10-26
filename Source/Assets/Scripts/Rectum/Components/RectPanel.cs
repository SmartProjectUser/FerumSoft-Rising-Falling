using UnityEngine;
using UnityEngine.UI;

using System.Collections;

namespace Rectum
{
  public class RectPanel : RectComponent
  {
    public RectImage rectImage;

    public Sprite sprite
    {
      get => rectImage.sprite;
      set {
        if (rectImage == null)
        {
          AddSpriteComponent();
        }

        rectImage.sprite = value;
        rectImage.size = size;
      }
    }

    public RectPanel()
    {
      onInit.AddListener(OnInit);
    }

    private void OnInit()
    {
    }

    private void AddSpriteComponent()
    {
      rectImage = AddChild<RectImage>("Sprite");
      rectImage.size = size;
      rectImage.position = new Vector2(0, 0);
    }
  }
}