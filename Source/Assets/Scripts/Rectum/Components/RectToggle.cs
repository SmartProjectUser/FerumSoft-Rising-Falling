using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using System.Collections;

namespace Rectum
{
  public class RectToggle : RectComponent
  {
    private Toggle toggleComponent;
    private Image imageComponent;

    public string bgColor = "#111111";
    public float bgAlpha = 0.5f;

    public ToggleGroup toggleGroup
    {
      get => toggleComponent.group;
      set => toggleComponent.group = value;
    }

    public RectBoolEvent onToggleChange = new RectBoolEvent();

    public RectToggle()
    {
      onInit.AddListener(OnInit);
    }

    private void OnInit()
    {
      AddImageComponent();
      AddToggleComponent();
    }

    private void AddImageComponent()
    {
      imageComponent = gameObject.AddComponent<Image>();
      imageComponent.color = RectUtils.HexToColor(bgColor, bgAlpha);
    }

    private void AddToggleComponent()
    {
      toggleComponent = gameObject.AddComponent<Toggle>();
      toggleComponent.targetGraphic = imageComponent;
      toggleComponent.onValueChanged.AddListener((value) => {
        if (enabled)
        {
          onToggleChange.Invoke(value);
        }
      });
    }
  }
}